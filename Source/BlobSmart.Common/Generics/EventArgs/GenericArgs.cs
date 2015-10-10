using System;

namespace BlobSmart.Common.Generics
{
    public class GenericArgs<T> : EventArgs where T : class
    {
        public GenericArgs(T item)
        {
            Contract.Requires(item != null, nameof(item));

            Item = item;
        }

        public T Item { get; private set; }
    }
}
