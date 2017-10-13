using System.Collections.Generic;
using System.Linq;

namespace ArgumentParser.Api
{
    class SyntaxBuilder : ISyntaxBuilder
    {
        private readonly Dictionary<object, IArgumentSpecification> _argumentSpecifications = new Dictionary<object, IArgumentSpecification>();

        public IArgumentBuilder AddCommand(string name)
        {
            var key = (object)name;
            return Add(key);
        }

        public IArgumentBuilder AddOption(string longName)
        {
            var key = (object)longName;
            return Add(key);
        }

        public IArgumentBuilder AddOption(char shortName)
        {
            var key = (object)shortName;
            return Add(key);
        }

        public IArgumentBuilder AddValue()
        {
            var index = _argumentSpecifications.Keys.OfType<int>().Count();
            return Add(index);
        }

        private IArgumentBuilder Add(object key)
        {

            if (_argumentSpecifications.ContainsKey(key))
            {
                var argumentSpecification = _argumentSpecifications[key];
                if (argumentSpecification is IArgumentSpecification)
                    return new ArgumentBuilder(argumentSpecification as IArgumentSpecification);
                _argumentSpecifications.Remove(argumentSpecification);
            }
            var optionSpecification = new ArgumentSpecification();
            _argumentSpecifications.Add(key, optionSpecification);
            return new ArgumentBuilder(optionSpecification);
        }
    }
}