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

        public void ShowDialog<T>(IDialogModel model, Action closed = null) where T : IDialogWindow, new()
        {
            IDialogWindow dialog = CreateDialog<T>(model, closed);

            if (model.IsModal)
            {
                dialog.ShowDialog();
            }
            else
            {
                dialog.Show();
            }
        }

        public async void ShowDialogAsync<T>(IDialogModel model, Action closed = null) where T : IDialogWindow, new()
        {
            IDialogWindow dialog = CreateDialog<T>(model, closed);
            if (model.IsModal)
            {
                await ShowModalDialog(dialog);
            }
            else
            {
                await ShowModalessDialog(dialog);
            }
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

        private async Task ShowModalessDialog(IDialogWindow dialog)
        {
            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                dialog.Show();
            });
        }

        private async Task ShowModalDialog(IDialogWindow dialog)
        {
            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                dialog.ShowDialog();
            });
        }
    }
}