using System;
using NUnit.Framework;

namespace TheJoyOfCode.QualityTools.Tests
{
    [TestFixture]
    public class MethodSignatureTests
    {

        [Test]
        public void ToString_Works()
        {
            var ms1 = new MethodSignature(typeof(Type[]), typeof(object));
            Assert.AreEqual("(System.Type[], System.Object)", ms1.ToString());

            var ms2 = new MethodSignature(new Type[0]);
            Assert.AreEqual("()", ms2.ToString());
        }

        [Test]
        public void Equals_TwoIdenticalSignatures()
        {
            var ms1 = new MethodSignature(typeof(MethodSignature).GetConstructors()[1].GetParameters());
            var ms2 = new MethodSignature(typeof(MethodSignature).GetConstructors()[1].GetParameters());

            Assert.AreEqual(ms1, ms2);
        }

        [Test]
        public void Equals_TwoDifferentSignaturesSameLength()
        {
            var ms1 = new MethodSignature(typeof(Type[]), typeof(object));
            var ms2 = new MethodSignature(typeof(Type), typeof(object));

            Assert.AreNotEqual(ms1, ms2);
        }

        [Test]
        public void Equals_TwoDifferentSignatures()
        {
            var ms1 = new MethodSignature(typeof(Type[]), typeof(object), typeof(string));
            var ms2 = new MethodSignature(typeof(Type[]), typeof(object));

            Assert.AreNotEqual(ms1, ms2);
            Assert.IsFalse(ms1.Equals(null));
        }

        [Test]
        public void Equals_SameClass()
        {
            var ms1 = new MethodSignature(typeof(Type[]), typeof(object));

            Assert.IsTrue(ms1.Equals(ms1));
        }
    }
}
