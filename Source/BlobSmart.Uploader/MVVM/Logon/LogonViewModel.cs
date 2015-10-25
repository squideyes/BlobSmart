using BlobSmart.GUI;

namespace BlobSmart.Uploader
{
    public class LogonViewModel : NotifyBase<LogonViewModel>
    {
        public LogonViewModel(LogonModel model)
        {
            Model = model;
        }

        public LogonModel Model { get; }
    }
}
