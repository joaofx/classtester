namespace TheJoyOfCode.QualityTools.Tests.DummyProject.WithErrors
{
    public class GenericClass<T>
    {
        private T genericProperty;

        public GenericClass(T genericProperty)
        {
        }

        public GenericClass()
        {
        }

        public T GenericProperty
        {
            get { return genericProperty; }
            set { genericProperty = value; }
        }
    }
}