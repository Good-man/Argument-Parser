using System;
using System.Linq.Expressions;

namespace ArgumentParser.Api
{
    public interface IFluentOptionBuilder<in TValue> : IFluentArgumentBuilder<IFluentOptionBuilder<TValue>>
    {
        IFluentOptionBuilder<TValue> As(string longName);
        IFluentOptionBuilder<TValue> As(string longName, char shortName);
        IFluentOptionBuilder<TValue> As(char shortName);
        IFluentOptionBuilder<TValue> As(char shortName, string longName);

        IFluentOptionBuilder<TValue> SetDefault(TValue value);
        IFluentOptionBuilder<TValue> IsRequired();
    }
}