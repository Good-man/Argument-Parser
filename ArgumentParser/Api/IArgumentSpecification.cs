using System.Reflection;

namespace ArgumentParser.Api
{
    public interface IArgumentSpecification
    {
        string LongName { get; }
        char ShortName { get; }
        int Index { get; }

        string Description { get; }
        bool Required { get; }
        object DefaultValue { get; }
        bool HasDefault { get; }
        MemberInfo MemberInfo { get; }
        bool HasName { get; }
        bool HasLongName { get; }
        bool HasShortName { get; }
    }
}