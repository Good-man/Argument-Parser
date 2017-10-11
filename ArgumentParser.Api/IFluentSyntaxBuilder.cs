using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArgumentParser.Api
{
    public interface IFluentSyntaxBuilder
    {
        IOptionBuilder<TValue> Add<TValue>(string longName);
        IOptionBuilder<TValue> Add<TValue>(char shortName);
    }
}
