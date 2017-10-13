using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArgumentParser.Api
{
    public interface ISyntaxBuilder
    {
        IArgumentBuilder AddCommand(string name);
        IArgumentBuilder AddOption(string longName);
        IArgumentBuilder AddOption(char shortName);
        IArgumentBuilder AddValue();
    }
}
