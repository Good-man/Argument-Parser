using System.Collections.Generic;

namespace ArgumentParser.Api
{
    class SyntaxBuilder : IFluentSyntaxBuilder
    {
        private readonly Dictionary<object, IArgumentSpecification> _argumentSpecifications = new Dictionary<object, IArgumentSpecification>();

        public IOptionBuilder<TValue> Add<TValue>(string longName)
        {
            var key = (object)longName;
            return Add<TValue>(key);
        }

        public IOptionBuilder<TValue> Add<TValue>(char shortName)
        {
            var key = (object) shortName;
            return Add<TValue>(key);
        }

        private IOptionBuilder<TValue> Add<TValue>(object key)
        {

            if (_argumentSpecifications.ContainsKey(key))
            {
                var argumentSpecification = _argumentSpecifications[key];
                if (argumentSpecification is IOptionSpecification<TValue>)
                    return new OptionBuilder<TValue>(argumentSpecification as IOptionSpecification<TValue>);
                _argumentSpecifications.Remove(argumentSpecification);
            }
            var optionSpecification = new OptionSpecification<TValue>();
            _argumentSpecifications.Add(key, optionSpecification);
            return new OptionBuilder<TValue>(optionSpecification);
        }
    }
}