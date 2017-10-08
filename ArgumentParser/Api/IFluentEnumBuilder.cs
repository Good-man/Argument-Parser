using System;

namespace ArgumentParser.Api
{
    public interface IFluentEnumBuilder<in TValue> : IFluentArgumentBuilder<IFluentEnumBuilder<TValue>>
    {
        IFluentEnumBuilder<TValue> Map(string longName, TValue toValue);
        IFluentEnumBuilder<TValue> Map(char shortName, TValue toValue);
    }
}