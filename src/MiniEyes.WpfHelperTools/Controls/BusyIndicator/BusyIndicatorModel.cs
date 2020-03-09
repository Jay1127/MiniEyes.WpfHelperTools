using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MiniEyes.WpfHelperTools
{
    public class BusyIndicatorModel : IDialogModel, INotifyPropertyChanged
    {
        private string _title;
        private string _message;
        private bool _isShown;

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                NotifyPropertyChanged();
            }
        }

        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                NotifyPropertyChanged();
            }
        }

        public bool IsShown
        {
            get => _isShown;
            set
            {
                bool requestClose = _isShown && (value == false);

                _isShown = value;

                if (requestClose)
                {
                    RequestClose?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public bool IsModal { get; set; }

        public ICommand CancelCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler RequestClose;

        public BusyIndicatorModel(string title = "", string content = "")
        {
            Title = title;
            Message = content;
            IsShown = false;
            IsModal = true;
        }

        private void NotifyPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}