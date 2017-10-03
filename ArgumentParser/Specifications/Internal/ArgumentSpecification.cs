using System;
using System.Reflection;
using ArgumentParser.Api;

namespace ArgumentParser.Internal
{
    internal abstract class ArgumentSpecification : IArgumentSpecification
    {
        protected ArgumentSpecification(MemberInfo memberInfo)
        {
            MemberInfo = memberInfo;
        }

        public string LongName { get; set; }

        public char ShortName { get; set; }

        public int Index { get; set; }

        public string Description { get; set; }
        public bool Required { get; set; }
        public object DefaultValue { get; set; }
        public virtual bool HasName => HasLongName | HasShortName;
        public bool HasLongName => !string.IsNullOrWhiteSpace(LongName);
        public bool HasShortName => ShortName != default(char);
        public bool HasDefault => DefaultValue != null;
        public MemberInfo MemberInfo { get; }
    }
}