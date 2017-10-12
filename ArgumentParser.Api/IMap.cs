using System;
using System.Linq.Expressions;

namespace ArgumentParser.Api
{
    public interface IMap<TOptions> 
    {
        IOptionBuilder To<TValue>(Expression<Func<TOptions, TValue>> selector);
    }
}