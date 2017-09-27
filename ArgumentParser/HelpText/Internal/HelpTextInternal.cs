using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using ArgumentParser.Api;

namespace ArgumentParser.Internal
{
    internal class HelpTextInternal
    {
        private readonly IEnumerable<IArgumentSpecification> _argumentSpecifications;

        public HelpTextInternal(IEnumerable<IArgumentSpecification> argumentSpecifications,
            Action<HelpSettings> configure) : this(argumentSpecifications)
        {
            configure(HelpSettings.Current);
        }

        public HelpTextInternal(IEnumerable<IArgumentSpecification> argumentSpecifications)
        {
            _argumentSpecifications = argumentSpecifications;
        }

        public void DisplayHelp()
        {
            var writer = HelpSettings.Current.HelpWriter;

            writer.WriteLine(Process.GetCurrentProcess().ProcessName);
            writer.WriteLine(Assembly.GetExecutingAssembly().GetName().Version.ToString());
            writer.WriteLine();

            var helpText = new List<ArgumentHelpText>();
            helpText.AddRange(GetCommandHelpText());
            helpText.AddRange(GetNamedOptionHelpText());
            helpText.AddRange(GetUnNamedOptionHelpText());

            var max = helpText.Max(t => t.Arguments.Length);
            helpText.ForEach(t =>
            {
                var format = $"   {{0,-{max + 4}}}\t{{1}}";
                writer.WriteLine(format, t.Arguments, t.HelpText);
                writer.WriteLine();
            });
        }

        internal IEnumerable<ArgumentHelpText> GetCommandHelpText()
        {
            var commandSpecifications = _argumentSpecifications.OfType<CommandSpecification>();
            var helpText = commandSpecifications.Select(ArgumentHelpText.Create);
            return helpText;
        }

        internal IEnumerable<ArgumentHelpText> GetNamedOptionHelpText()
        {
            var optionSpecifications = _argumentSpecifications.OfType<ArgumentSpecification>().Where(o => o.HasName);
            var helpText = optionSpecifications.Select(ArgumentHelpText.Create);
            return helpText;
        }

        internal IEnumerable<ArgumentHelpText> GetUnNamedOptionHelpText()
        {
            var valueSpecifications = _argumentSpecifications.OfType<ArgumentSpecification>().Where(o => !o.HasName);
            var helpText = valueSpecifications.Select(ArgumentHelpText.Create);
            return helpText;
        }
    }
}