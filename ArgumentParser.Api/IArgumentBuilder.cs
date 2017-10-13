namespace ArgumentParser.Api
{
    public interface IArgumentBuilder
    {
        IArgumentSpecification OptionSpecification { get; }
        IArgumentBuilder SetDefault(object value);
        IArgumentBuilder WithDescription(string description);
    }
}