/********************************************************************************
 *
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,
 * EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
 *
 ********************************************************************************/

using System;
using System.Collections.Generic;
using NUnit.Framework;
using TheJoyOfCode.QualityTools.Tests.TestSubjects;
using Rhino.Mocks;

namespace TheJoyOfCode.QualityTools.Tests
{
    [TestFixture]
    public class PropertyTesterTest
    {
        [Test]
        [ExpectedException(typeof (PropertyTestException))]
        public void TestProperties_BadProperty()
        {
            var tester = new PropertyTester(new DummyBadProperty());
            tester.TestProperties();
        }

        [Test]
        public void TestProperties_BadPropertySkipped()
        {
            var tester = new PropertyTester(new DummyBadProperty());
            tester.IgnoredProperties.Add("SomeString");
            tester.TestProperties();
        }
        [Test]
        public void TestProperties_BadPropertySkippedLambda()
        {
            var subject = new DummyBadProperty();
            var tester = new PropertyTester(subject);
            tester.AddIgnoredProperty(() => subject.SomeString);
            tester.TestProperties();
        }

        [Test]
        [ExpectedException(typeof (InvalidOperationException))]
        public void TestProperties_CannotGenerateTwoDifferentValues()
        {
            var tester = new PropertyTester(new DummyUsesSameEquals());
            tester.TestProperties();
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException))]
        public void TestProperties_ConstructorError()
        {
            new PropertyTester(null);
        }

        [Test]
        public void TestProperties_GenericClass()
        {
            var tester = new PropertyTester(new DummyGeneric<int, int?, List<string>, string>());
            tester.TestProperties();
        }

        [Test]
        [ExpectedException(typeof (PropertyTestException))]
        public void TestProperties_NoEvent()
        {
            var tester = new PropertyTester(new DummyNoEvent());
            tester.TestProperties();
        }

        [Test]
        public void TestProperties_NoEventSkipped()
        {
            var tester = new PropertyTester(new DummyNoEvent());
            tester.IgnoredProperties.Add("SomeInt");
            tester.TestProperties();
        }

        [Test]
        public void TestProperties_NoEventSkippedLambda()
        {
            var subject = new DummyNoEvent();
            var tester = new PropertyTester(subject);
            tester.AddIgnoredProperty(() => subject.SomeInt);
            tester.TestProperties();
        }

        [Test]
        public void TestProperties_Pass()
        {
            var tester = new PropertyTester(new DummyGood());
            tester.TestProperties();
        }

        [Test]
        public void TestProperties_Self()
        {
            var tester = new PropertyTester(new PropertyTester(new object()));
            tester.TestProperties();
        }

        [Test]
        public void TestProperties_UsingMock()
        {
            var repository = new MockRepository();
            var mock = repository.DynamicMock<IBasicProperties>();

            Expect.Call(mock.GetSetProperty).Return("one");
            Expect.Call(mock.GetOnlyProperty).Return("anyoldthing");
            mock.SetOnlyProperty = "two";

            repository.Replay(mock);

            var typeFactory = new CustomStringTypeFactory();
            typeFactory.ReturnValues.Enqueue("one");
            typeFactory.ReturnValues.Enqueue("two");

            var tester = new PropertyTester(mock, typeFactory);
            tester.IgnoredProperties.Add("ConstructorArguments");
            tester.TestProperties();
            repository.Verify(mock);
        }

        private class CustomStringTypeFactory : ITypeFactory
        {
            public CustomStringTypeFactory()
            {
                ReturnValues = new Queue<string>();
            }

            public Queue<string> ReturnValues { get; private set; }

            #region ITypeFactory Members

            public bool CanCreateInstance(Type type)
            {
                return type == typeof(string);
            }

            public object CreateRandomValue(Type type)
            {
                return ReturnValues.Dequeue();
            }

            #endregion
        }
    }
}