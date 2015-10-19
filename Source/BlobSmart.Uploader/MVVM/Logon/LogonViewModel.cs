using BlobSmart.GUI;
using System;
using System.Windows.Input;

namespace BlobSmart.Uploader
{
    public class LogonViewModel : NotifyBase<LogonViewModel>
    {
        private ICommand logonCommand;
        private ICommand cancelCommand;

        public event EventHandler<NotifyArgs> OnCancel;

        public LogonViewModel(LogonModel model)
        {
            Model = model;

            Model.PropertyChanged += (s, e) =>
            {
                if (IsProperty<LogonModel>(e, m => m.UserName))
                    NotifyPropertyChanged(vm => vm.LogonCommand);
            };
        }

        public LogonModel Model { get; }

        public ICommand LogonCommand
        {
            get
            {
                return logonCommand ?? (logonCommand = new DelegateCommand(
                    () =>
                    {
                    },
                    () => Model.IsValid()));
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                return cancelCommand ?? (cancelCommand = new DelegateCommand(
                    () => RaiseNotifyEvent(OnCancel)));
            }
        }
    }
}
