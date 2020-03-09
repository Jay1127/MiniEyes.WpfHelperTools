using System.Windows.Controls;

namespace MiniEyes.WpfHelperTools
{
    /// <summary>
    /// ColorPickerSamplePage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ColorPickerSamplePage : UserControl
    {
        public ColorPickerSamplePage()
        {
            InitializeComponent();

            this.DataContext = new ColorPickerViewModel();
        }
    }
}
