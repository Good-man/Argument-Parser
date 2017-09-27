namespace ArgumentParser
{
    public class HelpStyle
    {
        public static HelpStyle GnuStyle { get; } = new HelpStyle();
        public static HelpStyle UnixStyle { get; } = new HelpStyle();
        public static HelpStyle WindowsStyle { get; } = new HelpStyle();
    }
}