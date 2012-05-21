using NUnit.Framework;

namespace TheJoyOfCode.QualityTools.Tests
{
    [TestFixture]
    public class AssemblyTestExclusionsTest
    {
        [Test]
        public void NamespaceExclusionTest()
        {
            var exclusions = new AssemblyTestExclusions();
            exclusions.AddNamespace("TheJoyOfCode.QualityTools", false);
            Assert.IsFalse(exclusions.IsExcluded(typeof(AssemblyTestExclusionsTest)));
            Assert.IsTrue(exclusions.IsExcluded(typeof(AssemblyTestExclusions)));
        }

        [Test]
        public void SubNamespaceExclusionTest()
        {
            var exclusions = new AssemblyTestExclusions();
            exclusions.AddNamespace("TheJoyOfCode", true);
            Assert.IsTrue(exclusions.IsExcluded(typeof(AssemblyTestExclusionsTest)));
            Assert.IsFalse(exclusions.IsExcluded(typeof(System.String)));
        }

        [Test]
        public void TypeExclusionTest()
        {
            var exclusions = new AssemblyTestExclusions();
            exclusions.AddType(typeof(AssemblyTestExclusionsTest));
            Assert.IsTrue(exclusions.IsExcluded(typeof(AssemblyTestExclusionsTest)));
            Assert.IsFalse(exclusions.IsExcluded(typeof(AssemblyTestExclusions)));
        }
    }
}
