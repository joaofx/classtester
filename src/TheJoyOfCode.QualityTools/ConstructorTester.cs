using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Diagnostics;

namespace TheJoyOfCode.QualityTools
{
    public class ConstructorTester
    {
        private readonly Type _type;
        private readonly ITypeFactory _typeFactory;

        public ConstructorTester(Type type)
            : this (type, new TypeFactory())
        {
        }

        public ConstructorTester(Type type, ITypeFactory typeFactory)
        {
            Guard.ArgumentNotNull(type, "type");
            Guard.ArgumentNotNull(typeFactory, "typeFactory");
            IgnoredConstructors = new MethodSignatureCollection();

            if (type.IsGenericTypeDefinition)
            {
                throw new ArgumentException(string.Format(
                    "The type {0} is an open Generic Definition (e.g. Class<T>. Only closed generic types Class<int> are currently supported.",
                    type.FullName));
            }

            _type = type;
            _typeFactory = typeFactory;
        }

        /// <summary>
        /// Tests the constructors of the specified type by generating, where possible, random
        /// values and newing up the object. If testMappedProperties is specified as true, the 
        /// tester will also check to make sure that the value passed to any constructor parameters
        /// is also the value of any properties with the same name (case-insensitive) on the object.
        /// If the class has any constructors that require types that have no default value (such as
        /// an interface) this method will fail with a TesterException.
        /// </summary>
        /// <param name="testMappedProperties">Whether to test properties with the same name as the constructor parameters</param>
        public virtual void TestConstructors(bool testMappedProperties)
        {
            var constructors = _type.GetConstructors(BindingFlags.Public | BindingFlags.Instance);

            foreach (var constructor in constructors)
            {
                var parameters = constructor.GetParameters();
                var signature = new MethodSignature(parameters);

                if (IgnoredConstructors.Contains(signature))
                {
                    Debug.WriteLine("Ignoring constructor: " + signature);
                    break;
                }

                TestConstructor(constructor, parameters, signature, testMappedProperties);
            }
        }

        private void TestConstructor(ConstructorInfo constructor, ParameterInfo[] parameters, MethodSignature signature, bool testMappedProperties)
        {
            var paramValues = new object[parameters.Length];
            for (var i = 0; i < parameters.Length; i++)
            {
                if (!_typeFactory.CanCreateInstance(parameters[i].ParameterType))
                {
                    throw new ConstructorTestException(
                        string.Format(
                            "Cannot create an instance of the type '{0}' for the parameter '{1}' in the .ctor{2} for type {3}",
                            parameters[i].ParameterType,
                            parameters[i].Name,
                            signature,
                            _type));
                }
                paramValues[i] = _typeFactory.CreateRandomValue(parameters[i].ParameterType);
            }

            var result = constructor.Invoke(paramValues);

            if (testMappedProperties)
            {
                for (var i = 0; i < parameters.Length; i++)
                {
                    var parameter = parameters[i];
                    var paramValue = paramValues[i];
                    TestParam(parameter, result, paramValue);
                }
            }
        }

        private void TestParam(ParameterInfo parameter, object result, object paramValue)
        {
            var mappedProperty = _type.GetProperty(parameter.Name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (mappedProperty == null || !mappedProperty.CanRead)
            {
                return;
            }
            var valueOut = mappedProperty.GetValue(result, null);
            if (!(Equals(paramValue, valueOut)))
            {
                var message = string.Format("The value of the '{0}' property did not equal the value set with the '{1}' constructor parameter (in: '{2}', out: '{3}')",
                                            mappedProperty.Name,
                                            parameter.Name,
                                            valueOut,
                                            paramValue);
                throw new ConstructorTestException(message);
            }
        }

        public virtual MethodSignatureCollection IgnoredConstructors { get; private set; }

        /// <summary>
        /// Adds a constructor to be ignored for a specific type
        /// </summary>
        /// <typeparam name="T">The <see cref="Type"/> of the to exclude.</typeparam>
        /// <param name="expression">The <see cref="Func{T}"/> representing the constructor to exclude.</param>
        public void AddIgnoredConstructor<T>(Expression<Func<T>> expression)
        {
            var constructor = ((NewExpression)(expression.Body)).Constructor;
            IgnoredConstructors.Add(new MethodSignature(constructor.GetParameters()));
        }
    }
}
