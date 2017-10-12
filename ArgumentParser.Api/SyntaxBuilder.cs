using System.Collections.Generic;

namespace ArgumentParser.Api
{
    class SyntaxBuilder : IFluentSyntaxBuilder
    {
        private readonly Dictionary<object, IArgumentSpecification> _argumentSpecifications = new Dictionary<object, IArgumentSpecification>();

        public IOptionBuilder Add(string longName)
        {
            var key = (object)longName;
            return Add(key);
        }

        public IOptionBuilder Add(char shortName)
        {
            var key = (object) shortName;
            return Add(key);
        }

        private IOptionBuilder Add(object key)
        {

            if (_argumentSpecifications.ContainsKey(key))
            {
                var argumentSpecification = _argumentSpecifications[key];
                if (argumentSpecification is IOptionSpecification)
                    return new OptionBuilder(argumentSpecification as IOptionSpecification);
                _argumentSpecifications.Remove(argumentSpecification);
            }
            var optionSpecification = new OptionSpecification();
            _argumentSpecifications.Add(key, optionSpecification);
            return new OptionBuilder(optionSpecification);
        }
    }
}