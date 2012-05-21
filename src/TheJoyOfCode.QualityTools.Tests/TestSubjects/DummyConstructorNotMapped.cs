/********************************************************************************
 *
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,
 * EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
 *
 ********************************************************************************/

using System;

namespace TheJoyOfCode.QualityTools.Tests
{
    internal class DummyConstructorNotMapped
    {
        public DummyConstructorNotMapped(string myString, Type type)
        {
            _myString = myString + " ";
        }

        private string _myString;

        public string MyString
        {
            get { return _myString; }
            set { _myString = value; }
        }
    }
}