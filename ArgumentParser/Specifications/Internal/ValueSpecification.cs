using System;
using System.Reflection;
using ArgumentParser.Api;

namespace ArgumentParser.Internal
{
    internal class ValueSpecification<TValue> : ArgumentSpecification, IFluentValueBuilder<TValue>
    {
        internal ValueSpecification(MemberInfo memberInfo) : base(memberInfo)
        {
        }

        IFluentValueBuilder<TValue> IFluentValueBuilder<TValue>.IsRequired()
        {
            Required = true;
            return this;
        }

        IFluentValueBuilder<TValue> IFluentValueBuilder<TValue>.As(int index)
        {
            Index = index;
            return this;
        }

        IFluentValueBuilder<TValue> IFluentValueBuilder<TValue>.SetDefault(TValue value)
        {
            DefaultValue = value;
            return this;
        }

        IFluentValueBuilder<TValue> IFluentArgumentBuilder<IFluentValueBuilder<TValue>>.WithDescription(string description)
        {
            Description = description;
            return this;
        }

        public override string ToString()
        {
            return
                $"{{ Index = {Index}, Description = {Description}, Required = {Required}, Default = {DefaultValue} }}";
        }
    }
}