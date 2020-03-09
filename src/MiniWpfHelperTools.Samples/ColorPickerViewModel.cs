using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace MiniWpfHelperTools.Samples
{
    public class ColorPickerViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private HSB _hsb;
        private Color _rgb;

        public HSB HSB
        {
            get => _hsb;
            set
            {
                _hsb = value;
                NotifyPropertyChanged();
            }
        }
        
        public Color RGB
        {
            get => _rgb;
            set
            {
                _rgb = value;
                Color = new SolidColorBrush(RGB);
                NotifyPropertyChanged();
                NotifyPropertyChanged(nameof(Color));
            }
        }

        public SolidColorBrush Color { get; set; }

        public ColorPickerViewModel()
        {
        }

        private void NotifyPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}