/********************************************************************************
 *
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,
 * EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
 *
 ********************************************************************************/

namespace TheJoyOfCode.QualityTools.Tests
{
    public class DummyBadProperty
    {
        private string _someString;

        public string SomeString
        {
            get { return _someString + " "; }
            set { _someString = value; }
        }
    }
}