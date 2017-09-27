using System;

namespace ArgumentParser
{
    public class OptionAlreadyExistsException : Exception
    {
        public object Key { get; }

        public OptionAlreadyExistsException(object key)
        {
            Key = key;
        }
    }
}
