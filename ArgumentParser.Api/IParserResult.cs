namespace ArgumentParser
{
    public interface IParserResult<out TOptions>
    {
        TOptions Options { get; }
    }
}
