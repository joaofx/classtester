namespace TheJoyOfCode.QualityTools.Tests.DummyProject.WithErrors.SubNamespaceNoErrors
{
    public class CorrectConstructors
    {
        private readonly string dummy1;
        private readonly int dummy2;
        private readonly bool dummy3;

        public CorrectConstructors(string dummy1, int dummy2, bool dummy3)
        {
            this.dummy1 = dummy1;
            this.dummy2 = dummy2;
            this.dummy3 = dummy3;
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