using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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