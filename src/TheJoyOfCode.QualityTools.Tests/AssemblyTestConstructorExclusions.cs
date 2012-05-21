using NUnit.Framework;
using TheJoyOfCode.QualityTools.Tests.DummyProject.WithErrors;

namespace TheJoyOfCode.QualityTools.Tests
{
    [TestFixture]
    public class AssemblyTestConstructorExclusions
    {
        [Test]
        public void TestExcludedProperties()
        {
            var tester = new AssemblyTester(typeof(IncorrectConstructors).Assembly);
            tester.Exclusions.AddType(typeof(GenericClass<>));
            tester.AddConstructorExclusion(typeof(IncorrectConstructors), typeof(string), typeof(int), typeof(bool), typeof(string));
            tester.AddConstructorExclusion(typeof(IncorrectConstructors), typeof(string), typeof(string));
            tester.TestAssembly(false, true);
        }

        [Test]
        public void TestExcludedPropertiesLambda()
        {
            var tester = new AssemblyTester(typeof(IncorrectConstructors).Assembly);
            tester.Exclusions.AddType(typeof(GenericClass<>));
            tester.AddConstructorExclusion(() => new IncorrectConstructors(string.Empty,0, false,string.Empty));
            tester.AddConstructorExclusion(() => new IncorrectConstructors(string.Empty, string.Empty));
            tester.TestAssembly(false, true);
        }


    }
}
