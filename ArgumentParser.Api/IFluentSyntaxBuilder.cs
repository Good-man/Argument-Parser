using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArgumentParser.Api
{
    public interface IFluentSyntaxBuilder
    {
        IOptionBuilder Add(string longName);
        IOptionBuilder Add(char shortName);
        IOptionBuilder Add();
    }
}
