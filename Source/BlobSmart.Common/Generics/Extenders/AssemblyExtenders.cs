using System;
using System.Reflection;

namespace BlobSmart.Common.Generics
{
    public static partial class Extenders
    {
        public static T GetAttribute<T>(this Assembly callingAssembly)
            where T : Attribute
        {
            T result = null;

            var configAttributes = Attribute.
                GetCustomAttributes(callingAssembly, typeof(T), false);

            if (!configAttributes.IsNullOrEmpty())
                result = (T)configAttributes[0];

            return result;
        }
    }
}
