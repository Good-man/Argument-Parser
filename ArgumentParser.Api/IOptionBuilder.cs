namespace ArgumentParser.Api
{
    public interface IOptionBuilder : IArgumentBuilder
    {
        IOptionSpecification OptionSpecification { get; }

        IOptionBuilder SetDefault(object value);
        IOptionBuilder WithDescription(string description);
    }
}