using System.Drawing;
namespace PluginFramework
{
    public interface IFilter
    {
        Image RunPlungin(Image src,int r,int g,int b,int a = 255);
        string Name { get; }
        string Info { get; }
    }
    public interface IPixel
    {
        Image RunPlugin(Image src,int value, string[] htmlClrs);
        string Name { get; }
        string Info { get; }
    }
}
