using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using ArgumentParser.Api;

namespace ArgumentParser.Internal
{
    internal class ArgumentSpecifications<TOptions> : Dictionary<object, IArgumentSpecification>,
        IFluentSyntaxBuilder<TOptions>
    {
        public IFluentCommandBuilder SetupCommand(string name)
        {
            if (ContainsKey(name))
            {
                var builder = this[name] as CommandSpecification;
                if (builder != null)
                    return builder;
                Remove(name);
            }
            ValidateName(name);
            var commandBuilder = new CommandSpecification(name);
            commandBuilder.KeyChanging += ArgumentSpecification_KeyChanging;
            commandBuilder.KeyChanged += ArgumentSpecification_KeyChanged;
            this[name] = commandBuilder;
            return commandBuilder;
        }

        public IFluentOptionBuilder<TValue> SetupOption<TValue>(Expression<Func<TOptions, TValue>> selector)
        {
            var memberInfo = ((MemberExpression)selector.Body).Member;
            return SetupOption<TValue>(memberInfo);
        }

        private IFluentOptionBuilder<TValue> SetupOption<TValue>(MemberInfo memberInfo)
        {
            if (ContainsKey(memberInfo))
            {
                var builder = this[memberInfo] as OptionSpecification<TValue>;
                if (builder != null)
                    return builder;
                Remove(memberInfo);
            }
            var optionBuilder = new OptionSpecification<TValue>(memberInfo);
            optionBuilder.KeyChanging += ArgumentSpecification_KeyChanging;
            optionBuilder.KeyChanged += ArgumentSpecification_KeyChanged;
            this[memberInfo] = optionBuilder;
            return optionBuilder;
        }

        public IFluentValueBuilder<TValue> SetupValue<TValue>(Expression<Func<TOptions, TValue>> selector)
        {
            var memberInfo = ((MemberExpression)selector.Body).Member;
            return SetupValue<TValue>(memberInfo);
        }

        private IFluentValueBuilder<TValue> SetupValue<TValue>(MemberInfo memberInfo)
        {
            if (ContainsKey(memberInfo))
            {
                var builder = this[memberInfo] as ValueSpecification<TValue>;
                if (builder != null)
                    return builder;
                Remove(memberInfo);
            }
            var valueSpecification = new ValueSpecification<TValue>(memberInfo);
            valueSpecification.KeyChanging += ArgumentSpecification_KeyChanging;
            valueSpecification.KeyChanged += ArgumentSpecification_KeyChanged;
            this[memberInfo] = valueSpecification;
            return valueSpecification;
        }

        internal IEnumerable<IArgumentSpecification> GetSpecifications()
        {
            return Values.Distinct();
        }

        internal IEnumerable<TSpecification> GetSpecifications<TSpecification>()
            where TSpecification : IArgumentSpecification
        {
            return Values.Distinct().OfType<TSpecification>();
        }

        internal void ReadAttributes()
        {
            ReadCommandAttributes();
            ReadOptionAttributes();
            ReadValueAttributes();
        }

        internal void ValidateName(string name)
        {
            if (!Regex.IsMatch(name, $"^{Argument.NamePattern}$"))
                throw new InvalidOptionNameException(name);
        }

        private void ArgumentSpecification_KeyChanged(object sender, KeyChangedEventArgs e)
        {
            var key = e.Key;
            if (ContainsKey(key))
                Remove(key);
            Add(key, e.ArgumentSpecification);
        }

        private void ArgumentSpecification_KeyChanging(object sender, KeyChangingEventArgs e)
        {
            var key = e.Key;
            if (ContainsKey(key))
            {
                e.Cancel = true;
                e.Exception = new OptionAlreadyExistsException(key);
            }
            var name = key as string;
            if (name != null)
                ValidateName(name);
        }

        private void ReadCommandAttributes()
        {
            var commandAttributes = typeof(TOptions).GetCommandAttributes();
            foreach (var commandAttribute in commandAttributes)
            {
                var name = commandAttribute.Name;
                SetupCommand(name);
            }
        }

        private void ReadOptionAttributes()
        {
            var optionAttributes = typeof(TOptions).GetOptionAttributes();
            foreach (var memberInfo in optionAttributes.Keys)
            {
                var memberType = memberInfo.GetMemberType();
                var setupMethod = GetType()
                    .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic).Where(m => m.Name == "SetupOption")
                    .Select(m => new
                    {
                        Method = m,
                        Params = m.GetParameters()
                    })
                    .Where(x => x.Params.Length == 1 && x.Params[0].ParameterType == typeof(MemberInfo))
                    .Select(x => x.Method)
                    .Single();
                var genericSetup = setupMethod.MakeGenericMethod(memberType);
                var argumentSpecification = (ArgumentSpecification)genericSetup.Invoke(this, new object[] { memberInfo });

                var attribute = optionAttributes[memberInfo];

                var shortName = attribute.ShortName;
                var longName = attribute.LongName;
                var defaultValue = attribute.DefaultValue;
                var description = attribute.Description;
                var required = attribute.Required;

                argumentSpecification.ShortName = shortName;
                argumentSpecification.LongName = longName;
                argumentSpecification.DefaultValue = defaultValue;
                argumentSpecification.Description = description;
                argumentSpecification.Required = required;
            }
        }

        private void ReadValueAttributes()
        {
            var valueAttributes = typeof(TOptions).GetValueAttributes();
            foreach (var memberInfo in valueAttributes.Keys)
            {
                var memberType = memberInfo.GetMemberType();
                var setupMethod = GetType()
                    .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
                    .Where(m => m.Name == "SetupValue")
                    .Select(m => new
                    {
                        Method = m,
                        Params = m.GetParameters()
                    })
                    .Where(x => x.Params.Length == 1 && x.Params[0].ParameterType == typeof(MemberInfo))
                    .Select(x => x.Method)
                    .Single();
                var genericSetup = setupMethod.MakeGenericMethod(memberType);
                var argumentSpecification = (ArgumentSpecification) genericSetup.Invoke(this, new object[] { memberInfo });

                var attribute = valueAttributes[memberInfo];

                var index = attribute.Index;
                var defaultValue = attribute.DefaultValue;
                var description = attribute.Description;
                var required = attribute.Required;

                argumentSpecification.Index = index;
                argumentSpecification.DefaultValue = defaultValue;
                argumentSpecification.Description = description;
                argumentSpecification.Required = required;
            }
        }
    }
}