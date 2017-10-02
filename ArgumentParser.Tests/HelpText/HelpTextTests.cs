using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArgumentParser
{
    [TestClass]
    [TestCategory("HelpText.DisplayHelp")]
    public class HelpTextTests
    {
        [TestMethod]
        public void DisplayHelp_DefaultStyle()
        {
            var helpText = new HelpText<Options>();
            helpText.Configure().SetupCommand("command");
            helpText.Configure().SetupOption(o => o.Option1).As('o', "option").WithDescription("Option description");
            helpText.Configure().SetupValue(o => o.Value1).WithoutName(0).WithDescription("Value 1 description");
            helpText.Configure().SetupValue(o => o.Value2).WithoutName(1).WithDescription("Value 2 description");
            helpText.DisplayHelp();
        }

        [TestMethod]
        public void DisplayHelp_WindowsStyle()
        {
            var helpText = new HelpText<Options>(with => with.HelpStyle = HelpStyle.WindowsStyle);
            helpText.Configure().SetupCommand("command");
            helpText.Configure().SetupOption(o => o.Option1).As('o', "option").WithDescription("Option description");
            helpText.Configure().SetupValue(o => o.Value1).WithoutName(0).WithDescription("Value 1 description");
            helpText.Configure().SetupValue(o => o.Value2).WithoutName(1).WithDescription("Value 2 description");
            helpText.DisplayHelp();
        }

        [TestMethod]
        public void DisplayHelp_UnixStyle()
        {
            var helpText = new HelpText<Options>(with => with.HelpStyle = HelpStyle.UnixStyle);
            helpText.Configure().SetupCommand("command");
            helpText.Configure().SetupOption(o => o.Option1).As('o', "option").WithDescription("Option description");
            helpText.Configure().SetupValue(o => o.Value1).WithoutName(0).WithDescription("Value 1 description");
            helpText.Configure().SetupValue(o => o.Value2).WithoutName(1).WithDescription("Value 2 description");
            helpText.DisplayHelp();
        }

        public class Options
        {
            public string Option1 { get; set; }
            public string Option2 { get; set; }
            public string Value1 { get; set; }
            public string Value2 { get; set; }
            public MyEnum MyEnum { get; set; }
        }

        public enum MyEnum
        {
            OptionA,
            OptionB
        }
    }
}
