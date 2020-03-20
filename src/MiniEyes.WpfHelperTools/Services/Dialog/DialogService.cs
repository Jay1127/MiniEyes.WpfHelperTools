using System;
using System.Threading.Tasks;
using System.Windows;

namespace MiniEyes.WpfHelperTools
{
    public class DialogService : IDialogService
    {
        public DialogService()
        {

        }

        public void ShowDialog<T>(object dataContext, bool isModal = false, Action closed = null) where T : Window, new()
        {
            T dialog = new T();

            if (closed != null)
            {
                dialog.Closed += (s, e) => closed();
            }

            if (isModal)
            {
                dialog.ShowDialog();
            }
            else
            {
                dialog.Show();
            }
        }

        public async void ShowDialogAsync<T>(object dataContext, bool isModal = false, Action closed = null) where T : Window, new()
        {
            T dialog = new T();

            if (closed != null)
            {
                dialog.Closed += (s, e) => closed();
            }

            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                if (isModal)
                {
                    dialog.ShowDialog();
                }
                else
                {
                    dialog.Show();
                }
            });
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

        public void ShowDialogAsync<T>(IDialogModel model, Action closed = null) where T : IDialogWindow, new()
        {
            IDialogWindow dialog = CreateDialog<T>(model, closed);
            if (model.IsModal)
            {
                ShowModalDialog(dialog);
            }
            else
            {
                ShowModalessDialog(dialog);
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

        private async void ShowModalessDialog(IDialogWindow dialog)
        {
            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                dialog.Show();
            });
        }

        private async void ShowModalDialog(IDialogWindow dialog)
        {
            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                dialog.ShowDialog();
            });
        }
    }
}