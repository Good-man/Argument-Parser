using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using ArgumentParser.Api;
using ArgumentParser.Internal;

namespace ArgumentParser
{
    public class Parser<TOptions> : IFluentSyntaxBuilder<TOptions>, IParser<TOptions> where TOptions : new()
    {
        private readonly SyntaxBuilder<TOptions> _syntaxBuilder;

        public Parser()
        {
            _syntaxBuilder = new SyntaxBuilder<TOptions>();
        }

        public IParserResult<TOptions> Parse(IEnumerable<string> args)
        {
            var commandLine = CommandLine.Parse(args);
            var parsedOptions = new TOptions();

            var commands = _syntaxBuilder.GetSpecifications<CommandSpecification>().ToArray();
            foreach (var commandSpecification in commands)
            {
                var argument = commandLine.Find(commandSpecification);
                if (argument == null)
                    return default(IParserResult<TOptions>);
                commandLine.Remove(argument);
            }

            var options = _syntaxBuilder
                .GetSpecifications<ArgumentSpecification>()
                .Except(commands)
                .Where(o => o.HasName)
                .ToArray();
            foreach (var optionSpecification in options)
            {
                var argument = commandLine.Find(optionSpecification);
                SetMember(optionSpecification, parsedOptions, argument);
                if (argument != null)
                    commandLine.Remove(argument);
            }

            var valueSpecifications = _syntaxBuilder
                .GetSpecifications<ArgumentSpecification>()
                .Except(commands)
                .Except(options)
                .Where(o => !o.HasName)
                .ToArray();
            foreach (var valueSpecification in valueSpecifications)
            {
                var argument = commandLine.Find(valueSpecification);
                SetMember(valueSpecification, parsedOptions, argument);
                if (argument != null)
                    commandLine.Remove(argument);
            }

            return new ParserResult<TOptions>(parsedOptions);
        }

        private void SetMember(IArgumentSpecification specification, TOptions options, Argument argument)
        {
            var memberInfo = specification.MemberInfo;
            var type = memberInfo.GetMemberType();
            object value = null;
            if (argument != null)
                value = argument.ParseValue(type);
            if ((value == null) & specification.HasDefault)
                value = specification.DefaultValue;
            memberInfo.SetValue(options, value);
        }

        public IFluentCommandBuilder Setup(string name)
        {
            return _syntaxBuilder.Setup(name);
        }

        public IFluentOptionTypeBuilder<TValue> Setup<TValue>(Expression<Func<TOptions, TValue>> selector)
        {
            return _syntaxBuilder.Setup(selector);
        }
    }
}