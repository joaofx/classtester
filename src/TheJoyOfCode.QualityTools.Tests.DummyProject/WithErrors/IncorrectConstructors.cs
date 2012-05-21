namespace TheJoyOfCode.QualityTools.Tests.DummyProject.WithErrors
{
    public class IncorrectConstructors
    {
        private readonly string dummy1;
        private readonly int dummy2;
        private readonly bool dummy3;
        private readonly string dummy4;

        public IncorrectConstructors(string dummy1)
        {
            // this is correct
            this.dummy1 = dummy1;
        }

        public IncorrectConstructors(string dummy1, string dummy4)
        {
            // switch dummy1 with dummy4
            this.dummy1 = dummy4;
            this.dummy4 = dummy1;
        }

        public IncorrectConstructors(string dummy1, int dummy2, bool dummy3, string dummy4)
        {
            // switch dummy1 with dummy4
            this.dummy1 = dummy4;
            this.dummy2 = dummy2;
            this.dummy3 = dummy3;
            this.dummy4 = dummy1;
        }

        public string Dummy4
        {
            get { return dummy4; }
        }

        public string Dummy1
        {
            get { return dummy1; }
        }

        public int Dummy2
        {
            get { return dummy2; }
        }

        public bool Dummy3
        {
            get { return dummy3; }
        }
    }
}