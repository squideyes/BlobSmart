using BlobSmart.GUI;

namespace BlobSmart.Uploader
{
    public class LogonModel : NotifyBase<LogonModel>
    {
        private string userName;
        private string password;

        public LogonModel()
        {
            UserName = "louis";
        }

        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;

                NotifyPropertyChanged(m => m.UserName);
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;

                NotifyPropertyChanged(m => m.Password);
            }
        }
    }
}
