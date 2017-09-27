using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ArgumentParser
{
    [TestClass]
    [TestCategory("Argument.AssignmentOperator")]
    public class AssignOperatorTests
    {
        [TestMethod]
        public void EqualsColon()
        {
            var colonOperator = AssignmentOperator.Colon;
            Assert.IsTrue(colonOperator.Equals(":"));
        }

        [TestMethod]
        public void EqualsEquals()
        {
            var equalOperator = AssignmentOperator.Equal;
            Assert.IsTrue(equalOperator.Equals("="));
        }

        [TestMethod]
        public void EqualsSpace()
        {
            var equalOperator = AssignmentOperator.Space;
            Assert.IsTrue(equalOperator.Equals(" "));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ParseInvalidCharacterThrowsException()
        {
            AssignmentOperator.Parse("x");
        }
    }
}
