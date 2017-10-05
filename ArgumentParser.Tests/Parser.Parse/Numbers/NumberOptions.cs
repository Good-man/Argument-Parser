using System;

namespace ArgumentParser.Numbers
{
    public class NumberOptions<TNumber> where TNumber : IComparable
    {
        public TNumber Value { get; internal set; }
    }
}