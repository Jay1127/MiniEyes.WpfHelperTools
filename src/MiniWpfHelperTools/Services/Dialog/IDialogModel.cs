using System;

namespace MiniWpfHelperTools
{
    public interface IDialogModel
    {
        string Title { get; set; }

        string Message { get; set; }

        bool IsShown { get; set; }

        event EventHandler RequestClose;
    }
}