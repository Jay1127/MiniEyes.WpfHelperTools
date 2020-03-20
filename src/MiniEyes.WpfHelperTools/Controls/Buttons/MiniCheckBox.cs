using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace MiniEyes.WpfHelperTools
{
    public class MiniCheckBox : CheckBox
    {
        public Brush AccentColor
        {
            get { return (Brush)GetValue(AccentColorProperty); }
            set { SetValue(AccentColorProperty, value); }
        }

        public static readonly DependencyProperty AccentColorProperty =
            DependencyProperty.Register("AccentColor", typeof(Brush), typeof(MiniCheckBox), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

    }
}