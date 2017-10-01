using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using ArgumentParser.Api;

namespace ArgumentParser.Internal
{
    internal class ArgumentSpecifications<TOptions> : Dictionary<object, IArgumentSpecification>, IFluentSyntaxBuilder<TOptions>
    {
        public ArgumentSpecifications()
        {
        }

        internal void ReadAttributes()
        {
            ReadCommandAttributes();
            ReadOptionAttributes();
        }

        private void ReadCommandAttributes()
        {
            var commandAttributes = typeof(TOptions).GetCommandAttributes();
            foreach (var commandAttribute in commandAttributes)
            {
                var name = commandAttribute.Name;
                Setup(name);
            }
        }

        private void ReadOptionAttributes()
        {
            var optionAttributes = typeof(TOptions).GetOptionAttributes();
            foreach (var memberInfo in optionAttributes.Keys)
            {
                var memberType = memberInfo.GetMemberType();
                var setupMethod = this.GetType()
                    .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
                    .Where(m => m.Name == "Setup")
                    .Select(m => new
                    {
                        Method = m,
                        Params = m.GetParameters(),
                    })
                    .Where(x => x.Params.Length == 1 && x.Params[0].ParameterType == typeof(MemberInfo))
                    .Select(x => x.Method)
                    .Single();
                var genericSetup = setupMethod.MakeGenericMethod(memberType);
                var argumentSpecification = genericSetup.Invoke(this, new object[] { memberInfo }) as ArgumentSpecification;
                // TODO: set properties based on attributes
            }
        }

        public IFluentCommandBuilder Setup(string name)
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

        public IFluentOptionTypeBuilder<TValue> Setup<TValue>(Expression<Func<TOptions, TValue>> selector)
        {
            var memberInfo = ((MemberExpression)selector.Body).Member;
            return Setup<TValue>(memberInfo);
        }

        internal IFluentOptionTypeBuilder<TValue> Setup<TValue>(MemberInfo memberInfo)
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

        internal IEnumerable<IArgumentSpecification> GetSpecifications()
        {
            return Values.Distinct();
        }

        internal IEnumerable<TSpecification> GetSpecifications<TSpecification>()
            where TSpecification : IArgumentSpecification
        {
            return Values.Distinct().OfType<TSpecification>();
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
    }
}