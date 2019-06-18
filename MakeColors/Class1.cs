using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginFramework;
using System.Drawing;

namespace MakeColors
{
    public class MakeRed : IFilter
    {
        public string Name => "Make Red";

        public string Info =>  "Make your picture mostly red ";

        public Image RunPlungin(Image src,int r,int g,int b,int a = 255)
        {
            var bitmap = new Bitmap(src);
            Color color;
            for (int row = 0; row < bitmap.Height; row++)
            {
                for (int col = 0; col < bitmap.Width; col++)
                {
                    color = bitmap.GetPixel(col, row);
                    if(color.R > 0)
                    {
                        color = Color.FromArgb(color.A, r, color.G, color.B);
                    }
                    bitmap.SetPixel(col, row, color);
                }
            }
            return bitmap;
        }
    }
    public class MakeGreen : IFilter
    {
        public string Name => "Make Green";

        public string Info => "Make your picture mostly green ";

        public Image RunPlungin(Image src, int r, int g, int b, int a = 255)
        {
            var bitmap = new Bitmap(src);
            Color color;
            for (int row = 0; row < bitmap.Height; row++)
            {
                for (int col = 0; col < bitmap.Width; col++)
                {
                    color = bitmap.GetPixel(col, row);
                    if (color.R > 0)
                    {
                        color = Color.FromArgb(color.A, color.R, g, color.B);
                    }
                    bitmap.SetPixel(col, row, color);
                }
            }
            return bitmap;
        }
    }
    public class MakeBlue : IFilter
    {
        public string Name => "Make Blue";

        public string Info => "Make your picture mostly blue ";

        public Image RunPlungin(Image src, int r, int g, int b, int a = 255)
        {
            var bitmap = new Bitmap(src);
            Color color;
            for (int row = 0; row < bitmap.Height; row++)
            {
                for (int col = 0; col < bitmap.Width; col++)
                {
                    color = bitmap.GetPixel(col, row);
                    if (color.R > 0)
                    {
                        color = Color.FromArgb(color.A, color.R, color.G, b);
                    }
                    bitmap.SetPixel(col, row, color);
                }
            }
            return bitmap;
        }
    }
    public class MakeAlhpa : IFilter
    {
        public string Name => "Make Alpha";

        public string Info => "Sets apha of picture to maximum value - 255";

        public Image RunPlungin(Image src, int r, int g, int b, int a = 255)
        {
            var bitmap = new Bitmap(src);
            Color color;
            for (int row = 0; row < bitmap.Height; row++)
            {
                for (int col = 0; col < bitmap.Width; col++)
                {
                    color = bitmap.GetPixel(col, row);
                    if (color.R > 0)
                    {
                        color = Color.FromArgb(a, color.R, color.G, color.B);
                    }
                    bitmap.SetPixel(col, row, color);
                }
            }
            return bitmap;
        }
    }
    public class MakeGrayscale : IFilter
    {
        public string Name => "Make Grayscale";

        public string Info => "Adds grayscale to picture";

        public Image RunPlungin(Image src, int r, int g, int b, int a = 255)
        {
            var bitmap = new Bitmap(src);
            Color color;
            
            for (int row = 0; row < bitmap.Height; row++)
            {
                for (int col = 0; col < bitmap.Width; col++)
                {
                    color = bitmap.GetPixel(col, row);
                    int alpha = color.A;
                    int red = color.R;
                    int green = color.G;
                    int blue = color.B;
                    int avg = (red + green + blue) / 3;
                    color = Color.FromArgb(a, avg, avg, avg);
                    bitmap.SetPixel(col, row, color);
                }
            }
            return bitmap;
        }
    }
}
