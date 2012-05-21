using System.ComponentModel;

namespace TheJoyOfCode.QualityTools.Tests.DummyProject.WithErrors
{
    public class IncorrectProperties : INotifyPropertyChanged
    {
        private string dummy1;
        private int dummy2;
        private bool dummy3;
        private string dummy4;

        public string Dummy4
        {
            get { return dummy4; }
            set
            {
                dummy1 = value; // Set the wrong field
                OnNotifyPropertyChanged("Dummy4");
            }
        }

        public bool Dummy3
        {
            get { return dummy3; }
            set 
            { 
                dummy3 = value;
                // No OnNotifyPropertyChanged
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
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(parameterName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}