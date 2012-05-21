using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Reflection;
using Microsoft.CSharp;

namespace TheJoyOfCode.QualityTools
{
    /// <summary>
    /// Static helper class to help with the construction of types and random values
    /// </summary>
    public class TypeFactory : ITypeFactory
    {
        protected readonly Random _random = new Random();

        /// <summary>
        /// Inidicates whether a type has a default public constructor, e.g. can be 'newed' up 
        /// unlike System.String which cannot
        /// </summary>
        /// <param name="type">The type to test</param>
        /// <returns>true or false</returns>
        protected virtual bool HasDefaultConstructor(Type type)
        {
            // Is this a struct? If so - can be new'ed up.
            if (type.IsSubclassOf(typeof (ValueType)))
            {
                return true;
            }
            // otherwise test for an actual default constructor
            return type.GetConstructor(new Type[] {}) != null;
        }

        /// <summary>
        /// Indicates whether the CreateRandomValue method is able to create an instance of
        /// the specified type.
        /// </summary>
        /// <param name="type">The type to test</param>
        /// <returns>true or false</returns>
        public virtual bool CanCreateInstance(Type type)
        {
            return (HasDefaultConstructor(type) || type == typeof (string) || type == typeof (Type) || type.IsArray) && !type.IsGenericTypeDefinition;
        }

        /// <summary>
        /// Indicates whether the type is a nullable type (e.g. int? or Nullable&lt;int>)
        /// </summary>
        /// <param name="type">The type to test</param>
        /// <returns></returns>
        protected virtual bool IsNullableType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof (Nullable<>);
        }

        /// <summary>
        /// Generates a random value of the specified type
        /// </summary>
        /// <param name="type">The type to be generated</param>
        /// <returns>A random value or the default instance for unknown types</returns>
        public virtual object CreateRandomValue(Type type)
        {
            var typeCode = Type.GetTypeCode(type);

            // First, is the type a nullable type? if so return a value based on the
            // generic argument.
            if (IsNullableType(type))
            {
                var subType = type.GetGenericArguments()[0];
                return CreateRandomValue(subType);
            }

            if (type.IsArray)
            {
                return Array.CreateInstance(type.GetElementType(), _random.Next(50));
            }

            if (type.IsEnum)
            {
                var values = Enum.GetValues(type);
                return values.GetValue(_random.Next(values.Length));
            }

            if (type == typeof (Guid))
            {
                return Guid.NewGuid();
            }

            if (type == typeof (Type))
            {
                return GenerateNewType();
            }

            if (type == typeof(TimeSpan))
            {
                return new TimeSpan(_random.Next(int.MaxValue));
            }

            switch (typeCode)
            {
                case TypeCode.Boolean:
                    return _random.Next(2) == 1 ? true : false;
                case TypeCode.Byte:
                    return Convert.ToByte(_random.Next(byte.MinValue, byte.MaxValue));
                case TypeCode.Char:
                    return Convert.ToChar(_random.Next(char.MinValue, char.MaxValue));
                case TypeCode.DateTime:
                    return new DateTime(_random.Next(int.MaxValue));
                case TypeCode.Decimal:
                    return Convert.ToDecimal(_random.Next(int.MaxValue));
                case TypeCode.Double:
                    return _random.NextDouble();
                case TypeCode.Int16:
                    return Convert.ToInt16(_random.Next(Int16.MinValue, Int16.MaxValue));
                case TypeCode.Int32:
                    return _random.Next(Int32.MinValue, Int32.MaxValue);
                case TypeCode.Int64:
                    return Convert.ToInt64(_random.Next(Int32.MinValue, Int32.MaxValue));
                case TypeCode.SByte:
                    return Convert.ToSByte(_random.Next(SByte.MinValue, SByte.MaxValue));
                case TypeCode.Single:
                    return Convert.ToSingle(_random.Next(SByte.MinValue, SByte.MaxValue));
                case TypeCode.String:
                    return Guid.NewGuid().ToString();
                case TypeCode.UInt16:
                    return Convert.ToUInt16(_random.Next(0, UInt16.MaxValue));
                case TypeCode.UInt32:
                    return Convert.ToUInt32(_random.Next(0, Int32.MaxValue));
                case TypeCode.UInt64:
                    return Convert.ToUInt64(_random.Next(0, Int32.MaxValue));
                default:
                    return Activator.CreateInstance(type);
            }
        }

        /// <summary>
        /// Compiles an entirely new Class (with a random name) and returns its System.Type representation
        /// </summary>
        /// <returns>System.Type for a generated, random class</returns>
        protected virtual Type GenerateNewType()
        {
            const string GENERATED_NAMESPACE = "__GeneratedNamespace";
            var className = "_" + Guid.NewGuid().ToString("N");

            var ns = new CodeNamespace(GENERATED_NAMESPACE);
            ns.Imports.Add(new CodeNamespaceImport("System"));

            var cls = new CodeTypeDeclaration(className)
                      {
                          IsClass = true, TypeAttributes = TypeAttributes.Public | TypeAttributes.Sealed
                      };
            ns.Types.Add(cls);

            var ccu = new CodeCompileUnit();
            ccu.Namespaces.Add(ns);

            var cs = new CSharpCodeProvider();
            var compilerParameters = new CompilerParameters
                                     {
                                         GenerateInMemory = true
                                     };

            var compilerResults = cs.CompileAssemblyFromDom(compilerParameters, ccu);

            if (compilerResults.Errors.Count > 0)
            {
                throw new InvalidOperationException(string.Format(
                                                        "There were {2} error(s) compiling the new type (first error shown only):{0}{1}",
                                                        Environment.NewLine,
                                                        compilerResults.Errors[0].ErrorText,
                                                        compilerResults.Errors.Count));
            }

            var generatedType = compilerResults.CompiledAssembly.GetType(GENERATED_NAMESPACE + "." + className, true);

            var result = Activator.CreateInstance(generatedType);

            return result.GetType();
        }
    }
}