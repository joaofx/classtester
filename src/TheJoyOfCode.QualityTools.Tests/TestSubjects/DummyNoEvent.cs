/********************************************************************************
 *
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND,
 * EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
 *
 ********************************************************************************/

using System.ComponentModel;

namespace TheJoyOfCode.QualityTools.Tests
{
    public class DummyNoEvent : INotifyPropertyChanged
    {
        private int _someInt;

        public int SomeInt
        {
            get { return _someInt; }
            set
            {
                _someInt = value;
                // look, no event
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}