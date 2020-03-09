using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MiniWpfHelperTools
{
    internal class HsbToLinearGradientConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var hue = (double)value;

            return ColorConverter.ToRGB(hue, 1, 1);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}