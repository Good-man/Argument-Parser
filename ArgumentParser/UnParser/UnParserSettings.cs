using System;

namespace ArgumentParser
{
    public class UnParserSettings
    {
        private UnParserSettings()
        {
            // defaults
            AssignmentOperator = AssignmentOperator.Colon;
            OptionPrefix = OptionPrefix.SingleHyphen;
        }

        public static UnParserSettings Default => new UnParserSettings();

        public AssignmentOperator AssignmentOperator { get; set; }

        public OptionPrefix OptionPrefix { get; set; }

        internal string GetAssignmentOperator()
        {
            return AssignmentOperator.Operator;
        }
    }
}