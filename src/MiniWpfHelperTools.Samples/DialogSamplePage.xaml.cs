using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MiniWpfHelperTools.Samples
{
    /// <summary>
    /// DialogSamplePage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DialogSamplePage : UserControl
    {
        public DialogSamplePage()
        {
            InitializeComponent();
        }

        private async void ShowModalessDialogAsync(object sender, RoutedEventArgs e)
        {
            var model = new BusyIndicatorModel("Busy Indicator executing", "Is Busy...");
            model.IsModal = false;

            var dialogService = new DialogService();
            dialogService.ShowDialogAsync<MiniBusyIndicatorPopup>(model);

            await Task.Delay(3000);

            model.IsShown = false;
        }

        private async void ShowModalDialogAsync(object sender, RoutedEventArgs e)
        {
            var model = new BusyIndicatorModel("Busy Indicator executing", "Is Busy...");
            model.IsModal = true;

            var dialogService = new DialogService();
            dialogService.ShowDialogAsync<MiniBusyIndicatorPopup>(model);

            await Task.Delay(3000);

            model.IsShown = false;
        }
    }
}
