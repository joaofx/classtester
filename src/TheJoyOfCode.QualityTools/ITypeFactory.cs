using System;
namespace TheJoyOfCode.QualityTools
{
    public interface ITypeFactory
    {
        bool CanCreateInstance(Type type);
        object CreateRandomValue(Type type);
    }
}
