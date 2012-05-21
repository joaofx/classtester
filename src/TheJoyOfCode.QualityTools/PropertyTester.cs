/********************************************************************************
 *
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,
 * EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
 *
 ********************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace TheJoyOfCode.QualityTools
{
    /// <summary>
    /// For use in unit tests, the PropertyTester offers a number of benefits
    ///     - increased coverage testing all those property setters and getters that normally get ignored
    ///     - tests that simple properties are wired up correctly
    ///     - tests the implementation of INotifyPropertyChanged for classes that implement it
    /// For more information, see the help on the TestProperties method and the static TestConstructors method.
    /// It is designed to test simple POCO classes only - any complicated properties or constructors
    /// should be tested with a manual unit test as normal. It is important to use this tool in 
    /// conjunction with code coverage to ensure you are getting the coverage you think you are.
    /// </summary>
    /// <exception cref="TheJoyOfCode.QualityTools.PropertyTestException" />
    public class PropertyTester
    {
        private readonly bool _iNotifyPropertyChanged;
        private string _lastPropertyChanged;
        private readonly object _subject;
        private readonly Type _subjectType;
        private readonly ITypeFactory _typeFactory;

        public PropertyTester(object subject)
            : this(subject, new TypeFactory())
        { }

        public PropertyTester(object subject, ITypeFactory typeFactory)
        {
            MaxLoopsPerProperty = 1000;
            IgnoredProperties = new List<string>();
            Guard.ArgumentNotNull(subject, "subject");
            Guard.ArgumentNotNull(typeFactory, "typeFactory");

            _subject = subject;
            _subjectType = _subject.GetType();
            _typeFactory = typeFactory;

            if (_subjectType.GetInterface(typeof (INotifyPropertyChanged).FullName) != null)
            {
                _iNotifyPropertyChanged = true;
                var inpc = (INotifyPropertyChanged) subject;
                inpc.PropertyChanged += inpc_PropertyChanged;
            }
        }

        /// <summary>
        /// When trying to create random values, how many attempts should the algorithm
        /// have at creating different values before erroring.
        /// </summary>
        public virtual int MaxLoopsPerProperty { get; set; }

        /// <summary>
        /// Gets a list of Property names to be ignored when this class is tested.
        /// </summary>
        public virtual List<string> IgnoredProperties { get; private set; }

        /// <summary>
        /// Add a property to ignore
        /// </summary>
        /// <typeparam name="TMember">The <see cref="Type"/> of the parameter to ignore.</typeparam>
        /// <param name="expression">The <see cref="Expression{TDelegate}"/> representing the property to ignore.</param>
        public void AddIgnoredProperty<TMember>(Expression<Func<TMember>> expression)
        {
            var propertyName = (((MemberExpression)expression.Body).Member).Name;
            IgnoredProperties.Add(propertyName);
        }
        /// <summary>
        /// Tests set (where the property is settable) and get (where the property is gettable) for
        /// all properties on the instance of the object used to construct this PropertyTester instance.
        /// If the instance implements INotifyPropertyChanged, the tester will also check to ensure that
        /// when the property is changed, it fires the appropriate event. Properties are changed by 
        /// generating two random values and setting twice.
        /// 
        /// Properties with non default types (such as interfaces) will be skipped. It is important to 
        /// utilise this test in conjunction with a code coverage tool to ensure the bits you think are
        /// being tested actually are.
        /// 
        /// The tester will try MaxLoopsPerProperty attempts at generating two different random values.
        /// If the class can't generate two random values (because it doesn't understand the type) then
        /// consider ignoring that problem property and testing it manually.
        /// </summary>
        public virtual void TestProperties()
        {
            var properties = _subjectType.GetProperties();

            foreach (var property in properties)
            {
                // we can only SET if the property is writable and we can new up the type
                var testSet = property.CanWrite;
                var testGet = property.CanRead;

                if (IgnoreProperty(property) || PropertyIsIndexed(property))
                {
                    // skip this property - we can't test indexers or properties
                    // we've been asked to ignore
                    continue;
                }
                object valueIn2 = null;

                // we can only set properties 
                if (testSet)
                {
                    if (!_typeFactory.CanCreateInstance(property.PropertyType))
                    {
                        throw new PropertyTestException(string.Format(
                            "Cannot generate type '{0}' to set on property '{1}'. Consider ignoring this property on the type '{2}'",
                            property.PropertyType,
                            property.Name,
                            _subjectType));
                    }
                    
                    // We need two 'in' values to ensure the property actually changes.
                    // because the values are random - we need to loop to make sure we
                    // get different ones (i.e. bool);
                    var valueIn1 = valueIn2 = _typeFactory.CreateRandomValue(property.PropertyType);

                    if (_iNotifyPropertyChanged)
                    {
                        // safety net 
                        var counter = 0;
                        while (valueIn2.Equals(valueIn1))
                        {
                            if (counter++ > MaxLoopsPerProperty)
                            {
                                throw new InvalidOperationException(string.Format(
                                    "Could not generate two different values for the type '{0}'. Consider ignoring the '{1}' property on the type '{3}' or increasing the MaxLoopsPerProperty value above {2}",
                                    property.PropertyType,
                                    property.Name,
                                    MaxLoopsPerProperty,
                                    _subjectType));
                            }
                            valueIn2 = _typeFactory.CreateRandomValue(property.PropertyType);
                        }
                    }

                    property.SetValue(_subject, valueIn1, null);
                    if (_iNotifyPropertyChanged)
                        property.SetValue(_subject, valueIn2, null);
                    
                    // This currently assumes single threaded execution - do we need to consider threads here?
                    if (_iNotifyPropertyChanged)
                    {
                        if (_lastPropertyChanged != property.Name)
                        {
                            throw new PropertyTestException(string.Format(
                               "The property '{0}' on the type '{1}' did not throw a PropertyChangedEvent",
                               property.Name,
                               _subjectType));
                        }
                        _lastPropertyChanged = null;
                    }
                }

                if (testGet)
                {
                    var valueOut = property.GetValue(_subject, null);

                    // if we can also write - we should test the value
                    // we written to the variable.
                    if (testSet)
                    {
                        if (!(Equals(valueIn2, valueOut)))
                        {
                            throw new PropertyTestException(string.Format(
                               "The get value of the '{0}' property on the type '{3}' did not equal the set value (in: '{2}', out: '{1}')",
                               property.Name,
                               valueOut,
                               valueIn2,
                               _subjectType));
                        }
                    }
                }
            }
        }

        private static bool PropertyIsIndexed(PropertyInfo property)
        {
            return property.GetIndexParameters().Length > 0;
        }

        private bool IgnoreProperty(PropertyInfo property)
        {
            return IgnoredProperties.Contains(property.Name);
        }



        private void inpc_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            _lastPropertyChanged = e.PropertyName;
        }
    }
}