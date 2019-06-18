using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using PluginFramework;
namespace FlipImage
{
    public class FlipImage : IFilter
    {
        public string Name => "Flip Image";

        public string Info => "Flips image on 180 degrees";

        public Image RunPlungin(Image src, int r, int g , int b , int a = 255)
        {
            Bitmap bitmap = new Bitmap(src);
            Bitmap newBitmap = new Bitmap(src);
            for (int row = 0; row < bitmap.Height; ++row)
            {
                for (int col = 0; col < bitmap.Width; ++col)
                {
                    newBitmap.SetPixel(bitmap.Width - col - 1, bitmap.Height - row - 1, bitmap.GetPixel(col,row));
                }
            }
            bitmap.Dispose();
            return newBitmap;
        }
    }
}
