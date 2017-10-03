using System;
using ArgumentParser.Api;

namespace ArgumentParser.Internal
{
    internal class KeyChangedEventArgs : EventArgs
    {
        public object Key { get; }
        public IArgumentSpecification ArgumentSpecification { get; }

        public KeyChangedEventArgs(object key, IArgumentSpecification argumentSpecification)
        {
            Key = key;
            ArgumentSpecification = argumentSpecification;
        }
    }
}