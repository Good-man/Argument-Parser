using System.Collections.Generic;

namespace ArgumentParser.Api
{
    public interface IParser<out TOptions>
    {
        IParserResult<TOptions> Parse(IEnumerable<string> args);
    }
}