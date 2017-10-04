using System;
using System.Reflection;
using System.Text.RegularExpressions;
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
        public bool HasLongName => !String.IsNullOrWhiteSpace(LongName);
        public bool HasShortName => ShortName != default(char);
        public bool HasDefault => DefaultValue != null;
        public MemberInfo MemberInfo { get; }

        internal void Validate()
        {
            if (HasShortName)
                ValidateName(ShortName);
            if (HasLongName)
                ValidateName(LongName);
        }

        internal static void ValidateName(char name)
        {
            ValidateName(name.ToString());
        }

        internal static void ValidateName(string name)
        {
            if (!Regex.IsMatch(name, $"^{Argument.NamePattern}$"))
                throw new InvalidOptionNameException(name);
        }
    }
}