using System.Collections.Generic;
using System.Linq;
using ArgumentParser.Api;
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
            var syntaxBuilder = new ArgumentSpecifications<Options>() as IFluentSyntaxBuilder<Options>;
            syntaxBuilder.SetupValue(o => o.MyEnum)
                .As(0)
                .SetDefault(MyEnum.OptionA)
                .WithDescription("Enum option description");
            var helpText = new HelpTextInternal((IEnumerable<IArgumentSpecification>) syntaxBuilder);
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