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
            ArgumentSpecification.ValidateName(name);
            var commandBuilder = new CommandSpecification(name);
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
                var argumentSpecification = (ArgumentSpecification)genericSetup.Invoke(this, new object[] { memberInfo });

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

        public void Validate()
        {
            var x = Values.Where(a => a.HasLongName).GroupBy(a => (object) a.LongName).Where(g => g.Count() > 1);
            var y = Values.Where(a => a.HasShortName).GroupBy(a => (object) a.ShortName).Where(g => g.Count() > 1);
            var dups = x.Union(y).ToArray();

            var hasDups = dups.Any();
            if (hasDups)
            {
                var firstDup = dups.First();
                throw new DuplicateOptionException(firstDup.Key);
            }
        }
    }
}