using System;
using System.Collections.Generic;
using System.Reflection;
using ArgumentParser.Api;

namespace ArgumentParser.Internal
{
    internal class EnumSpecification<TValue> : ArgumentSpecification, IFluentEnumBuilder<TValue>
    {
        private readonly Dictionary<object, TValue> _mappings = new Dictionary<object, TValue>();

        internal EnumSpecification(MemberInfo memberInfo) : base(memberInfo)
        {
        }

        IFluentEnumBuilder<TValue> IFluentEnumBuilder<TValue>.IsRequired()
        {
            Required = true;
            return this;
        }

        IFluentEnumBuilder<TValue> IFluentEnumBuilder<TValue>.Map(string longName, TValue toValue)
        {
            _mappings[longName] = toValue;
            return this;
        }

        IFluentEnumBuilder<TValue> IFluentEnumBuilder<TValue>.Map(char shortName, TValue toValue)
        {
            _mappings[shortName] = toValue;
            return this;
        }

        IFluentEnumBuilder<TValue> IFluentEnumBuilder<TValue>.SetDefault(TValue value)
        {
            DefaultValue = value;
            return this;
        }

        IFluentEnumBuilder<TValue> IFluentArgumentBuilder<IFluentEnumBuilder<TValue>>.WithDescription(string description)
        {
            Description = description;
            return this;
        }

        public override string ToString()
        {
            return
                $"{{ Enum = ?, Description = {Description}, Required = {Required}, Default = {DefaultValue} }}";
        }
    }
}