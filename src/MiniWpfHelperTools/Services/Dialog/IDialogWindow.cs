using System;

namespace MiniWpfHelperTools
{
    public interface IDialogWindow
    {
        IDialogModel Model { get; set; }

        event EventHandler Closed;

        void Show();

        bool? ShowDialog();

        void Close();
    }
}