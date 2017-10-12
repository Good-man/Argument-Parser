namespace ArgumentParser.Api
{
    public class OptionBuilder : IOptionBuilder
    {
        public IOptionSpecification OptionSpecification { get; }

        public OptionBuilder(IOptionSpecification optionSpecification)
        {
            OptionSpecification = optionSpecification;
        }

        public IOptionBuilder WithDescription(string description)
        {
            throw new System.NotImplementedException();
        }

        public IOptionBuilder SetDefault(object value)
        {
            throw new System.NotImplementedException();
        }
    }
}