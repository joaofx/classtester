using System.ComponentModel;

namespace TheJoyOfCode.QualityTools.Tests.DummyProject.NoErrors
{
    public class CorrectProperties : INotifyPropertyChanged
    {
        private string dummy1;
        private int dummy2;
        private bool dummy3;

        public bool Dummy3
        {
            get { return dummy3; }
            set 
            { 
                dummy3 = value;
                OnNotifyPropertyChanged("Dummy3");
            }
        }

        public string Dummy1
        {
            get { return dummy1; }
            set
            {
                dummy1 = value;
                OnNotifyPropertyChanged("Dummy1");
            }
        }

        public int Dummy2
        {
            get { return dummy2; }
            set
            {
                dummy2 = value;
                OnNotifyPropertyChanged("Dummy2");
            }
        }

        private void OnNotifyPropertyChanged(string parameterName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(parameterName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}