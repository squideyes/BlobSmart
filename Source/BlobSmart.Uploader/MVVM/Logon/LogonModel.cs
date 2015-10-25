using BlobSmart.GUI;
using System;

namespace BlobSmart.Uploader
{
    public class LogonModel : NotifyBase<LogonModel>
    {
        private Uri uri;
        private Exception error;

        public Uri Uri
        {
            get
            {
                return uri;
            }
            set
            {
                uri = value;

                NotifyPropertyChanged(m => m.Uri);
            }
        }

        public Exception Error
        {
            get
            {
                return error;
            }
            set
            {
                error = value;

                NotifyPropertyChanged(m => m.Error);
            }
        }
    }
}
