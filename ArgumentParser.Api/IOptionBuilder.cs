namespace ArgumentParser.Api
{
    public interface IOptionBuilder<TValue> : IArgumentBuilder<IOptionBuilder<TValue>>
    {
        IOptionSpecification<TValue> OptionSpecification { get; }

        IOptionBuilder<TValue> SetDefault(TValue value);
    }
}