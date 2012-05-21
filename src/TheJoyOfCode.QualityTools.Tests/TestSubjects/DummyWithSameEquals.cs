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
    public class DummyUsesSameEquals : INotifyPropertyChanged
    {
        private DummyWithSameEquals _dummyWithSameEquals;

        public DummyWithSameEquals DummyWithSameEquals
        {
            get { return _dummyWithSameEquals; }
            set { _dummyWithSameEquals = value; }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }

    // This class always equals another object. Total nonsense but should prove an exception.
    public class DummyWithSameEquals
    {
        public override bool Equals(object obj)
        {
            return true;
        }
    }
}