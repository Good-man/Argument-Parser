using System;

namespace ArgumentParser
{
    public class AssignmentOperator
    {
        private readonly string _name;
        public string Operator { get; }

        private AssignmentOperator(string character, string name)
        {
            Operator = character;
            _name = name;
        }

        public static AssignmentOperator Colon { get; } = new AssignmentOperator(":", "Colon");
        public static AssignmentOperator Equal { get; } = new AssignmentOperator("=", "Equal");
        public static AssignmentOperator Space { get; } = new AssignmentOperator(" ", "Space");

        public override string ToString()
        {
            return _name;
        }

        public bool Equals(string character)
        {
            return Operator == character;
        }

        internal static AssignmentOperator Parse(string assignmentCharacter)
        {
            switch (assignmentCharacter)
            {
                case ":":
                    return Colon;
                case "=":
                    return Equal;
                case " ":
                    return Space;
                case "":
                case null:
                    return null;
                default:
                    throw new ArgumentOutOfRangeException(nameof(assignmentCharacter), assignmentCharacter,
                        "Assignment character not supported.");
            }
        }
    }
}