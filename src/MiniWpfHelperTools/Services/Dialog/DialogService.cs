using System;
using System.Threading.Tasks;
using System.Windows;

namespace MiniWpfHelperTools
{
    public class DialogService : IDialogService
    {
        public DialogService()
        {

        }

        public async void ShowDialog<T>(IDialogModel model, Action closed = null) where T : IDialogWindow, new()
        {
            IDialogWindow dialog = CreateDialog<T>(model, closed);

            await ShowDialog(dialog);
        }

        public void ShowDialogAsync<T>(IDialogModel model, Action closed = null) where T : IDialogWindow, new()
        {
            IDialogWindow dialog = CreateDialog<T>(model, closed);

            dialog.Show();
        }

        private IDialogWindow CreateDialog<T>(IDialogModel model, Action closed) where T : IDialogWindow, new()
        {
            IDialogWindow dialog = new T
            {
                Model = model                
            };

            model.IsShown = true;
            model.RequestClose += (s, e) => dialog.Close();

            if (closed != null)
            {
                dialog.Closed += (s, e) => closed();
            }

            return dialog;
        }

        private async Task ShowDialog(IDialogWindow dialog)
        {
            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                dialog.Show();
            });
        }
    }
}