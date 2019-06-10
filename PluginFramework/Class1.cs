using System.Drawing;
namespace PluginFramework
{
    public interface IFilter
    {
        Image RunPlungin(Image src);
        string Name { get; }
    }
    public interface IPixel
    {
        Image RunPlugin(Image src,int value, string[] htmlClrs);
        string Name { get; }
    }
}
