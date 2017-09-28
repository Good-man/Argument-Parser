using System;
using System.Linq.Expressions;

namespace ArgumentParser.Api
{
    public interface IFluentSyntaxBuilder<TOptions>
    {
        IFluentCommandBuilder Setup(string name);
        IFluentOptionTypeBuilder<TValue> Setup<TValue>(Expression<Func<TOptions, TValue>> selector);
    }
}