namespace ArgumentParser.Api
{
    public class OptionBuilder<TValue> : IOptionBuilder<TValue>
    {
        public IOptionSpecification<TValue> OptionSpecification { get; }

        public OptionBuilder(IOptionSpecification<TValue> optionSpecification)
        {
            OptionSpecification = optionSpecification;
        }

        public IOptionBuilder<TValue> WithDescription(string description)
        {
            throw new System.NotImplementedException();
        }

        public IOptionBuilder<TValue> SetDefault(TValue value)
        {
            throw new System.NotImplementedException();
        }
    }
}