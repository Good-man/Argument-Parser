using System.Reflection;
using ArgumentParser.Api;

namespace ArgumentParser.Internal
{
    internal class OptionSpecification<TValue> : ArgumentSpecification, IFluentOptionBuilder<TValue>
    {
        internal OptionSpecification(MemberInfo memberInfo) : base(memberInfo)
        {
        }

        IFluentOptionBuilder<TValue> IFluentOptionBuilder<TValue>.IsRequired()
        {
            Required = true;
            return this;
        }

        IFluentOptionBuilder<TValue> IFluentOptionBuilder<TValue>.As(string longName)
        {
            ValidateName(longName);
            LongName = longName;
            return this;
        }

        IFluentOptionBuilder<TValue> IFluentOptionBuilder<TValue>.As(string longName, char shortName)
        {
            var typeBuilder = (IFluentOptionBuilder<TValue>)this;
            typeBuilder.As(longName);
            typeBuilder.As(shortName);
            return this;
        }

        IFluentOptionBuilder<TValue> IFluentOptionBuilder<TValue>.As(char shortName)
        {
            ValidateName(shortName);
            ShortName = shortName;
            return this;
        }

        IFluentOptionBuilder<TValue> IFluentOptionBuilder<TValue>.As(char shortName, string longName)
        {
            var typeBuilder = (IFluentOptionBuilder<TValue>)this;
            typeBuilder.As(shortName);
            typeBuilder.As(longName);
            return this;
        }

        IFluentOptionBuilder<TValue> IFluentOptionBuilder<TValue>.SetDefault(TValue value)
        {
            DefaultValue = value;
            return this;
        }

        IFluentOptionBuilder<TValue> IFluentArgumentBuilder<IFluentOptionBuilder<TValue>>.WithDescription(string description)
        {
            Description = description;
            return this;
        }

        public override string ToString()
        {
            return
                $"{{ Name = {LongName}, Description = {Description}, Required = {Required}, Default = {DefaultValue} }}";
        }
    }
}