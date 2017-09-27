using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ArgumentParser.Api;

namespace ArgumentParser.Internal
{
    internal class OptionSpecification<TValue> : ArgumentSpecification, IFluentOptionBuilder<TValue>, IFluentOptionTypeBuilder<TValue>
    {
        internal OptionSpecification(MemberInfo memberInfo) : base(memberInfo)
        {
        }

        IFluentOptionBuilder<TValue> IFluentOptionBuilder<TValue>.IsRequired()
        {
            Required = true;
            return this;
        }

        IFluentOptionBuilder<TValue> IFluentOptionTypeBuilder<TValue>.As(string longName)
        {
            LongName = longName;
            return this;
        }

        IFluentOptionBuilder<TValue> IFluentOptionTypeBuilder<TValue>.As(string longName, char shortName)
        {
            var typeBuilder = (IFluentOptionTypeBuilder<TValue>)this;
            typeBuilder.As(longName);
            typeBuilder.As(shortName);
            return this;
        }

        IFluentOptionBuilder<TValue> IFluentOptionTypeBuilder<TValue>.As(char shortName)
        {
            ShortName = shortName;
            return this;
        }

        IFluentOptionBuilder<TValue> IFluentOptionTypeBuilder<TValue>.As(char shortName, string longName)
        {
            var typeBuilder = (IFluentOptionTypeBuilder<TValue>)this;
            typeBuilder.As(shortName);
            typeBuilder.As(longName);
            return this;
        }

        IFluentOptionBuilder<TValue> IFluentOptionTypeBuilder<TValue>.WithoutName(int index)
        {
            Index = index;
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

        private readonly Dictionary<object, TValue> _enumOptions = new Dictionary<object, TValue>();

        public override string ToString()
        {
            return
                $"{{ Name = {LongName}, Description = {Description}, Required = {Required}, Default = {DefaultValue} }}";
        }
    }
}