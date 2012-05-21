/********************************************************************************
 *
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,
 * EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
 *
 ********************************************************************************/

using System;
using System.Reflection;
using NUnit.Framework;
using TheJoyOfCode.QualityTools.Extensions;
using TheJoyOfCode.QualityTools.Tests.DummyProject.WithErrors;
using TheJoyOfCode.QualityTools.Tests.TestSubjects;

namespace TheJoyOfCode.QualityTools.Tests
{
    [TestFixture]
    public class AssemblyTesterTest
    {
       

        [Test]
        public void AssemblyTester_AllExcluded()
        {
            var tester = new AssemblyTester(Assembly.GetExecutingAssembly());
            tester.Exclusions.AddNamespace("TheJoyOfCode", true);
            tester.TestAssembly(true, true);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AssemblyTester_ConstructorNull()
        {
            new AssemblyTester(null);
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void AssemblyTester_ConstructorError()
        {
            new AssemblyTester(null);
        }

        [Test]
        public void AssemblyTester_Self()
        {
            var tester = new AssemblyTester(Assembly.GetExecutingAssembly());
            tester.Exclusions.AddType(typeof(DummyWithSameEquals));
            tester.Exclusions.AddType(typeof(DummyUsesSameEquals));
            tester.Exclusions.AddType(typeof(DummyBadProperty));
            tester.Exclusions.AddType(typeof(DummyNoEvent));
            tester.Exclusions.AddType(typeof(DummyConstructorNotMapped));
            tester.Exclusions.AddType(typeof(DummyCtorAbstractParams));
            tester.Exclusions.AddType(typeof(ISomeInterface));
            tester.Exclusions.AddType(typeof(DummyGeneric<,,,>));
            tester.Exclusions.AddType(typeof(IBasicProperties));
            tester.TestAssembly(true, true);
        }

        [Test]
        [ExpectedException(typeof (AssemblyTestException))]
        public void AssemblyTester_Self_WithoutExclusions()
        {
            var tester = new AssemblyTester(Assembly.GetExecutingAssembly());
            tester.TestAssembly(true, true);
        }

        [Test]
        public void AssemblyTester_PassWithExclusions()
        {
            var tester = new AssemblyTester(typeof(IncorrectConstructors).Assembly);
            tester.Exclusions.AddNamespace("TheJoyOfCode.QualityTools.Tests.DummyProject.WithErrors", false);
            tester.TestAssembly(true, true);
        }

        [Test]
        [ExpectedException(typeof(AssemblyTestException))]
        public void AssemblyTester_FailNoExclusions()
        {
            var tester = new AssemblyTester(typeof(IncorrectConstructors).Assembly);
            tester.TestAssembly(true, true);
        }

        [Test]
        public void AssemblyTester_TestQualityTools()
        {
            var tester = new AssemblyTester(typeof (AssemblyTester).Assembly, new CustomTypeFactory(), true);
            tester.Exclusions.AddType<AssemblyTester>();
            tester.Exclusions.AddType<MethodSignature>();
            tester.Exclusions.AddType<MethodSignatureCollection>();
            tester.Exclusions.AddType<TestExclusion>();
            tester.Exclusions.AddType<AssemblyTestExclusions>();
            tester.Exclusions.AddType<AssemblyTester>();
            tester.Exclusions.AddType(typeof (Guard));
            tester.Exclusions.AddType(typeof (ExpressionExtensions));
            tester.TestAssembly(true, true);

        }

        private class CustomTypeFactory : TypeFactory
        {
            private readonly Type _iTypeFactoryType = typeof(ITypeFactory);

            public override bool CanCreateInstance(Type type)
            {
                if (type == _iTypeFactoryType || type.IsSubclassOf(_iTypeFactoryType))
                {
                    return true;
                }
                return base.CanCreateInstance(type);
            }

            public override object CreateRandomValue(Type type)
            {
                if (type == _iTypeFactoryType || type.IsSubclassOf(_iTypeFactoryType))
                {
                    return new TypeFactory();
                }
                return base.CreateRandomValue(type);
            }
        }

        [Test]
        [ExpectedException(typeof (AssemblyTestException))]
        public void AssemblyTester_TestQualityTools_WithoutExclusions()
        {
            var tester = new AssemblyTester(typeof(AssemblyTester).Assembly);
            tester.TestAssembly(true, true);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void AssemblyTester_TestQualityTools_BothFalse()
        {
            var tester = new AssemblyTester(typeof(AssemblyTester).Assembly);
            tester.TestAssembly(false, false);
        }

        [Test]
        public void AssemblyTester_TestQualityTools_CustomTestExclusion()
        {
            var tester = new AssemblyTester(typeof(IncorrectConstructors).Assembly);
            tester.Exclusions.Add(new FullNameContainsStringExclusion("WithErrors"));
            tester.TestAssembly(true, true);
        }

        private class FullNameContainsStringExclusion : TestExclusion
        {
            private string _searchFor;

            public FullNameContainsStringExclusion(string searchFor)
            {
                _searchFor = searchFor;
            }

            public override bool IsExcluded(Type type)
            {
                return type.FullName.Contains(_searchFor);
            }
        }
    }
}