using System.Windows;

namespace MiniWpfHelperTools
{
    /// <summary>
    /// MiniBusyIndicatorPopup.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MiniBusyIndicatorPopup : Window, IDialogWindow
    {
        public IDialogModel Model
        {
            get => DataContext as IDialogModel;
            set => DataContext = value;
        }

        public MiniBusyIndicatorPopup()
        {
            InitializeComponent();
            Owner = Application.Current.MainWindow;
        }

        private void OnCancelButtonClicked(object sender, RoutedEventArgs e)
        {
            Close();            
        }
    }
}
