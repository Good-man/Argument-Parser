using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using ArgumentParser.Api;
using ArgumentParser.Internal;

namespace ArgumentParser
{
    public class Parser<TOptions> : IFluentSyntaxBuilder<TOptions>, IParser<TOptions> where TOptions : new()
    {
        private readonly ArgumentSpecifications<TOptions> _argumentSpecifications;

        public Parser()
        {
            _argumentSpecifications = new ArgumentSpecifications<TOptions>();
            _argumentSpecifications.ReadAttributes();
        }

        public IParserResult<TOptions> Parse(IEnumerable<string> args)
        {
            var commandLine = CommandLine.Parse(args);
            var parsedOptions = new TOptions();

            var commands = _argumentSpecifications.GetSpecifications<CommandSpecification>().ToArray();
            foreach (var commandSpecification in commands)
            {
                var argument = commandLine.FindFirst(commandSpecification);
                if (argument == null)
                    return default(IParserResult<TOptions>);
                commandLine.Remove(argument);
            }

            var options = _argumentSpecifications
                .GetSpecifications<ArgumentSpecification>()
                .Except(commands)
                .Where(o => o.HasName)
                .ToArray();
            foreach (var optionSpecification in options)
            {
                var argument = commandLine.FindFirst(optionSpecification);
                SetMember(optionSpecification, parsedOptions, argument);
                if (argument != null)
                    commandLine.Remove(argument);
            }

            var valueSpecifications = _argumentSpecifications
                .GetSpecifications<ArgumentSpecification>()
                .Except(commands)
                .Except(options)
                .Where(o => !o.HasName)
                .ToArray();
            foreach (var valueSpecification in valueSpecifications)
            {
                var memberInfo = valueSpecification.MemberInfo;
                var type = memberInfo.GetMemberType();
                if (typeof(IEnumerable).IsAssignableFrom(type) && type.IsGenericType)
                {
                    var arguments = commandLine.FindAll(valueSpecification);
                    foreach (var argument in arguments)
                    {
                        AddToMember(valueSpecification, parsedOptions, argument);
                        commandLine.Remove(argument);
                    }
                }
                else
                {
                    var argument = commandLine.FindFirst(valueSpecification);
                    SetMember(valueSpecification, parsedOptions, argument);
                    if (argument != null)
                        commandLine.Remove(argument);
                }
            }

            return new ParserResult<TOptions>(parsedOptions);
        }

        private void AddToMember(IArgumentSpecification argumentSpecification, TOptions options, Argument argument)
        {
            throw new NotImplementedException();
        }

        //if (typeof(IEnumerable).IsAssignableFrom(type) && type.IsGenericType)
        //{
        //    var listItemType = type.GetGenericArguments()[0];
        //    object value = null;
        //    if (argument != null)
        //        value = argument.ParseValue(listItemType);
        //    var list = memberInfo.GetValue(options);
        //    if (list == null)
        //    {
        //        var listRef = typeof(List<>);
        //        Type[] listParam = { listItemType };
        //        list = Activator.CreateInstance(listRef.MakeGenericType(listParam));
        //        memberInfo.SetValue(options, list);
        //    }

        //    list.GetType().GetMethod("Add").Invoke(list, new[] { value
        //    });
        //}

        private void SetMember(IArgumentSpecification specification, TOptions options, Argument argument)
        {
            var memberInfo = specification.MemberInfo;
            var type = memberInfo.GetMemberType();

            var hasDefault = specification.HasDefault;
            var defaultValue = specification.DefaultValue;
            object value = null;
            if (argument != null)
                value = argument.ParseValue(type);
            if ((value == null) & hasDefault)
                value = defaultValue;
            memberInfo.SetValue(options, value);
        }

        public IFluentCommandBuilder SetupCommand(string name)
        {
            return _argumentSpecifications.SetupCommand(name);
        }

        public IFluentOptionBuilder<TValue> SetupOption<TValue>(Expression<Func<TOptions, TValue>> selector)
        {
            return _argumentSpecifications.SetupOption(selector);
        }

        public IFluentValueBuilder<TValue> SetupValue<TValue>(Expression<Func<TOptions, TValue>> selector)
        {
            return _argumentSpecifications.SetupValue(selector);
        }

        public void Validate()
        {
            _argumentSpecifications.Validate();
        }
    }
}