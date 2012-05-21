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
    public class DummyGeneric<T, U, V, W> : INotifyPropertyChanged
    {
        private T _myT;
        private U _myU;
        private V _myV;
        private W _myW;

        public T MyT
        {
            get { return _myT; }
            set
            {
                _myT = value;
                FirePropertyChanged("MyT");
            }
        }

        public U MyU
        {
            get { return _myU; }
            set
            {
                _myU = value;
                FirePropertyChanged("MyU");
            }
        }

        public V MyV
        {
            get { return _myV; }
            set
            {
                _myV = value;
                FirePropertyChanged("MyV");
            }
        }

        public W MyW
        {
            get { return _myW; }
            set
            {
                _myW = value;
                FirePropertyChanged("MyW");
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public void FirePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}