using System;

namespace BlobSmart.Common.Generics
{
    public class LogItemArgs : EventArgs
    {
        public LogItemArgs(LogItem logItem)
        {
            Contract.Requires(logItem != null, nameof(logItem));

            Item = logItem;
        }

        public LogItem Item { get; private set; }
    }
}
