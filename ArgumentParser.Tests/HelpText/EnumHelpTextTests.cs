using System.Linq;
using ArgumentParser.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArgumentParser
{
    [TestClass]
    [TestCategory("HelpText.DisplayHelp")]
    public class EnumHelpTextTests
    {
        [TestMethod]
        public void Enum_DisplayHelp_DefaultStyle()
        {
            var argumentSpecifications = new ArgumentSpecifications<Options>();
            argumentSpecifications.SetupValue(o => o.MyEnum)
                .As(0)
                .SetDefault(MyEnum.OptionA)
                .WithDescription("Enum option description");
            var helpText = new HelpTextInternal(argumentSpecifications.GetSpecifications());
            var argumentHelpTexts = helpText.GetNamedOptionHelpText().ToArray();
            Assert.AreEqual(2, argumentHelpTexts.Length);
        }

        public class Options
        {
            public MyEnum MyEnum { get; set; }
        }

        public enum MyEnum
        {
            OptionA,
            OptionB
        }
    }
}