using System;
using System.Diagnostics;
using System.Linq;
using ArgumentParser.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArgumentParser
{
    [TestClass]
    [TestCategory("Argument.Parse")]
    public class ArgumentParseTests
    {
        [TestMethod]
        public void Parse_FourHundredArgumentVariations()
        {
            Debug.WriteLine(Argument.ArgumentPattern);
            var prefixes = new[] { "-", "--", "/", null };
            var names = new[] { "o", "option", null };
            var suffixes = new[] { "+", "-", null };
            var assignmentCharacters = new[] { ":", "=", " ", "" };
            var values = new[]
            {
                "some string value",
                "123",              // Integer
                "11/22/1999",       // Date
                "true", "false",    // Boolean
                "True", "False",
                null
            };
            var variations = (from p in prefixes
                             from n in names
                             from s in suffixes
                             from a in assignmentCharacters
                             from v in values
                             where !(string.IsNullOrEmpty(n) && !string.IsNullOrEmpty(p))
                                && !(string.IsNullOrEmpty(p) ^ string.IsNullOrEmpty(n))
                                && !(string.IsNullOrEmpty(n) && !string.IsNullOrEmpty(a))
                                && !(string.IsNullOrEmpty(n) && !string.IsNullOrEmpty(s))
                                && !(string.IsNullOrEmpty(a) && !string.IsNullOrEmpty(n) && !string.IsNullOrEmpty(v))
                             select new
                             {
                                 Text = p + n + s + a + v,
                                 Prefix = OptionPrefix.Parse(p),
                                 Name = string.IsNullOrEmpty(n) ? null : n,
                                 Suffix = OptionSuffix.Parse(s),
                                 AssignmentCharacter = AssignmentOperator.Parse(a),
                                 Value = v,
                             }).ToArray();

            Debug.WriteLine($"Test Variations: {variations.Length}");
            foreach (var expected in variations.ToArray())
            {
                var actual = Argument.Parse(expected.Text);
                Debug.WriteLine($"Expected: {expected}");
                Debug.WriteLine($"Actual  : {actual}");
                Assert.AreEqual(expected.Prefix, actual.OptionPrefix);
                Assert.AreEqual(expected.Name, actual.Name);
                Assert.AreEqual(expected.Suffix, actual.OptionSuffix);
                Assert.AreEqual(expected.AssignmentCharacter, actual.AssignmentOperator);
                Assert.AreEqual(expected.Value, actual.Value);
            }
        }
    }
}
