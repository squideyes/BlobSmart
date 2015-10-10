using System;
using System.IO;

namespace BlobSmart.Common.Generics
{
    public static partial class Extenders
    {
        public static int GetLittleEndianInt32(this BinaryReader reader)
        {
            Contract.Requires(reader != null, nameof(reader));

            var bytes = reader.ReadBytes(4);

            Array.Reverse(bytes);

            return BitConverter.ToInt32(bytes, 0);
        }
    }
}
