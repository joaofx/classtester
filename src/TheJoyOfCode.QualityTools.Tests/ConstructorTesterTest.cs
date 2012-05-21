/********************************************************************************
 *
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,
 * EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
 *
 ********************************************************************************/

using NUnit.Framework;
using TheJoyOfCode.QualityTools.Tests.DummyProject.WithErrors;

namespace TheJoyOfCode.QualityTools.Tests
{
    [TestFixture]
    public class ConstructorTesterTest
    {
        [Test]
        public void TestConstructors_DummyGood()
        {
            var tester = new ConstructorTester(typeof(DummyGood));
            tester.TestConstructors(true);
        }

        [Test]
        [ExpectedException(typeof(ConstructorTestException))]
        public void TestConstructors_DummyConstructorNotMapped()
        {
            var tester = new ConstructorTester(typeof(DummyConstructorNotMapped));
            tester.TestConstructors(true);
        }

        [Test]
        public void TestConstructors_ParamsNotMappedSkipped()
        {
            var tester = new ConstructorTester(typeof(DummyConstructorNotMapped));
            tester.TestConstructors(false);
   
        }

        [Test]
        public void TestConstructors_Generic()
        {
            var tester = new ConstructorTester(typeof(DummyGeneric<string, int, int?, MyEnum>));
            tester.TestConstructors(false);
        }

        [Test]
        [ExpectedException(typeof(ConstructorTestException), ExpectedMessage = "Cannot create an instance of the type 'TheJoyOfCode.QualityTools.Tests.ISomeInterface' for the parameter 'someInstance' in the .ctor(System.String, TheJoyOfCode.QualityTools.Tests.ISomeInterface, System.Object) for type TheJoyOfCode.QualityTools.Tests.DummyCtorAbstractParams")]
        public void TestConstructors_WithAbstractParams()
        {
            var tester = new ConstructorTester(typeof(DummyCtorAbstractParams));
            tester.TestConstructors(true);
        }

        [Test]
        public void TestConstructors_IngoreAbstractConstructor()
        {
            var tester = new ConstructorTester(typeof(DummyCtorAbstractParams));
            tester.IgnoredConstructors.Add(typeof(string), typeof(ISomeInterface), typeof(object));
            tester.TestConstructors(true);
        }

        [Test]
        public void TestConstructors_IngoreAbstractConstructorLambda()
        {
            var tester = new ConstructorTester(typeof(DummyCtorAbstractParams));
            tester.AddIgnoredConstructor(() => new DummyCtorAbstractParams(string.Empty, null, null));
            tester.TestConstructors(true);
        }

        [Test]
        [ExpectedException(typeof(ConstructorTestException))]
        public void TestConstructors_TestDodgyConstructor()
        {
            var tester = new ConstructorTester(typeof(IncorrectConstructors));
            tester.TestConstructors(true);
        }
    }
}
