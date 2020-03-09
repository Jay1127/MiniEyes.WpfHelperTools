using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Windows.Media;

namespace MiniWpfHelperTools
{
    /// <summary>
    /// Definition of HSB
    /// </summary>
    public struct HSB
    {
        /// <summary>
        /// Component Hue
        /// </summary>
        public double Hue { get; set; }

        /// <summary>
        /// Component Saturation
        /// </summary>
        public double Saturation { get; set; }

        /// <summary>
        /// Component Brightness
        /// </summary>
        public double Brightness { get; set; }

        public HSB(double hue, double saturation, double brightness)
        {
            Hue = hue;
            Saturation = saturation;
            Brightness = brightness;
        }
    }
}