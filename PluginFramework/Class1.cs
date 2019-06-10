using System.Drawing;
namespace PluginFramework
{
    public interface IFilter
    {
        Image RunPlungin(Image src);
        string Name { get; }
    }
}
