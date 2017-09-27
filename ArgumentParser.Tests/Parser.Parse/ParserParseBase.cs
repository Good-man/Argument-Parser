namespace ArgumentParser
{
    public class ParserParseBase<TOptions> where TOptions : new()
    {
        protected Parser<TOptions> Parser;
    }
}