using System.Text;

namespace ArgumentParser.Internal
{
    internal class ArgumentHelpText
    {
        private ArgumentHelpText(string arguments, string helpText)
        {
            Arguments = arguments;
            HelpText = helpText;
        }

        internal string Arguments { get; }
        internal string HelpText { get; }

        internal static ArgumentHelpText Create(ArgumentSpecification argumentSpecification)
        {
            var sb = new StringBuilder();
            var helpSettings = HelpSettings.Current;
            if (helpSettings.HelpStyle == HelpStyle.WindowsStyle)
                if (argumentSpecification.HasName)
                    sb.Append($"{OptionPrefix.ForwardSlash}{argumentSpecification.ShortName}");
            if (helpSettings.HelpStyle == HelpStyle.UnixStyle)
                sb.Append($"{OptionPrefix.SingleHyphen}{argumentSpecification.ShortName}");
            if (helpSettings.HelpStyle == HelpStyle.GnuStyle)
            {
                if (argumentSpecification.HasLongName)
                {
                    sb.Append($"{OptionPrefix.DoubleHyphen}{argumentSpecification.LongName}");
                    if (argumentSpecification.HasShortName) sb.Append(", ");
                }
                if (argumentSpecification.HasShortName)
                    sb.Append($"{OptionPrefix.SingleHyphen}{argumentSpecification.ShortName}");
            }

            return new ArgumentHelpText(sb.ToString(), argumentSpecification.Description);
        }
    }
}