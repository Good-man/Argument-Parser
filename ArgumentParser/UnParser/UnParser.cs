using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using ArgumentParser.Api;
using ArgumentParser.Internal;

namespace ArgumentParser
{
    public class UnParser
    {
        private readonly UnParserSettings _unparserSettings;

        public UnParser() : this(UnParserSettings.Default)
        {
        }

        internal UnParser(UnParserSettings unParserSettings)
        {
            _unparserSettings = unParserSettings;
        }

        internal UnParser(Action<UnParserSettings> configure) : this(UnParserSettings.Default)
        {
            configure(_unparserSettings);
        }

        internal string FormatArgument(IArgumentSpecification argumentSpecification, Type memberType, object memberValue)
        {
            var required = argumentSpecification.Required;
            var defaultValue = argumentSpecification.DefaultValue;
            if (memberType != null && memberType.IsEnum)
            {
                return FormatEnum(argumentSpecification, memberValue as Enum);
            }
            if (argumentSpecification.HasName)
                return FormatOptionArgument(argumentSpecification, memberValue, required, defaultValue);
            return FormatValue(memberValue, required, defaultValue);            
        }

        internal string FormatEnum(IArgumentSpecification argumentSpecification, Enum enumValue)
        {
            string argument;
            var enumString = enumValue.ToString();
            if (string.Equals(argumentSpecification.LongName, enumString,
                StringComparison.CurrentCultureIgnoreCase))
                argument = FormatOptionFlag(argumentSpecification);
            else
                argument = enumString;
            return argument;
        }

        internal string FormatOptionArgument(
            IArgumentSpecification optionSpecification, 
            object optionValue,
            bool required, 
            object defaultValue)
        {
            if (required || !Equals(optionValue, defaultValue))
            {
                var optionString = FormatOptionFlag(optionSpecification);

                if (optionValue is bool)
                {
                    var isTrue = (bool)optionValue;
                    if (isTrue)
                        return $"{optionString}";
                    return $"{optionString}-";
                }

                var assignment = _unparserSettings.GetAssignmentOperator();
                return $"{optionString}{assignment}{optionValue}";
            }
            return String.Empty;
        }


        internal string FormatOptionFlag(IArgumentSpecification optionSpecification)
        {
            var preferredPrefix = _unparserSettings.OptionPrefix;
            if (preferredPrefix == OptionPrefix.DoubleHyphen && optionSpecification.HasLongName)
            {
                var longName = optionSpecification.LongName;
                return $"{preferredPrefix.Prefix}{longName}";
            }
            if (preferredPrefix == OptionPrefix.SingleHyphen | preferredPrefix == OptionPrefix.ForwardSlash
                && optionSpecification.HasShortName)
            {
                var shortName = optionSpecification.ShortName;
                return $"{preferredPrefix.Prefix}{shortName}";
            }
            if (optionSpecification.HasLongName)
            {
                var longName = optionSpecification.LongName;
                return $"{OptionPrefix.DoubleHyphen.Prefix}{longName}";
            }
            if (optionSpecification.HasShortName)
            {
                var shortName = optionSpecification.ShortName;
                return $"{OptionPrefix.SingleHyphen.Prefix}{shortName}";
            }
            return String.Empty;
        }

        internal static string FormatValue(object value, bool required, object defaultValue)
        {
            if (required && value == null && defaultValue == null)
                throw new ArgumentNullException(nameof(value));

            if (!required && value == null && defaultValue == null)
                return string.Empty;

            var type = value?.GetType() ?? defaultValue?.GetType();

            if (type == typeof(string))
            {
                var str = value as string ?? defaultValue as string;
                return WithQuotes(str);
            }
            if (type == typeof(bool))
            {
                var boolean = (bool?) value ?? (bool)defaultValue;
                return $"{boolean.ToString().ToLower()}";
            }
            if (type == typeof(DateTime))
            {
                var dateTime = (DateTime?) value ?? (DateTime)defaultValue;
                return dateTime.ToString();
            }
            throw new NotSupportedException($"Type {type} is currently not supported.");
        }

        internal static IEnumerable<string> WithQuotes(IEnumerable<string> args)
        {
            foreach (var arg in args)
            {
                string withQuotes = WithQuotes(arg);
                yield return withQuotes;
            }
        }

        internal static string WithQuotes(string arg)
        {
            var sb = new StringBuilder();
            var containsSpaces = arg.Contains(" ");
            if (containsSpaces)
                sb.Append("\"");
            sb.Append(arg);
            if (containsSpaces)
                sb.Append("\"");
            var withQuotes = sb.ToString();
            return withQuotes;
        }

    }

    public class UnParser<TOptions> : UnParser
    {
        private readonly SyntaxBuilder<TOptions> _syntaxBuilder;

        public UnParser() : this(new SyntaxBuilder<TOptions>(), UnParserSettings.Default)
        {
        }

        public UnParser(Action<UnParserSettings> configure) : base(configure)
        {
            _syntaxBuilder = new SyntaxBuilder<TOptions>();
        }

        private UnParser(SyntaxBuilder<TOptions> syntaxBuilder, UnParserSettings unParserSettings) : base(unParserSettings)
        {
            _syntaxBuilder = syntaxBuilder;
        }

        public IFluentSyntaxBuilder<TOptions> Configure()
        {
            return _syntaxBuilder;
        }

        public string FormatCommandLine(TOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            var sb = new StringBuilder();

            foreach (var commandBuilder in _syntaxBuilder.GetSpecifications<CommandSpecification>())
            {
                // TODO: Format Command
                sb.Append(commandBuilder.LongName);
                sb.Append(" ");
            }

            foreach (var memberInfo in options.GetType().GetMembers().Where(m => m is PropertyInfo | m is FieldInfo))
            {
                var argument = FormatMember(options, memberInfo);
                if (argument != null)
                {
                    sb.Append(argument);
                    sb.Append(" ");
                }
            }

            return sb.ToString().Trim();
        }

        internal string FormatMember(TOptions options, MemberInfo memberInfo)
        {
            var argumentSpecification = _syntaxBuilder
                .GetSpecifications<ArgumentSpecification>()
                .FirstOrDefault(a => a.MemberInfo == memberInfo);
            if (argumentSpecification == null)
                return null;

            Type memberType;
            object memberValue;
            if (memberInfo is PropertyInfo)
            {
                var propertyInfo = (PropertyInfo)memberInfo;
                memberType = propertyInfo.PropertyType;
                memberValue = propertyInfo.GetValue(options);
            }
            else if (memberInfo is FieldInfo)
            {
                var fieldInfo = (FieldInfo)memberInfo;
                memberType = fieldInfo.FieldType;
                memberValue = fieldInfo.GetValue(options);
            }
            else
            {
                throw new NotSupportedException($"MemberInfo '{memberInfo}' is not supported.");
            }
            return FormatArgument(argumentSpecification, memberType, memberValue);
        }
    }
}