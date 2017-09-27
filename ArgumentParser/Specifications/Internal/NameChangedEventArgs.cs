using System;
using ArgumentParser.Api;

namespace ArgumentParser.Internal
{
    internal class NameChangedEventArgs : EventArgs
    {
        public object Key { get; }
        public IArgumentSpecification ArgumentSpecification { get; }

        public NameChangedEventArgs(object key, IArgumentSpecification argumentSpecification)
        {
            Key = key;
            ArgumentSpecification = argumentSpecification;
        }
    }
}