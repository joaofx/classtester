using System;
using System.Collections.Generic;

namespace TheJoyOfCode.QualityTools
{
    /// <summary>
    /// Manages a list of exclusions and testing those exclusions against a specific type
    /// </summary>
    public class AssemblyTestExclusions : List<TestExclusion>
    {
        /// <summary>
        /// Adds a whole namespace to the list of excluded types
        /// </summary>
        /// <param name="namespace">The namespace to exclude, e.g: System.Collections</param>
        /// <param name="includeSubNamespaces">Whether subnamespaces should also be included</param>
        public void AddNamespace(string @namespace, bool includeSubNamespaces)
        {
            Add(new NamespaceExclusion(@namespace, includeSubNamespaces));
        }

        /// <summary>
        /// Adds a specific type to the list of excluded types
        /// </summary>
        /// <param name="type">The Type to exclude</param>
        public void AddType(Type type)
        {
            Add(new TypeExclusion(type));
        }
        /// <summary>
        /// Adds a specific type to the list of excluded types
        /// </summary>
        /// <typeparam name="T">The Type to exclude</typeparam>
        public void AddType<T>()
        {
            AddType(typeof(T));
        }

        /// <summary>
        /// Tests a specific type to see if it falls under one of the exclusion rules
        /// </summary>
        /// <typeparam name="T">The type to be tested</typeparam>
        /// <returns>true if this type should be excluded</returns>
        public bool IsExcluded<T>()
        {
            return IsExcluded(typeof (T));
        }

        /// <summary>
        /// Tests a specific type to see if it falls under one of the exclusion rules
        /// </summary>
        /// <param name="type">The type to be tested</param>
        /// <returns>true if this type should be excluded</returns>
        public bool IsExcluded(Type type)
        {
            foreach (var exclusion in this)
            {
                if (exclusion.IsExcluded(type))
                {
                    return true;
                }
            }

            return false;
        }

        #region Nested type: NamespaceExclusion

        /// <summary>
        /// nested class implementing a strategy to test namespace exclusions
        /// </summary>
        private class NamespaceExclusion : TestExclusion
        {
            private readonly bool _includeSubNamespaces;
            private readonly string _namespace;

            public NamespaceExclusion(string @namespace, bool includeSubNamespaces)
            {
                Guard.ArgumentNotNullOrEmptyString(@namespace, "@namespace");
                _namespace = @namespace;
                _includeSubNamespaces = includeSubNamespaces;
            }

            public override bool IsExcluded(Type type)
            {
                if (type.Namespace == _namespace)
                {
                    return true;
                }
                else if (_includeSubNamespaces && type.Namespace.StartsWith(_namespace + "."))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        #endregion

        #region Nested type: TypeExclusion

        /// <summary>
        /// nested class implementing a strategy to test type exclusions
        /// </summary>
        private class TypeExclusion : TestExclusion
        {
            private readonly Type _type;

            public TypeExclusion(Type type)
            {
                Guard.ArgumentNotNull(type,"type");
                _type = type;
            }

            public override bool IsExcluded(Type type)
            {
                return _type.Equals(type);
            }
        }

        #endregion
    }

    /// <summary>
    /// Base for creating an exclusion rule. You can implement your own exclusion rules
    /// by inheriting from this type and adding to the collection.
    /// </summary>
    public abstract class TestExclusion
    {
        /// <summary>
        /// Tests a specific type to see if it is excluded by this rule or not
        /// </summary>
        /// <param name="type">The type to be tested</param>
        /// <returns>true if this type should be excluded</returns>
        public abstract bool IsExcluded(Type type);
    }
}