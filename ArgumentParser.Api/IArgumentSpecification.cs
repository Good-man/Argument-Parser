using System.Reflection;

namespace ArgumentParser.Api
{
    public interface IArgumentSpecification
    {
        string Description { get; }
        bool Required { get; }
        object DefaultValue { get; }
        bool HasDefault { get; }
        MemberInfo MemberInfo { get; }        
    }
}