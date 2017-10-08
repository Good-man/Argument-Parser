using System;
using ArgumentParser.Api;
using ArgumentParser.Internal;

namespace ArgumentParser
{
    public class HelpText<TOptions>
        where TOptions : new()
    {
        private readonly ArgumentSpecifications<TOptions> _argumentSpecifications;

        public HelpText()
        {
            _argumentSpecifications = new ArgumentSpecifications<TOptions>();
        }

        public HelpText(Action<HelpSettings> configure) : this()
        {
            configure(HelpSettings.Current);
        }

        public IFluentSyntaxBuilder<TOptions> Configure()
        {
            return _argumentSpecifications;
        }

        public void DisplayHelp()
        {
            var helpText = new HelpTextInternal(_argumentSpecifications);
            helpText.DisplayHelp();
        }
    }
}