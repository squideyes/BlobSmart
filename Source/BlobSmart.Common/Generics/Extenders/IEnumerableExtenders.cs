using System.Collections.Generic;
using System.Linq;

namespace BlobSmart.Common.Generics
{
    public static partial class Extenders
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> items)
        {
            return (items == null) || (!items.Any());
        }

        public static bool IsOrdered<T>(this IEnumerable<T> items)
        {
            if (items == null)
                return false;

            var comparer = Comparer<T>.Default;
         
            var previous = default(T);
            
            bool first = true;

            foreach (T element in items)
            {
                if ((!first) && (comparer.Compare(previous, element) > 0))
                    return false;
            
                first = false;

                previous = element;
            }

            return true;
        }

        public static bool IsUnique<T>(this IEnumerable<T> items)
        {
            if (items == null)
                return false;

            var diffChecker = new HashSet<T>();

            return items.All(diffChecker.Add);
        }
    }
}
