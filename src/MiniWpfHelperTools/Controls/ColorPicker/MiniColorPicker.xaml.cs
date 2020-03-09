using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MiniWpfHelperTools
{
    /// <summary>
    /// implement color picker
    /// </summary>
    public partial class MiniColorPicker : Control
    {
        /// <summary>
        /// hue area
        /// </summary>
        private FrameworkElement _hueHost;

        /// <summary>
        /// hue pick arrow
        /// </summary>
        private FrameworkElement _huePicker;

        /// <summary>
        /// satuation, brightness area
        /// </summary>
        private FrameworkElement _sbHost;

        /// <summary>
        /// satuation, brightness thumb 
        /// picker
        /// </summary>
        private FrameworkElement _sbPicker;

        private bool _canChangeProperty = true;

        /// <summary>
        /// Selected HSB
        /// </summary>
        public HSB HSB
        {
            get { return (HSB)GetValue(HSBProperty); }
            set { SetValue(HSBProperty, value); }
        }

        public static readonly DependencyProperty HSBProperty =
            DependencyProperty.Register("HSB", typeof(HSB), typeof(MiniColorPicker),
                new FrameworkPropertyMetadata(ColorConverter.ToHSB(Colors.Red), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, HSBPropertyChanged));

        /// <summary>
        /// Selected Color
        /// </summary>
        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register("Color", typeof(Color), typeof(MiniColorPicker),
                 new FrameworkPropertyMetadata(Colors.Red, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, ColorPropertyChanged));

        public MiniColorPicker()
        {
            InitializeComponent();
        }

        public override void OnApplyTemplate()
        {
            _hueHost = GetTemplateChild("HueHost") as FrameworkElement;
            _huePicker = GetTemplateChild("HuePicker") as FrameworkElement;

            _hueHost.PreviewMouseLeftButtonDown += HueHost_PreviewMouseLeftButtonDown;
            _hueHost.PreviewMouseLeftButtonUp += HueHost_PreviewMouseLeftButtonUp;
            _hueHost.MouseMove += HueHost_MouseMove;

            _sbHost = GetTemplateChild("SBHost") as FrameworkElement;
            _sbPicker = GetTemplateChild("SBPicker") as FrameworkElement;

            _sbHost.MouseMove += SBHost_MouseMove;
            _sbHost.PreviewMouseLeftButtonDown += SBHost_PreviewMouseLeftButtonDown;
            _sbHost.PreviewMouseLeftButtonUp += SBHost_PreviewMouseLeftButtonUp;

            base.OnApplyTemplate();
        }

        private void HueHost_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _hueHost.ReleaseMouseCapture();
        }

        private void HueHost_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _hueHost.CaptureMouse();
            ChangeHueByPick(e);
        }

        private void HueHost_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released)
            {
                return;
            }

            ChangeHueByPick(e);
        }

        private void SBHost_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _sbHost.ReleaseMouseCapture();
        }

        private void SBHost_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _sbHost.CaptureMouse();
            ChangeSBByPick(e);
        }

        private void SBHost_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Released)
            {
                return;
            }

            ChangeSBByPick(e);
        }

        /// <summary>
        /// 마우스 위치에 따라 Hue 변경
        /// </summary>
        /// <param name="e"></param>
        private void ChangeHueByPick(MouseEventArgs e)
        {
            Point pickPoint = e.GetPosition(_hueHost);

            var hue = 360.0 * (pickPoint.Y / _hueHost.ActualHeight);
            hue = Clamp(hue, 0, 360);

            SetCurrentValue(HSBProperty, new HSB(hue, HSB.Saturation, HSB.Brightness));

            ChangePosition();
        }

        /// <summary>
        /// 마우스 위치에 따라 Satuation, Brightness 변경
        /// </summary>
        /// <param name="e"></param>
        private void ChangeSBByPick(MouseEventArgs e)
        {
            Point pickPoint = e.GetPosition(_sbHost);

            var saturation = pickPoint.X / _sbHost.ActualWidth;
            var brightness = 1 - (pickPoint.Y / _sbHost.ActualHeight);

            saturation = Clamp(saturation, 0, 1);
            brightness = Clamp(brightness, 0, 1);

            SetCurrentValue(HSBProperty, new HSB(HSB.Hue, saturation, brightness));

            ChangePosition();
        }

        private void ChangePosition()
        {
            if(_hueHost == null || _sbHost == null)
            {
                return;
            }

            Canvas.SetTop(_huePicker, _hueHost.ActualHeight * HSB.Hue / 360.0);
            Canvas.SetTop(_sbPicker, _sbHost.ActualHeight * (1 - HSB.Brightness));
            Canvas.SetLeft(_sbPicker, _sbHost.ActualWidth * HSB.Saturation);
        }

        private double Clamp(double value, double min, double max)
        {
            if (value > max)
            {
                return max;
            }

            if (value < min)
            {
                return min;
            }

            return value;
        }

        private static void ColorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var colorPicker = d as MiniColorPicker;

            if (!colorPicker._canChangeProperty)
            {
                return;
            }

            colorPicker._canChangeProperty = false;

            var color = (Color)e.NewValue;

            colorPicker.SetCurrentValue(HSBProperty, ColorConverter.ToHSB(color));
            colorPicker.ChangePosition();

            colorPicker._canChangeProperty = true;
        }

        private static void HSBPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var colorPicker = d as MiniColorPicker;

            if (!colorPicker._canChangeProperty)
            {
                return;
            }

            colorPicker._canChangeProperty = false;

            var hsb = (HSB)e.NewValue;

            colorPicker.SetCurrentValue(ColorProperty, ColorConverter.ToRGB(hsb));
            colorPicker.ChangePosition();

            colorPicker._canChangeProperty = true;
        }
    }
}
