/********************************************************************************
 *
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,
 * EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
 *
 ********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using TheJoyOfCode.QualityTools.Extensions;

namespace TheJoyOfCode.QualityTools
{
    /// <summary>
    /// Together with the <see cref="TheJoyOfCode.QualityTools.ConstructorTester"/> and <see cref="TheJoyOfCode.QualityTools.PropertyTester"/> the AssemblyTester
    /// offers the benefits of not having to write a test for every class in your assembly.
    /// </summary>
    /// <exception cref="AssemblyTestException" />
    public class AssemblyTester
    {
        private readonly AssemblyTestExclusions _exclusions = new AssemblyTestExclusions();
        private readonly Assembly _assembly;
        private readonly List<string> _testErrors = new List<string>();
        private readonly ITypeFactory _typeFactory;
        private readonly bool _throwOnFirst;
        private readonly Dictionary<Type, List<string>> _propertyExclusions = new Dictionary<Type, List<string>>();
        private readonly Dictionary<Type, List<MethodSignature>> _constructorExclusions = new Dictionary<Type, List<MethodSignature>>();

        /// <summary>
        /// Creates a new instance of the AssemblyTester ready to test the specified assembly
        /// </summary>
        /// <param name="assembly">The assembly to test</param>
        public AssemblyTester(Assembly assembly)
            : this(assembly, new TypeFactory())
        {
        }

        /// <summary>
        /// Creates a new instance of the AssemblyTester ready to test the specified assembly
        /// </summary>
        /// <param name="assembly">The assembly to test</param>
        /// <param name="throwOnFirst">Whether the assembly should throw at the first error or generate a report</param>
        public AssemblyTester(Assembly assembly, bool throwOnFirst)
            : this(assembly, new TypeFactory(), throwOnFirst)
        {
        }

        /// <summary>
        /// Creates a new instance of the AssemblyTester ready to test the specified assembly
        /// </summary>
        /// <param name="assembly">The assembly to test</param>
        /// <param name="typeFactory">The ITypeFactory instance to use to create types</param>
        public AssemblyTester(Assembly assembly, ITypeFactory typeFactory)
            : this(assembly, typeFactory, false)
        {
        }

        /// <summary>
        /// Creates a new instance of the AssemblyTester ready to test the specified assembly
        /// </summary>
        /// <param name="assembly">The assembly to test</param>
        /// <param name="typeFactory">The ITypeFactory instance to use to create types</param>
        /// <param name="throwOnFirst">Whether the assembly should throw at the first error or generate a report</param>
        public AssemblyTester(Assembly assembly, ITypeFactory typeFactory, bool throwOnFirst)
        {
            Guard.ArgumentNotNull(assembly, "assembly");
            Guard.ArgumentNotNull(typeFactory, "typeFactory");

            _assembly = assembly;
            _typeFactory = typeFactory;
            _throwOnFirst = throwOnFirst;
        }

        /// <summary>
        /// Starts testing the assembly
        /// </summary>
        /// <param name="testProperties">Indicates whether properties should be automatically tested</param>
        /// <param name="testConstructors">Indicates whether constructors should be automatically tested</param>
        public void TestAssembly(bool testProperties, bool testConstructors)
        {
            if (!testProperties && !testConstructors)
            {
                throw new ArgumentException("It is invalid to specify both the testProperties and testConstructors parameters as false");
            }

            var types = _assembly.GetTypes();

            foreach (var type in types)
            {
                if (!_exclusions.IsExcluded(type))
                {
                    TestClass(type, testProperties, testConstructors);
                }
            }

            AssertTestErrors();
        }

        private void AssertTestErrors()
        {
            if (_testErrors.Count > 0)
            {
                var message = new StringBuilder();
                message.AppendFormat("Testing the assembly {0} produced the following errors:{1}------------{1}", _assembly.FullName, Environment.NewLine);

                foreach (var error in _testErrors)
                {
                    message.AppendFormat("{0}{1}{1}", error, Environment.NewLine);
                }

                throw new AssemblyTestException(message.ToString());
            }
        }

        private void TestClass(Type type, bool testProperties, bool testConstructors)
        {
            if (testConstructors)
            {
                TestConstructors(type);
            }
            if (testProperties)
            {
                TestProperties(type);
            }
        }

        /// <summary>
        /// Adds a list of properties to exclude for a specific type
        /// </summary>
        /// <param name="type">The type on which to exclude properties</param>
        /// <param name="properties">A list of properties to exclude</param>
        public void AddPropertyExclusions(Type type, params string[] properties)
        {
            if (_propertyExclusions.ContainsKey(type))
            {
                _propertyExclusions[type].AddRange(properties);
            }
            else
            {
                _propertyExclusions.Add(type, new List<string>(properties));
            }
        }
        /// <summary>
        /// Adds a list of properties to exclude for a specific type
        /// </summary>
        /// <param name="type">The type on which to exclude properties</param>
        /// <param name="properties">A list of properties to exclude</param>
        public void AddPropertyExclusion<TMember>(Expression<Func<TMember, object>> property)
        {
            AddPropertyExclusions(property.GetMemberParentType(), property.GetPropertyName());
        }

        /// <summary>
        /// Adds a constructor to be excluded for a specific type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="parameterTypes"></param>
        public void AddConstructorExclusion(Type type, params Type[] parameterTypes)
        {
            var signature = new MethodSignature(parameterTypes);

            if (_constructorExclusions.ContainsKey(type))
            {
                _constructorExclusions[type].Add(signature);
            }
            else
            {
                _constructorExclusions.Add(type, new List<MethodSignature> {signature});
            }
        }
        /// <summary>
        /// Adds a constructor to be excluded for a specific type
        /// </summary>
        /// <typeparam name="T">The <see cref="Type"/> of the to exclude.</typeparam>
        /// <param name="expression">The <see cref="Func{T}"/> representing the constructor to exclude.</param>
        public void AddConstructorExclusion<T>(Expression<Func<T>> expression)
        {
            var type = expression.Body.Type;
            var constructor = ((NewExpression) (expression.Body)).Constructor;
            var parameterTypes = new List<Type>();
            foreach (var info in constructor.GetParameters())
            {
                parameterTypes.Add(info.ParameterType);
            }
            AddConstructorExclusion(type, parameterTypes.ToArray());
        }

        /// <summary>
        /// A list of excluded types and namespaces that shouldn't be tested (either properties or constructors).
        /// </summary>
        public AssemblyTestExclusions Exclusions
        {
            get
            {
                return _exclusions;
            }
        }

        private void TestProperties(Type type)
        {
            try
            {
                var tester = CreateClassTester(type);
                if (tester != null)
                {
                    tester.TestProperties();
                }
            }
            catch (Exception ex)
            {
                if (_throwOnFirst)
                {
                    throw;
                }
                _testErrors.Add(string.Format("{0} [TestProperties,Type:{1}]", ex.Message, type));
            }
        }

        private void TestConstructors(Type type)
        {
            try
            {
                var tester = new ConstructorTester(type, _typeFactory);

                if (_constructorExclusions.ContainsKey(type))
                {
                    foreach (var signature in _constructorExclusions[type] )
                    {
                        tester.IgnoredConstructors.Add(signature);
                    }
                }

                tester.TestConstructors(true);
            }
            catch (Exception ex)
            {
                if (_throwOnFirst)
                {
                    throw;
                }
                _testErrors.Add(string.Format("{0} [TestConstructors,Type:{1}]", ex.Message, type));
            }
        }

        private PropertyTester CreateClassTester(Type type)
        {
            object instance;

            if (_typeFactory.CanCreateInstance(type))
            {
                instance = _typeFactory.CreateRandomValue(type);
            }
            else
            {
                instance = TryCreateInstanstanceFromConstructor(type);
                if (instance == null)
                {
                    _testErrors.Add(string.Format("Can not create type dynamically [TestProperties,Type:{0}]", type));
                }
            }

            var tester = new PropertyTester(instance, _typeFactory);

            // Add any property exclusions
            if (_propertyExclusions.ContainsKey(type))
            {
                foreach (var property in _propertyExclusions[type])
                {
                    tester.IgnoredProperties.Add(property);
                }
            }

            return tester;
        }

        private object TryCreateInstanstanceFromConstructor(Type type)
        {
            object classInstance = null;
            var constructors = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance);

            foreach (var constructor in constructors)
            {
                var canCreateFromConstructor = true;
                var parameters = constructor.GetParameters();
                var paramValues = new object[parameters.Length];
                for (var i = 0; i < parameters.Length; i++)
                {
                    if (_typeFactory.CanCreateInstance(parameters[i].ParameterType))
                    {
                        paramValues[i] = _typeFactory.CreateRandomValue(parameters[i].ParameterType);
                    }
                    else
                    {
                        canCreateFromConstructor = false;
                    }
                }

                if (canCreateFromConstructor)
                {
                    classInstance = constructor.Invoke(paramValues);
                    break;
                }
            }
            return classInstance;
        }

      
    }
}
