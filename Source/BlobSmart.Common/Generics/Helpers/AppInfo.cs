using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BlobSmart.Common.Generics
{
    public class AppInfo
    {
        public AppInfo(Assembly assembly)
        {
            Contract.Requires(assembly != null, nameof(assembly));

            Company = GetEntryCompany(assembly);
            Product = GetEntryProduct(assembly);
            Version = assembly.GetName().Version;
        }

        private string Company { get; set; }
        private string Product { get; set; }
        private Version Version { get; set; }

        public string GetTitle(string extraInfo = null)
        {
            var sb = new StringBuilder();

            sb.Append(Product);

            if (!string.IsNullOrWhiteSpace(extraInfo))
            {
                sb.Append(" (");
                sb.Append(extraInfo);
                sb.Append(')');
            }

            sb.Append(" v");
            sb.Append(Version.Major);
            sb.Append('.');
            sb.Append(Version.Minor);

            if ((Version.Build != 0) || (Version.Revision != 0))
            {
                sb.Append('.');
                sb.Append(Version.Build);
            }

            if (Version.Revision == 0) 
                return sb.ToNewString();

            sb.Append('.');
            sb.Append(Version.Revision);

            return sb.ToNewString();
        }

        private static string GetEntryProduct(Assembly assembly)
        {
            return assembly.GetAttribute<AssemblyProductAttribute>().Product;
        }

        private static string GetEntryCompany(Assembly assembly)
        {
            return assembly.GetAttribute<AssemblyCompanyAttribute>().Company;
        }

        private string CleanUp(string value)
        {
            return Path.GetInvalidFileNameChars().Aggregate(value,
                (current, c) => current.Replace(c.ToString(), " ")).Trim();
        }

        public string GetLocalAppDataPath(params string[] subFolders)
        {
            Contract.Requires(subFolders != null, nameof(subFolders));
            Contract.Requires(subFolders.Length >= 1, nameof(subFolders));
            Contract.Requires(subFolders.All(sf => sf.IsTrimmed()), nameof(subFolders));

            var path = Path.Combine(Environment.GetFolderPath(
               Environment.SpecialFolder.LocalApplicationData),
               CleanUp(Company), CleanUp(Product.Replace("™", "")));

            foreach (var subFolder in subFolders)
                path = Path.Combine(path, CleanUp(subFolder));

            return path;
        }
    }
}
