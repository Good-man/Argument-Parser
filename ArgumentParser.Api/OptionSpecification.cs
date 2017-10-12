using System.Reflection;

namespace ArgumentParser.Api
{
    public class OptionSpecification : IOptionSpecification
    {
        public string LongName { get; }
        public char ShortName { get; }
        public int Index { get; }
        public string Description { get; }
        public bool Required { get; }
        public object DefaultValue { get; }
        public bool HasDefault { get; }
        public MemberInfo MemberInfo { get; }
        public bool HasIndex { get; }
        public bool HasName { get; }
        public bool HasLongName { get; }
        public bool HasShortName { get; }
    }
}