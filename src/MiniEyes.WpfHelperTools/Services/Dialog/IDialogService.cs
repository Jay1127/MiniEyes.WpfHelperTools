using System;
using System.Threading.Tasks;
using System.Windows;

namespace MiniEyes.WpfHelperTools
{
    public interface IDialogService
    {
        void ShowDialog<T>(object dataContext, bool isModal = false, Action closed = null) where T : Window, new();
        Task ShowDialogAsync<T>(object dataContext, bool isModal = false, Action closed = null) where T : Window, new();

        void ShowDialog<T>(IDialogModel model, Action closed = null) where T : IDialogWindow, new();
        Task ShowDialogAsync<T>(IDialogModel model, Action closed = null) where T : IDialogWindow, new();
    }
}