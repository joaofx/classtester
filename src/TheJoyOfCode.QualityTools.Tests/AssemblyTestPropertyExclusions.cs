using NUnit.Framework;
using TheJoyOfCode.QualityTools.Tests.DummyProject.WithErrors;

namespace TheJoyOfCode.QualityTools.Tests
{
    [TestFixture]
    public class AssemblyTestPropertyExclusions
    {
        [Test]
        public void TestExcludedProperties()
        {
            var tester = new AssemblyTester(typeof(IncorrectConstructors).Assembly);
            tester.Exclusions.AddType(typeof(GenericClass<>));
            tester.AddPropertyExclusions(typeof(IncorrectProperties), "Dummy4", "Dummy3");
            tester.TestAssembly(true, false);
        }
        [Test]
        public void TestExcludedPropertiesWithLambda()
        {
            var tester = new AssemblyTester(typeof(IncorrectConstructors).Assembly);
            tester.Exclusions.AddType(typeof(GenericClass<>));

            tester.AddPropertyExclusion<IncorrectProperties>(x => x.Dummy4);
            tester.AddPropertyExclusion<IncorrectProperties>(x => x.Dummy3);
            tester.TestAssembly(true, false);
        }

        [Test]
        public void TestExcludedPropertiesAddedSeparately()
        {
            var tester = new AssemblyTester(typeof(IncorrectConstructors).Assembly);
            tester.Exclusions.AddType(typeof(GenericClass<>));
            tester.AddPropertyExclusions(typeof(IncorrectProperties), "Dummy4");
            tester.AddPropertyExclusions(typeof(IncorrectProperties), "Dummy3");
            tester.TestAssembly(true, false);
        }
    }
}
