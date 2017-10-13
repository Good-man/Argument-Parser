using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArgumentParser.Api
{
    public interface IFluentSyntaxBuilder
    {
        IArgumentBuilder Add(string longName);
        IArgumentBuilder Add(char shortName);
        IArgumentBuilder Add();
    }
}
