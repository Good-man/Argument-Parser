using System;
using ArgumentParser.Api;
using ArgumentParser.Internal;

namespace ArgumentParser
{
    public class HelpText<TOptions>
        where TOptions : new()
    {
        private readonly SyntaxBuilder<TOptions> _syntaxBuilder;

        public HelpText()
        {
            _syntaxBuilder = new SyntaxBuilder<TOptions>();
        }

        public HelpText(Action<HelpSettings> configure) : this()
        {
            configure(HelpSettings.Current);
        }

        public IFluentSyntaxBuilder<TOptions> Configure()
        {
            return _syntaxBuilder;
        }

        public void DisplayHelp()
        {
            var argumentSpecifications = _syntaxBuilder.GetSpecifications();
            var helpText = new HelpTextInternal(argumentSpecifications);
            helpText.DisplayHelp();
        }
    }
}