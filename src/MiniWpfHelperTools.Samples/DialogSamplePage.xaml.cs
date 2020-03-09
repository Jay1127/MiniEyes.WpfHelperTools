using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
