namespace ArgumentParser.Internal
{
    internal class ParserResult<TOptions> : IParserResult<TOptions>
    {
        public ParserResult(TOptions parsedOptions)
        {
            Options = parsedOptions;
        }

        public TOptions Options { get; }
    }
}