namespace ArgumentParser.Api
{
    public interface IArgumentBuilder
    {
    }

    public interface IArgumentBuilder<out TBuilder> : IArgumentBuilder
    {
        TBuilder WithDescription(string description);
    }
}