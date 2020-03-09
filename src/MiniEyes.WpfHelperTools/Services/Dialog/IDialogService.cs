using System;

namespace MiniEyes.WpfHelperTools
{
    public interface IDialogService
    {
        void ShowDialog<T>(IDialogModel model, Action closed = null) where T : IDialogWindow, new();
        void ShowDialogAsync<T>(IDialogModel model, Action closed = null) where T : IDialogWindow, new();
    }
}