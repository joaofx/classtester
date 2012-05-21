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

namespace TheJoyOfCode.QualityTools.Tests
{
    public class DummyGood : INotifyPropertyChanged
    {
        // Default public constructor
        public DummyGood()
        {
        }

        public DummyGood(string readOnly, string someString)
        {
            _readOnly = readOnly;
            _someString = someString;
        }

        public DummyGood(int someInt, int noMatchingProperty, int setOnly)
        {
            _someInt = someInt;
        }

        private int _setOnly;

        public int SetOnly
        {
            set
            {
                if (_setOnly == value)
                    return;
                _setOnly = value;
                FireChangedEvent("SetOnly");
            }
        }

        private Guid _guid;

        public Guid Guid
        {
            get { return _guid; }
            set
            {
                if (_guid == value)
                    return;
                _guid = value;
                FireChangedEvent("Guid");
            }
        }

        private bool _bool;

        public bool Bool
        {
            get { return _bool; }
            set
            {
                if (_bool == value)
                    return;
                _bool = value;
                FireChangedEvent("Bool");
            }
        }

        private char _char;

        public char Char
        {
            get { return _char; }
            set
            {
                if (_char == value)
                    return;
                _char = value;
                FireChangedEvent("Char");
            }
        }

        private byte _byte;

        public byte Byte
        {
            get { return _byte; }
            set
            {
                if (_byte == value)
                    return;
                _byte = value;
                FireChangedEvent("Byte");
            }
        }

        private Type _type;

        public Type Type
        {
            get { return _type; }
            set
            {
                if (_type == value)
                    return;
                _type = value;
                FireChangedEvent("Type");
            }
        }


        private UInt16 _uInt16;

        public UInt16 UInt16
        {
            get { return _uInt16; }
            set
            {
                if (_uInt16 == value)
                    return;
                _uInt16 = value;
                FireChangedEvent("UInt16");
            }
        }

        private UInt32 _uInt32;

        public UInt32 UInt32
        {
            get { return _uInt32; }
            set
            {
                if (_uInt32 == value)
                    return;
                _uInt32 = value;
                FireChangedEvent("UInt32");
            }
        }

        private UInt64 _uInt64;

        public UInt64 UInt64
        {
            get { return _uInt64; }
            set
            {
                if (_uInt64 == value)
                    return;
                _uInt64 = value;
                FireChangedEvent("UInt64");
            }
        }

        private Int16 _int16;

        public Int16 Int16
        {
            get { return _int16; }
            set
            {
                if (_int16 == value)
                    return;
                _int16 = value;
                FireChangedEvent("Int16");
            }
        }

        private Int32 _int32;

        public Int32 Int32
        {
            get { return _int32; }
            set
            {
                if (_int32 == value)
                    return;
                _int32 = value;
                FireChangedEvent("Int32");
            }
        }

        private Int64 _int64;

        public Int64 Int64
        {
            get { return _int64; }
            set
            {
                if (_int64 == value)
                    return;
                _int64 = value;
                FireChangedEvent("Int64");
            }
        }

        private Single _single;

        public Single Single
        {
            get { return _single; }
            set
            {
                if (_single == value)
                    return;
                _single = value;
                FireChangedEvent("Single");
            }
        }

        private double _double;

        public double Double
        {
            get { return _double; }
            set
            {
                if (_double == value)
                    return;
                _double = value;
                FireChangedEvent("Double");
            }
        }

        private object _object;

        public object Object
        {
            get { return _object; }
            set
            {
                if (_object == value)
                    return;
                _object = value;
                FireChangedEvent("Object");
            }
        }

        private sbyte _sbyte;

        public sbyte SByte
        {
            get { return _sbyte; }
            set
            {
                if (_sbyte == value)
                    return;
                _sbyte = value;
                FireChangedEvent("SByte");
            }
        }

        private int _someInt;

        public int SomeInt
        {
            get { return _someInt; }
            set
            {
                if (_someInt == value)
                    return;
                _someInt = value;
                FireChangedEvent("SomeInt");
            }
        }

        private string _someString;

        public string SomeString
        {
            get { return _someString; }
            set
            {
                if (_someString == value)
                    return;
                _someString = value;
                FireChangedEvent("SomeString");
            }
        }

        private DateTime _someDateTime;

        public DateTime SomeDateTime
        {
            get { return _someDateTime; }
            set
            {
                if (_someDateTime == value)
                    return;
                _someDateTime = value;
                FireChangedEvent("SomeDateTime");
            }
        }

        public string this[string key]
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        private decimal _decimal;

        public decimal Decimal
        {
            get { return _decimal; }
            set
            {
                if (_decimal == value)
                    return;
                _decimal = value;
                FireChangedEvent("Decimal");
            }
        }

        private decimal? _nullableDecimal;

        public decimal? NullableDecimal
        {
            get { return _nullableDecimal; }
            set
            {
                if (_nullableDecimal == value)
                    return;
                _nullableDecimal = value;
                FireChangedEvent("NullableDecimal");
            }
        }

        private ISomeInterface[] _arrayOfInterfaces;

        public ISomeInterface[] ArrayOfInterfaces
        {
            get { return _arrayOfInterfaces; }
            set
            {
                if (_arrayOfInterfaces == value)
                    return;
                _arrayOfInterfaces = value;
                FireChangedEvent("ArrayOfInterfaces");
            }
        }

        private TimeSpan _timeSpan;

        public TimeSpan TimeSpan
        {
            get { return _timeSpan; }
            set
            {
                if (_timeSpan == value)
                    return;
                _timeSpan = value;
                FireChangedEvent("TimeSpan");
            }
        }

        private List<string> _closedGenericList;

        public List<string> ClosedGenericList
        {
            get { return _closedGenericList; }
            set
            {
                if (_closedGenericList == value)
                    return;
                _closedGenericList = value;
                FireChangedEvent("ClosedGenericList");
            }
        }

        private DateTime? _nullableDateTime;

        public DateTime? NullableDateTime
        {
            get { return _nullableDateTime; }
            set
            {
                if (_nullableDateTime == value)
                    return;
                _nullableDateTime = value;
                FireChangedEvent("NullableDateTime");
            }
        }

        private MyEnum _myEnum;

        public MyEnum MyEnum
        {
            get { return _myEnum; }
            set
            {
                if (_myEnum == value)
                    return;
                _myEnum = value;
                FireChangedEvent("MyEnum");
            }
        }

        private MyEnum? _nullableMyEnum;

        public MyEnum? NullableMyEnum
        {
            get { return _nullableMyEnum; }
            set
            {
                if (_nullableMyEnum == value)
                    return;
                _nullableMyEnum = value;
                FireChangedEvent("NullableMyEnum");
            }
        }

        private string _readOnly;

        public string ReadOnly
        {
            get { return _readOnly; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void FireChangedEvent(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public enum MyEnum
    {
        One,
        Two,
        Three,
        Four,
        Five
    }
}