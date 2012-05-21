namespace TheJoyOfCode.QualityTools.Tests.TestSubjects
{
    public interface IBasicProperties
    {
        string GetSetProperty { get; set; }
        string GetOnlyProperty { get; }
        string SetOnlyProperty { set; }
    }
}
