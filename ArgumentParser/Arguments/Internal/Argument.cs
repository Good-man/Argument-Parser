using System;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ArgumentParser.Internal
{
    internal class Argument
    {
        internal const string OptionId = "option";
        internal const string PrefixId = "prefix";
        internal const string NameId = "name";
        internal const string Suffix = "suffix";
        internal const string AssignmentId = "assignment";
        internal const string ValueId = "value";

        private Argument(string text)
        {
            Text = text;
        }

        public AssignmentOperator AssignmentOperator { get; set; }

        public bool HasName => !string.IsNullOrWhiteSpace(Name);
        public bool HasValue => !string.IsNullOrWhiteSpace(Value);

        public string Name { get; set; }

        public OptionPrefix OptionPrefix { get; set; }

        public OptionSuffix OptionSuffix { get; set; }

        public string Text { get; set; }

        public string Value { get; set; }

        internal static string ArgumentPattern => $@"{OptionPattern}?{AssignmentPattern}?{ValuePattern}?";
        internal static string OptionPattern => $"(?<{OptionId}>{PrefixPattern}{NamePattern}?{SuffixPattern}?)";
        internal static string PrefixPattern => $"(?<{PrefixId}>(?:-{{1,2}}|\\/))";
        internal static string NamePattern => $"(?<{NameId}>[a-zA-Z_0-9]{{1,}})";
        internal static string SuffixPattern => $"(?<{Suffix}>[+-])";
        internal static string AssignmentPattern => $"(?<{AssignmentId}>[=: ])";
        internal static string ValuePattern => $@"(?<{ValueId}>.+)"; // Need optional quotes

        public Argument Clone()
        {
            return new Argument(Text)
            {
                OptionPrefix = OptionPrefix,
                Name = Name,
                OptionSuffix = OptionSuffix,
                AssignmentOperator = AssignmentOperator,
                Value = Value
            };
        }

        public bool Is(Type type)
        {
            if (type.IsEnum)
            {
                if (HasName && Name.Is(type))
                    return true;
                if (HasValue && Value.Is(type))
                    return true;
                return false;
            }
            if (type == typeof(bool))
                return !HasValue || HasValue && Value.Is(type);
            return HasValue && Value.Is(type);
        }

        public static Argument Parse(string text)
        {
            var regex = new Regex(ArgumentPattern);
            var match = regex.Match(text);

            if (!match.Success) return null;

            var argument = new Argument(text);
            if (match.Groups[PrefixId].Success)
                argument.OptionPrefix = OptionPrefix.Parse(match.Groups[PrefixId].Value);
            if (match.Groups[NameId].Success)
                argument.Name = match.Groups[NameId].Value;
            if (match.Groups[Suffix].Success)
                argument.OptionSuffix = OptionSuffix.Parse(match.Groups[Suffix].Value);
            if (match.Groups[AssignmentId].Success)
                argument.AssignmentOperator = AssignmentOperator.Parse(match.Groups[AssignmentId].Value);
            if (match.Groups[ValueId].Success)
                argument.Value = match.Groups[ValueId].Value;
            return argument;
        }

        public object ParseValue(Type type)
        {
            if (type.IsEnum)
            {
                if (HasName && Name.Is(type))
                    return Name.ConvertTo(type);
                if (HasValue && Value.Is(type))
                    return Value.ConvertTo(type);
                return null;
            }
            if (type == typeof(bool))
            {
                if (HasValue && Value.Is(type))
                    return Value.ConvertTo(type);
                return true;
            }
            if (type == typeof(string))
            {
                if (HasValue)
                    return Value;
                return Name;
            }
            if (type == typeof(DateTime))
            {
                if (HasValue && Value.Is(type))
                    return Value.ConvertTo(type);
                return null;
            }
            if (type.IsValueType)
            {
                if (HasValue && Value.Is(type))
                    return Value.ConvertTo(type);
            }
            throw new InvalidOperationException($"Unable to convert the argument to '{type}'");
        }

        public override string ToString()
        {
            return
                $"{{ Text = {Text}, OptionPrefix = {OptionPrefix}, Name = {Name}, ToggleCharacter = {OptionSuffix}, AssignmentCharacter = {AssignmentOperator}, Value = {Value} }}";
        }
    }
}