using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using ArgumentParser.Api;

namespace ArgumentParser.Internal
{
    internal class ArgumentSpecifications<TOptions> : IEnumerable<IArgumentSpecification>,
        IFluentSyntaxBuilder<TOptions>
    {

        private readonly HashSet<IArgumentSpecification> _argumentSpecifications = new HashSet<IArgumentSpecification>();

        public IFluentCommandBuilder SetupCommand(string name)
        {
            if (_argumentSpecifications.Any(s => s.LongName == name))
            {
                var specification = _argumentSpecifications.First(s => s.LongName == name);
                var builder = specification as CommandSpecification;
                if (builder != null)
                    return builder;
                _argumentSpecifications.Remove(specification);
            }
            ArgumentSpecification.ValidateName(name);
            var commandBuilder = new CommandSpecification(name);
            _argumentSpecifications.Add(commandBuilder);
            return commandBuilder;
        }

        public IFluentOptionBuilder<TValue> SetupOption<TValue>(Expression<Func<TOptions, TValue>> selector)
        {
            var memberInfo = ((MemberExpression)selector.Body).Member;
            return SetupOption<TValue>(memberInfo);
        }

        private IFluentOptionBuilder<TValue> SetupOption<TValue>(MemberInfo memberInfo)
        {
            if (_argumentSpecifications.Any(s => s.MemberInfo == memberInfo))
            {
                var specification = _argumentSpecifications.First(s => s.MemberInfo == memberInfo);
                var builder = specification as OptionSpecification<TValue>;
                if (builder != null)
                    return builder;
                _argumentSpecifications.Remove(specification);
            }
            var optionBuilder = new OptionSpecification<TValue>(memberInfo);
            _argumentSpecifications.Add(optionBuilder);
            return optionBuilder;
        }

        public IFluentValueBuilder<TValue> SetupValue<TValue>(Expression<Func<TOptions, TValue>> selector)
        {
            var memberInfo = ((MemberExpression)selector.Body).Member;
            return SetupValue<TValue>(memberInfo);
        }

        private IFluentValueBuilder<TValue> SetupValue<TValue>(MemberInfo memberInfo)
        {
            if (_argumentSpecifications.Any(s => s.MemberInfo == memberInfo))
            {
                var specification = _argumentSpecifications.First(s => s.MemberInfo == memberInfo);
                var builder = specification as ValueSpecification<TValue>;
                if (builder != null)
                    return builder;
                _argumentSpecifications.Remove(specification);
            }
            var optionBuilder = new ValueSpecification<TValue>(memberInfo);
            _argumentSpecifications.Add(optionBuilder);
            return optionBuilder;
        }

        [Obsolete("ArgumentSpecifications should be IEnumerable<IArgumentSpecification>")]
        internal IEnumerable<IArgumentSpecification> GetSpecifications()
        {
            return _argumentSpecifications;
        }

        internal IEnumerable<TSpecification> GetSpecifications<TSpecification>()
            where TSpecification : IArgumentSpecification
        {
            return _argumentSpecifications.OfType<TSpecification>();
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
            var x = this.Where(a => a.HasLongName).GroupBy(a => (object)a.LongName).Where(g => g.Count() > 1);
            var y = this.Where(a => a.HasShortName).GroupBy(a => (object)a.ShortName).Where(g => g.Count() > 1);
            var z = this.Where(a => a.GetType().IsGenericTypeOf(typeof(ValueSpecification<>))).GroupBy(a => (object)a.Index).Where(g => g.Count() > 1);
            var dups = x.Union(y).Union(z).ToArray();

            var hasDups = dups.Any();
            if (hasDups)
            {
                var firstDup = dups.First();
                throw new DuplicateKeyException(firstDup.Key);
            }
        }

        public IEnumerator<IArgumentSpecification> GetEnumerator()
        {
            return _argumentSpecifications.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}