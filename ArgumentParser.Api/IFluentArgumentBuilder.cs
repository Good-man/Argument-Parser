namespace ArgumentParser.Api
{
    public interface IFluentArgumentBuilder<out TBuilder>
    {
        TBuilder WithDescription(string description);
    }
}