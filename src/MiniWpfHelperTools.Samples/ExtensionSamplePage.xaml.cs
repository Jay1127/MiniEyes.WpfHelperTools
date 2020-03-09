using System.Windows.Controls;

namespace MiniWpfHelperTools.Samples
{
    public enum ImageExtension
    {
        Jpg,
        Png,
        Gif,
        Bmp,
        Ico
    }

    /// <summary>
    /// ExtensionSamplePages.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ExtensionSamplePage : UserControl
    {
        public ExtensionSamplePage()
        {
            InitializeComponent();            

            this.DataContext = this;
        }
    }
}
