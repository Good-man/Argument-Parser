namespace ArgumentParser.Api
{
    public class ArgumentBuilder : IArgumentBuilder
    {
        public IArgumentSpecification OptionSpecification { get; }

        public ArgumentBuilder(IArgumentSpecification optionSpecification)
        {
            OptionSpecification = optionSpecification;
        }

        public IArgumentBuilder WithDescription(string description)
        {
            throw new System.NotImplementedException();
        }

        public IArgumentBuilder SetDefault(object value)
        {
            throw new System.NotImplementedException();
        }
    }
}