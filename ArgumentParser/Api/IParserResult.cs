using System;

namespace ArgumentParser.Api
{
    public interface IParserResult<out TOptions>
    {
        TOptions Options { get; }
    }
}
