using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using PluginFramework;

namespace PixelArt
{
    public class PixelArt : IPixel
    {
        public string Name => "Pixel Art";
        

        //private Color Clr(Color[] cs,Color[] clrs)
        //{
        //    Color c = Color.Black;

        //    int r = 0;
        //    int g = 0;
        //    int b = 0;

        //    for (int i = 0; i < cs.Length; i++)
        //    {
        //        r += cs[i].R;
        //        g += cs[i].G;
        //        b += cs[i].B;
        //    }

        //    r /= cs.Length;
        //    g /= cs.Length;
        //    b /= cs.Length;

        //    int near = 1000;
        //    int ind = 0;

        //    for (int cl = 0; cl < clrs.Length; cl++)
        //    {
        //        int valR = (clrs[cl].R - r);
        //        int valG = (clrs[cl].G - g);
        //        int valB = (clrs[cl].B - b);

        //        if (valR < 0) valR = -valR;
        //        if (valG < 0) valG = -valG;
        //        if (valB < 0) valB = -valB;

        //        int total = valR + valG + valB;

        //        if (total < near)
        //        {
        //            ind = cl;
        //            near = total;
        //        }
        //    }

        //    c = clrs[ind];

        //    return c;
        //}


        public Image RunPlugin(Image src,int value, string[] htmlClrs)
        {
            Color[] clrs = new Color[1];
            Bitmap btm = new Bitmap(1, 1);
            Bitmap bBt = new Bitmap(1, 1);
            Graphics graphics = null;
            
            clrs = new Color[htmlClrs.Length];

            for (int v = 0; v < htmlClrs.Length; v++)
            {
                try
                {
                    clrs[v] = ColorTranslator.FromHtml(htmlClrs[v]);
                }
                catch
                {
                    clrs[v] = Color.Transparent;
                }
            }

            int num = (int)value;

            btm = (Bitmap)src;
            bBt = new Bitmap(btm.Width, btm.Height);

            using (graphics = Graphics.FromImage(bBt))
            {
                List<Color> block = new List<Color>();

                Rectangle rec = new Rectangle();

                SolidBrush sb = new SolidBrush(Color.Black);

                Color final = Color.Black;

                for (int x = 0; x < btm.Width; x += num)
                {
                    for (int y = 0; y < btm.Height; y += num)
                    {
                        block = new List<Color>();

                        for (int v = 0; v < num; v++)
                        {
                            for (int c = 0; c < num; c++)
                            {
                                if (x + v < btm.Width && y + c < btm.Height)
                                {
                                    block.Add(btm.GetPixel(x + v, y + c));
                                }
                            }
                        }

                        if (block.Count > 0)
                        {



                            Color[] cs = block.ToArray();
                            Color c = Color.Black;

                            int r = 0;
                            int g = 0;
                            int b = 0;

                            for (int i = 0; i < cs.Length; i++)
                            {
                                r += cs[i].R;
                                g += cs[i].G;
                                b += cs[i].B;
                            }

                            r /= cs.Length;
                            g /= cs.Length;
                            b /= cs.Length;

                            int near = 1000;
                            int ind = 0;

                            for (int cl = 0; cl < clrs.Length; cl++)
                            {
                                int valR = (clrs[cl].R - r);
                                int valG = (clrs[cl].G - g);
                                int valB = (clrs[cl].B - b);

                                if (valR < 0) valR = -valR;
                                if (valG < 0) valG = -valG;
                                if (valB < 0) valB = -valB;

                                int total = valR + valG + valB;

                                if (total < near)
                                {
                                    ind = cl;
                                    near = total;
                                }
                            }

                            c = clrs[ind];

                            final = c;

                            sb.Color = final;

                            rec.X = x;
                            rec.Y = y;
                            rec.Width = num;
                            rec.Height = num;

                            graphics.FillRectangle(sb, rec);
                        }
                    }
                }

                return bBt;
            }
        }
    }
}
