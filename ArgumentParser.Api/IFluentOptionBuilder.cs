using System;
using System.Linq.Expressions;

namespace ArgumentParser.Api
{
    public interface IFluentOptionBuilder<in TValue> : IFluentArgumentBuilder<IFluentOptionBuilder<TValue>>
    {
        IFluentOptionBuilder<TValue> SetDefault(TValue value);
        IFluentOptionBuilder<TValue> IsRequired();
    }
}