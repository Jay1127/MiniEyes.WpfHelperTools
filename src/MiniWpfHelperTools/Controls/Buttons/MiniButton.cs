using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MiniWpfHelperTools
{
    public class MiniButton : Button
    {
        /// <summary>
        /// MouseOver시 색상
        /// </summary>
        public Brush MouseOverBackground
        {
            get { return (Brush)GetValue(MouseOverColorProperty); }
            set { SetValue(MouseOverColorProperty, value); }
        }

        public static readonly DependencyProperty MouseOverColorProperty =
            DependencyProperty.Register("MouseOverColor", typeof(Brush), typeof(MiniButton),
                new PropertyMetadata(new SolidColorBrush(Color.FromRgb(190, 230, 253))));

        /// <summary>
        /// MouseOver시 보더라인 색상
        /// </summary>
        public Brush MouseOverBorderBrush
        {
            get { return (Brush)GetValue(MouseOverBorderBrushProperty); }
            set { SetValue(MouseOverBorderBrushProperty, value); }
        }

        public static readonly DependencyProperty MouseOverBorderBrushProperty =
            DependencyProperty.Register("MouseOverBorderColor", typeof(Brush), typeof(MiniButton),
                new PropertyMetadata(new SolidColorBrush(Color.FromRgb(60, 127, 177))));
    }
}