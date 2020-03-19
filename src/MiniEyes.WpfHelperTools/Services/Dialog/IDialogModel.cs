using System;

namespace MiniEyes.WpfHelperTools
{
    public interface IDialogModel
    {
        string Title { get; set; }

        string Message { get; set; }

        bool IsShown { get; set; }

        bool IsModal { get; }

        event EventHandler RequestClose;
    }
}