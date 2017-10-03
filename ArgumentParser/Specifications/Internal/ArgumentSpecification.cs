using System;
using System.Reflection;
using ArgumentParser.Api;

namespace ArgumentParser.Internal
{
    internal abstract class ArgumentSpecification : IArgumentSpecification
    {
        private string _longName;
        private char _shortName;

        protected ArgumentSpecification(MemberInfo memberInfo)
        {
            MemberInfo = memberInfo;
        }

        public string LongName
        {
            get => _longName;
            set
            {
                var keyChangingEventArgs = new KeyChangingEventArgs(value);
                OnKeyChanging(keyChangingEventArgs);
                if (!keyChangingEventArgs.Cancel)
                {
                    _longName = value;
                    OnKeyChanged(new KeyChangedEventArgs(_longName, this));
                }
                else if (keyChangingEventArgs.Exception != null)
                {
                    throw keyChangingEventArgs.Exception;
                }
            }
        }

        public char ShortName
        {
            get => _shortName;
            set
            {
                var keyChangingEventArgs = new KeyChangingEventArgs(value);
                OnKeyChanging(keyChangingEventArgs);
                if (!keyChangingEventArgs.Cancel)
                {
                    _shortName = value;
                    OnKeyChanged(new KeyChangedEventArgs(_shortName, this));
                }
                else if (keyChangingEventArgs.Exception != null)
                {
                    throw keyChangingEventArgs.Exception;
                }
            }
        }

        public int Index { get; set; }

        public string Description { get; set; }
        public bool Required { get; set; }
        public object DefaultValue { get; set; }
        public virtual bool HasName => HasLongName | HasShortName;
        public bool HasLongName => !string.IsNullOrWhiteSpace(LongName);
        public bool HasShortName => ShortName != default(char);
        public bool HasDefault => DefaultValue != null;
        public MemberInfo MemberInfo { get; }

        public event EventHandler<KeyChangingEventArgs> KeyChanging;
        public event EventHandler<KeyChangedEventArgs> KeyChanged;
        
        private void OnKeyChanging(KeyChangingEventArgs keyChangingEventArgs)
        {
            KeyChanging?.Invoke(this, keyChangingEventArgs);
        }

        private void OnKeyChanged(KeyChangedEventArgs keyChangedEventArgs)
        {
            KeyChanged?.Invoke(this, keyChangedEventArgs);
        }
    }
}