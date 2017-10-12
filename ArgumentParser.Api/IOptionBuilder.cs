namespace ArgumentParser.Api
{
    public interface IOptionBuilder : IArgumentBuilder<IOptionBuilder>
    {
        IOptionSpecification OptionSpecification { get; }

        IOptionBuilder SetDefault(object value);
    }
}