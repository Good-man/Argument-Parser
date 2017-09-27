using ArgumentParser.Api;

namespace ArgumentParser.Internal
{
    internal class CommandSpecification : ArgumentSpecification, IFluentCommandBuilder
    {
        public CommandSpecification(string name) : base(null)
        {
            LongName = name;
            Required = true;
        }

        public override string ToString()
        {
            return
                $"{{ Name = {LongName}, Description = {Description}, Required = {Required}, Default = {DefaultValue} }}";
        }

        IFluentCommandBuilder IFluentArgumentBuilder<IFluentCommandBuilder>.WithDescription(string description)
        {
            Description = description;
            return this;
        }
    }
}