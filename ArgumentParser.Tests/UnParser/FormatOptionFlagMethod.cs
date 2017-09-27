using ArgumentParser.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArgumentParser
{
    [TestClass]
    [TestCategory("UnParser.FormatOptionFlag")]
    public class FormatOptionFlagMethod : UnParserTestBase
    {
        [TestMethod]
        public void FormatNoNameOption()
        {
            var unParser = new UnParser(with => with.OptionPrefix = OptionPrefix.DoubleHyphen);
            var optionFlag = unParser.FormatOptionFlag(new OptionSpecification<object>(null));
            Assert.AreEqual(string.Empty, optionFlag);
        }

        [TestMethod]
        public void FormatPreferredDoubleHyphenOption()
        {
            var unParser = new UnParser(with => with.OptionPrefix = OptionPrefix.DoubleHyphen);
            var optionFlag = unParser.FormatOptionFlag(new OptionSpecification<object>(null) { ShortName = 's', LongName = "option" });
            Assert.AreEqual("--option", optionFlag);
        }

        [TestMethod]
        public void FormatPreferredSingleHyphenOption()
        {
            var unParser = new UnParser(with => with.OptionPrefix = OptionPrefix.SingleHyphen);
            var optionFlag = unParser.FormatOptionFlag(new OptionSpecification<object>(null) { ShortName = 's', LongName = "option" });
            Assert.AreEqual("-s", optionFlag);
        }

        [TestMethod]
        public void FormatPreferredForwardSlashOption()
        {
            var unParser = new UnParser(with => with.OptionPrefix = OptionPrefix.ForwardSlash);
            var optionFlag = unParser.FormatOptionFlag(new OptionSpecification<object>(null) { ShortName = 's', LongName = "option" });
            Assert.AreEqual("/s", optionFlag);
        }

        [TestMethod]
        public void FormatLongOption()
        {
            var unParser = new UnParser();
            var optionFlag = unParser.FormatOptionFlag(new OptionSpecification<object>(null) { LongName = "option" });
            Assert.AreEqual("--option", optionFlag);
        }

        [TestMethod]
        public void FormatShortWithDoubleHyphenOption()
        {
            var unParser = new UnParser(with => with.OptionPrefix = OptionPrefix.DoubleHyphen);
            var optionFlag = unParser.FormatOptionFlag(new OptionSpecification<object>(null) { ShortName = 's' });
            Assert.AreEqual("-s", optionFlag);
        }

        [TestMethod]
        public void FormatShortSingleHypenOption()
        {
            var unParser = new UnParser();
            var optionFlag = unParser.FormatOptionFlag(new OptionSpecification<object>(null) { ShortName = 's' });
            Assert.AreEqual("-s", optionFlag);
        }

        [TestMethod]
        public void FormatShortForwardSlashOption()
        {
            var unParser = new UnParser(with => with.OptionPrefix = OptionPrefix.ForwardSlash);
            var optionFlag = unParser.FormatOptionFlag(new OptionSpecification<object>(null) { ShortName = 's' });
            Assert.AreEqual("/s", optionFlag);
        }
    }
}