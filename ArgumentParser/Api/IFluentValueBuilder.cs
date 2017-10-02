using System;

namespace ArgumentParser.Api
{
    public interface IFluentValueBuilder<in TValue> : IFluentArgumentBuilder<IFluentValueBuilder<TValue>>
    {
        IFluentValueBuilder<TValue> WithoutName(int index);
        IFluentValueBuilder<TValue> SetDefault(TValue value);
        IFluentValueBuilder<TValue> IsRequired();
    }
}