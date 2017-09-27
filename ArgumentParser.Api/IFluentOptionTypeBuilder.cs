using System;

namespace ArgumentParser.Api
{
    public interface IFluentOptionTypeBuilder <in TValue>
    {
        IFluentOptionBuilder<TValue> As(string longName);
        IFluentOptionBuilder<TValue> As(string longName, char shortName);
        IFluentOptionBuilder<TValue> As(char shortName);
        IFluentOptionBuilder<TValue> As(char shortName, string longName);
        IFluentOptionBuilder<TValue> WithoutName(int index);
    }
}