using System;
using System.Linq;
using System.Windows.Media;

namespace MiniWpfHelperTools
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
            var r = color.R / 255;
            var g = color.G / 255;
            var b = color.B / 255;

            var rgb = new[] { r, g, b };

            var min = rgb.Min();
            var max = rgb.Max();

            var v = max - min; // b
            double delta = max - min;

            var s = max != 0 ? delta / max : 0;

            var h = 0.0;

            if (r == max)
                h = (g - b) / delta;       // between yellow & magenta
            else if (g == max)
                h = 2 + (b - r) / delta;   // between cyan & yellow
            else
                h = 4 + (r - g) / delta;   // between magenta & cyan
            h *= 60;               // degrees
            if (h < 0)
                h += 360;

            return new HSB(h, s, v);
        }
    }
}