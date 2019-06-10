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

        public Image RunPlungin(Image src)
        {
            var bitmap = new Bitmap(src);
            for (int row = 0; row < bitmap.Height; row++)
            {
                for (int col = 0; col < bitmap.Width; col++)
                {
                    Color color = bitmap.GetPixel(col, row);
                    if(color.R > 0)
                    {
                        color = Color.FromArgb(color.A, 255, color.G, color.B);
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

        public Image RunPlungin(Image src)
        {
            var bitmap = new Bitmap(src);
            for (int row = 0; row < bitmap.Height; row++)
            {
                for (int col = 0; col < bitmap.Width; col++)
                {
                    Color color = bitmap.GetPixel(col, row);
                    if (color.G > 0)
                    {
                        color = Color.FromArgb(color.A, color.R, 255, color.B);
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

        public Image RunPlungin(Image src)
        {
            var bitmap = new Bitmap(src);
            for (int row = 0; row < bitmap.Height; row++)
            {
                for (int col = 0; col < bitmap.Width; col++)
                {
                    Color color = bitmap.GetPixel(col, row);
                    if (color.B > 0)
                    {
                        color = Color.FromArgb(color.A, color.R, color.G, 255);
                    }
                    bitmap.SetPixel(col, row, color);
                }
            }
            return bitmap;
        }
    }
}
