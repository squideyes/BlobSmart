using System;

namespace BlobSmart.Common.Contracts
{
    public class SasResult
    {
        public Guid Guid { get; set; }
        public Uri UriWithSas { get; set; }
    }
}
