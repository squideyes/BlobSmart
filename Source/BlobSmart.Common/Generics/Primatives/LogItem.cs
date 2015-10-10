using System;

namespace BlobSmart.Common.Generics
{
    public class LogItem
    {
        public LogItem(LogItemKind kind, string format, params object[] args)
        {
            Contract.Requires(kind.IsDefinedEnumValue(), nameof(kind));
            Contract.Requires(!string.IsNullOrWhiteSpace(format), nameof(format));

            Kind = kind;
            LoggedOn = DateTime.UtcNow;
            Message = string.Format(format, args);
        }

        public LogItemKind Kind { get; private set; }
        public DateTime LoggedOn { get; private set; }
        public string Message { get; private set; }

        public override string ToString()
        {
            return string.Format("{0:MM/dd/yyyy HH:mm:ss.fff}  {1,-7}  {2}", 
                LoggedOn, Kind.ToString().ToUpper(), Message);
        }
    }
}
