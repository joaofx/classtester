using System;
using System.Reflection;

namespace TheJoyOfCode.QualityTools
{
    public class MethodSignature
    {
        private readonly Type[] _types;

        public MethodSignature(params Type[] types)
        {
            _types = types;
        }

        public MethodSignature(params ParameterInfo[] parameters)
        {
            _types = new Type[parameters.Length];

            for (var i = 0; i < parameters.Length; i++)
            {
                _types[i] = parameters[i].ParameterType;
            }
        }

        /// <summary>
        /// List of types (in order) that represent this method signature. 
        /// Details such as out, ref, params and parameter names are ignored.
        /// </summary>
        public Type[] Types
        {
            get { return _types; }
        }

        /// <summary>
        /// Generates a string that would identify an overload of a method signature, 
        /// e.g. (int, int, string, object)
        /// </summary>
        /// <returns>list of types in parantheses</returns>
        public override string ToString()
        {
            var methodSignature = "(";

            for (var i = 0; i < _types.Length; i++)
            {
                methodSignature += _types[i].ToString();

                if (i != _types.Length - 1)
                {
                    methodSignature += ", ";
                }
            }

            return methodSignature + ")";
        }

        /// <summary>
        /// Tests for equality by comparing this types internal Types array against another
        /// MethodSignature's internal Types array. Any other type returns false.
        /// </summary>
        /// <param name="obj">The MethodSignature for testing</param>
        /// <returns>true or false</returns>
        public override bool Equals(object obj)
        {
            var ms = obj as MethodSignature;

            if (ms == null)
                return false;

            if (ReferenceEquals(ms, this))
                return true;

            if (ms.Types.Length != Types.Length)
                return false;

            for (var i = 0; i < ms.Types.Length; i++)
            {
                if (!ms.Types[i].Equals(Types[i]))
                    return false;
            }

            return true;
        }
    }
}