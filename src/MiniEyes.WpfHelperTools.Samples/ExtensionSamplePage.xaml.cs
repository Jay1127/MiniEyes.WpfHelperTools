using System.Collections.Generic;
using System.Windows.Controls;

namespace MiniEyes.WpfHelperTools
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
        public List<string> Items { get; set; }

        public ExtensionSamplePage()
        {
            InitializeComponent();            

            this.DataContext = this;

            Items = new List<string>() { "A", "B", "C", "D", "E" };
        }
    }
}
