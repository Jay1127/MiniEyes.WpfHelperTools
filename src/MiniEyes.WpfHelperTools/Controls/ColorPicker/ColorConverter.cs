using System;
using System.Linq;
using System.Windows.Media;

namespace MiniEyes.WpfHelperTools
{
    /// <summary>
    /// rgb & hsb color converter
    /// </summary>
    /// <see cref="https://www.cs.rit.edu/~ncs/color/t_convert.html"/>
    internal class ColorConverter
    {
        /// <summary>
        /// convert hsb to rgb color
        /// </summary>
        /// <param name="hsb"></param>
        /// <returns></returns>
        public static Color ToRGB(HSB hsb)
        {
            return ToRGB(hsb.Hue, hsb.Saturation, hsb.Brightness);
        }
        
        /// <summary>
        /// convert hsb to rgb
        /// </summary>
        /// <param name="hue"></param>
        /// <param name="saturation"></param>
        /// <param name="brightness"></param>
        /// <returns></returns>
        public static Color ToRGB(double hue, double saturation, double brightness)
        {
            var h = hue;
            var s = saturation;
            var b = brightness * 255;

            if (h < 0 || h >= 360)
            {
                h = 0;
            }

            if (s == 0)
            {
                return Color.FromArgb(255, (byte)b, (byte)b, (byte)b);
            }

            h /= 60;

            int i = (int)Math.Floor(h);
            var f = h - i;
            var p = b * (1 - s);
            var q = b * (1 - s * f);
            var t = b * (1 - s * (1 - f));

            switch (i)
            {
                case 0:
                    return Color.FromRgb((byte)b, (byte)t, (byte)p);
                case 1:
                    return Color.FromRgb((byte)q, (byte)b, (byte)p);
                case 2:
                    return Color.FromRgb((byte)p, (byte)b, (byte)t);
                case 3:
                    return Color.FromRgb((byte)p, (byte)q, (byte)b);
                case 4:
                    return Color.FromRgb((byte)t, (byte)p, (byte)b);
                case 5:
                    return Color.FromRgb((byte)b, (byte)p, (byte)q);
            }

            throw new Exception("Can't convert hsv to rgb");
        }

        /// <summary>
        /// convert rgb to hsb
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static HSB ToHSB(Color color)
        {
            var r = color.R / 255.0;
            var g = color.G / 255.0;
            var b = color.B / 255.0;

            var rgb = new[] { r, g, b };

            var min = rgb.Min();
            var max = rgb.Max();

            if(max == 0)
            {
                return new HSB(0, 0, 0);
            }

            double delta = max - min;

            if(delta == 0)
            {
                return new HSB(0, 0, max);
            }

            var h = 0.0;

            if (max == r && g >= b)
                h = 60 * (g - b) / delta;
            else if (max == r && g < b)
            {
                h = 60 * (g - b) / delta + 360;
            }
            else if (max == g)
                h = 60 * (b - r) / delta + 120;
            else if(max == b)
                h = 60 * (r - g) / delta + 240;

            var s = delta / max;
            var v = max; 

            return new HSB(h, s, v);
        }
    }
}