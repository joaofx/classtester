using NUnit.Framework;
using TheJoyOfCode.QualityTools.Extensions;

namespace TheJoyOfCode.QualityTools.Tests.Extensions
{
    [TestFixture]
    public class ExpressionExtensionsTests
    {
        public class Foo
        {
            public string GetSetProperty { get; set; }
            public string InternalSetProperty { get; internal set; }
            public string PrivateSetProperty { get; private set; }
            public string InternalGetProperty { internal get; set; }
            public string PrivateGetProperty { private get; set; }

            public string GetProperty
            {
                get
                {
                    return null;
                }
            }
            public string SetProperty
            {
                set
                {
                }
            }

            public string this[int index]
            {
                get
                {
                    return null;
                }
                set
                {

                }
            }

            public string ReturnMethod()
            {
                return null;
            }
            public string ReturnMethod(string arg1)
            {
                return null;
            }
            public string ReturnMethod(string arg1, bool arg2)
            {
                return null;
            }
            public string ReturnMethod(string arg1, bool arg2, int arg3)
            {
                return null;
            }
            public string ReturnMethod(string arg1, bool arg2, int arg3, long arg4)
            {
                return null;
            }

            public void NullMethod()
            {
            }
            public void NullMethod(string arg1)
            {
            }
            public void NullMethod(string arg1, bool arg2)
            {
            }
            public void NullMethod(string arg1, bool arg2, int arg3)
            {
            }
            public void NullMethod(string arg1, bool arg2, int arg3, long arg4)
            {
            }
        }


        [TestFixture]
        public class GetMethodInfoInstanceTests
        {

            [Test]
            public void ReturnZeroArgs()
            {
                var foo = new Foo();
                var methodInfo = ExpressionExtensions.GetMethodInfo(() => foo.ReturnMethod());
                Assert.AreEqual("ReturnMethod", methodInfo.Name);
            }
            [Test]
            public void ReturnOneArg()
            {
                var foo = new Foo();
                var methodInfo = ExpressionExtensions.GetMethodInfo(() => foo.ReturnMethod(string.Empty));
                Assert.AreEqual("ReturnMethod", methodInfo.Name);
            }
            [Test]
            public void ReturnTwoArg()
            {
                var foo = new Foo();
                var methodInfo = ExpressionExtensions.GetMethodInfo(() => foo.ReturnMethod(string.Empty, false));
                Assert.AreEqual("ReturnMethod", methodInfo.Name);
            }
            [Test]
            public void NoReturnZeroArgs()
            {
                var foo = new Foo();
                var methodInfo = ExpressionExtensions.GetMethodInfo(() => foo.NullMethod());
                Assert.AreEqual("NullMethod", methodInfo.Name);
            }
            [Test]
            public void NoReturnOneArg()
            {
                var foo = new Foo();
                var methodInfo = ExpressionExtensions.GetMethodInfo(() => foo.NullMethod(string.Empty));
                Assert.AreEqual("NullMethod", methodInfo.Name);
            }
            [Test]
            public void NoReturnTwoArg()
            {
                var foo = new Foo();
                var methodInfo = ExpressionExtensions.GetMethodInfo(() => foo.NullMethod(string.Empty, false));
                Assert.AreEqual("NullMethod", methodInfo.Name);
            }
        }
        [TestFixture]
        public class GetMethodInfoStaticTests
        {

            [Test]
            public void ReturnZeroArgs()
            {
                var methodInfo = ExpressionExtensions.GetMethodInfo<Foo>(x => x.ReturnMethod());
                Assert.AreEqual("ReturnMethod", methodInfo.Name);
            }
            [Test]
            public void ReturnOneArg()
            {
                var methodInfo = ExpressionExtensions.GetMethodInfo<Foo>(x => x.ReturnMethod(string.Empty));
                Assert.AreEqual("ReturnMethod", methodInfo.Name);
            }
            [Test]
            public void ReturnTwoArg()
            {
                var methodInfo = ExpressionExtensions.GetMethodInfo<Foo>(x => x.ReturnMethod(string.Empty, false));
                Assert.AreEqual("ReturnMethod", methodInfo.Name);
            }
            [Test]
            public void NoReturnZeroArgs()
            {
                var methodInfo = ExpressionExtensions.GetMethodInfo<Foo>(x => x.NullMethod());
                Assert.AreEqual("NullMethod", methodInfo.Name);
            }
            [Test]
            public void NoReturnOneArg()
            {
                var methodInfo = ExpressionExtensions.GetMethodInfo<Foo>(x => x.NullMethod(string.Empty));
                Assert.AreEqual("NullMethod", methodInfo.Name);
            }
            [Test]
            public void NoReturnTwoArg()
            {
                var methodInfo = ExpressionExtensions.GetMethodInfo<Foo>(x => x.NullMethod(string.Empty, false));
                Assert.AreEqual("NullMethod", methodInfo.Name);
            }
        }
        [TestFixture]
        public class GetPropertyInfoInstanceTests
        {

            [Test]
            public void GetSet()
            {
                var foo = new Foo();
                var propertyInfo = ExpressionExtensions.GetPropertyInfo(() => foo.GetSetProperty);
                Assert.AreEqual("GetSetProperty", propertyInfo.Name);
            }
            [Test]
            public void Get()
            {
                var foo = new Foo();
                var propertyInfo = ExpressionExtensions.GetPropertyInfo(() => foo.GetProperty);
                Assert.AreEqual("GetProperty", propertyInfo.Name);
            }
            [Test]
            public void InternalGet()
            {
                var foo = new Foo();
                var propertyInfo = ExpressionExtensions.GetPropertyInfo(() => foo.InternalGetProperty);
                Assert.AreEqual("InternalGetProperty", propertyInfo.Name);
            }
            //[Test]
            //public void PrivateGet()
            //{
            //    Foo foo = new Foo();
            //    PropertyInfo propertyInfo = ExpressionExtensions.GetPropertyInfo(() => foo.PrivateGetProperty);
            //    Assert.AreEqual("PrivateGetProperty", propertyInfo.Name);
            //}
            //[Test]
            //public void Set()
            //{
            //    Foo foo = new Foo();
            //    PropertyInfo propertyInfo = ExpressionExtensions.GetPropertyInfo(() => foo.SetProperty);
            //    Assert.AreEqual("SetProperty", propertyInfo.Name);
            //}
            [Test]
            public void InternalSet()
            {
                var foo = new Foo();
                var propertyInfo = ExpressionExtensions.GetPropertyInfo(() => foo.InternalSetProperty);
                Assert.AreEqual("InternalSetProperty", propertyInfo.Name);
            }
            [Test]
            public void PrivateSet()
            {
                var foo = new Foo();
                var propertyInfo = ExpressionExtensions.GetPropertyInfo(() => foo.PrivateSetProperty);
                Assert.AreEqual("PrivateSetProperty", propertyInfo.Name);
            }

        }
        [TestFixture]
        public class GetPropertyInfoStaticTests
        {


            [Test]
            public void GetSet()
            {
                var propertyInfo = ExpressionExtensions.GetPropertyInfo<Foo>(x => x.GetSetProperty);
                Assert.AreEqual("GetSetProperty", propertyInfo.Name);
            }
            [Test]
            public void Get()
            {
                var propertyInfo = ExpressionExtensions.GetPropertyInfo<Foo>(x => x.GetProperty);
                Assert.AreEqual("GetProperty", propertyInfo.Name);
            }
            [Test]
            public void InternalGet()
            {
                var propertyInfo = ExpressionExtensions.GetPropertyInfo<Foo>(x => x.InternalGetProperty);
                Assert.AreEqual("InternalGetProperty", propertyInfo.Name);
            }
            //[Test]
            //public void PrivateGet()
            //{
            //    PropertyInfo propertyInfo = ExpressionExtensions.GetPropertyInfo<Foo>(x => x.PrivateGetProperty);
            //    Assert.AreEqual("PrivateGetProperty", propertyInfo.Name);
            //}
            //[Test]
            //public void Set()
            //{
            //    PropertyInfo propertyInfo = ExpressionExtensions.GetPropertyInfo<Foo>(x => x.SetProperty);
            //    Assert.AreEqual("SetProperty", propertyInfo.Name);
            //}
            [Test]
            public void InternalSet()
            {
                var propertyInfo = ExpressionExtensions.GetPropertyInfo<Foo>(x => x.InternalSetProperty);
                Assert.AreEqual("InternalSetProperty", propertyInfo.Name);
            }
            [Test]
            public void PrivateSet()
            {
                var propertyInfo = ExpressionExtensions.GetPropertyInfo<Foo>(x => x.PrivateSetProperty);
                Assert.AreEqual("PrivateSetProperty", propertyInfo.Name);
            }

        }
        [TestFixture]
        public class GetPropertyNameInstanceTests
        {


            [Test]
            public void GetSet()
            {
                var foo = new Foo();
                var memberName = ExpressionExtensions.GetPropertyName(() => foo.GetSetProperty);
                Assert.AreEqual("GetSetProperty", memberName);
            }
            [Test]
            public void Get()
            {
                var foo = new Foo();
                var memberName = ExpressionExtensions.GetPropertyName(() => foo.GetProperty);
                Assert.AreEqual("GetProperty", memberName);
            }
            [Test]
            public void InternalGet()
            {
                var foo = new Foo();
                var memberName = ExpressionExtensions.GetPropertyName(() => foo.InternalGetProperty);
                Assert.AreEqual("InternalGetProperty", memberName);
            }
            //[TestMethod]
            //public void PrivateGet()
            //{
            //    Foo foo = new Foo();
            //    string memberName = ExpressionExtensions.GetPropertyName(() => foo.PrivateGetProperty);
            //    Assert.AreEqual("PrivateGetProperty", memberName);
            //}
            //[TestMethod]
            //public void Set()
            //{
            //    Foo foo = new Foo();
            //    PropertyInfo memberName = ExpressionExtensions.GetPropertyName(() => foo.SetProperty);
            //    Assert.AreEqual("SetProperty", memberName.Name);
            //}
            [Test]
            public void InternalSet()
            {
                var foo = new Foo();
                var memberName = ExpressionExtensions.GetPropertyName(() => foo.InternalSetProperty);
                Assert.AreEqual("InternalSetProperty", memberName);
            }
            [Test]
            public void PrivateSet()
            {
                var foo = new Foo();
                var memberName = ExpressionExtensions.GetPropertyName(() => foo.PrivateSetProperty);
                Assert.AreEqual("PrivateSetProperty", memberName);
            }

        }
        [TestFixture]
        public class GetPropertyNameStaticTests
        {


            [Test]
            public void GetSet()
            {
                var memberName = ExpressionExtensions.GetPropertyName<Foo>(x => x.GetSetProperty);
                Assert.AreEqual("GetSetProperty", memberName);
            }
            [Test]
            public void Get()
            {
                var memberName = ExpressionExtensions.GetPropertyName<Foo>(x => x.GetProperty);
                Assert.AreEqual("GetProperty", memberName);
            }
            [Test]
            public void InternalGet()
            {
                var memberName = ExpressionExtensions.GetPropertyName<Foo>(x => x.InternalGetProperty);
                Assert.AreEqual("InternalGetProperty", memberName);
            }
            //[TestMethod]
            //public void PrivateGet()
            //{
            //    string memberName = ExpressionExtensions.GetPropertyName<Foo>(x => x.SetProperty);
            //    Assert.AreEqual("PrivateGetProperty", memberName);
            //}
            //[TestMethod]
            //public void Set()
            //{
            //    string memberName = ExpressionExtensions.GetPropertyName<Foo>(x => x.SetProperty);
            //    Assert.AreEqual("SetProperty", memberName);
            //}
            [Test]
            public void InternalSet()
            {
                var memberName = ExpressionExtensions.GetPropertyName<Foo>(x => x.InternalSetProperty);
                Assert.AreEqual("InternalSetProperty", memberName);
            }
            [Test]
            public void PrivateSet()
            {
                var memberName = ExpressionExtensions.GetPropertyName<Foo>(x => x.PrivateSetProperty);
                Assert.AreEqual("PrivateSetProperty", memberName);
            }

        }
    }
}