namespace TheJoyOfCode.QualityTools.Tests
{
    public class DummyCtorAbstractParams
    {
        public DummyCtorAbstractParams(string s, object o)
        {
        }

        public DummyCtorAbstractParams(string s, ISomeInterface someInstance, object o)
        {
        }
    }

    public interface ISomeInterface
    {
        void DoSomething();
    }
}