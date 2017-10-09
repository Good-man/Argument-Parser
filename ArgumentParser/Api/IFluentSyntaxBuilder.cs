using System;
using System.Linq.Expressions;

namespace ArgumentParser.Api
{
    public interface IFluentSyntaxBuilder<TOptions>
    {
        IFluentCommandBuilder SetupCommand(string name);
        IFluentOptionBuilder<TValue> SetupOption<TValue>(Expression<Func<TOptions, TValue>> selector);
        IFluentValueBuilder<TValue> SetupValue<TValue>(Expression<Func<TOptions, TValue>> selector);
        IFluentEnumBuilder<TValue> SetupEnum<TValue>(Expression<Func<TOptions, TValue>> selector);

        void Validate();
    }
}