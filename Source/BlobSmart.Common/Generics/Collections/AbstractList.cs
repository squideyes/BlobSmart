using System.Collections;
using System.Collections.Generic;

namespace BlobSmart.Common.Generics
{
    public abstract class AbstractList<T> : IEnumerable<T>
    {
        public readonly List<T> Items = new List<T>();

        public int Count
        {
            get { return Items.Count; }
        }

        public T this[int index]
        {
            get { return Items[index]; }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}