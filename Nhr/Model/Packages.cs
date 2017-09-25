namespace Nhr.Core.Model
{
    using System.Collections.Generic;

    using System.IO;
    using System.Xml;

    using Conditions;

    using Nhr.Core.Helpers;

    /// <summary>
    /// Contains direct outcome from reading 'packages.config' file
    /// </summary>
    public class Packages
    {
        private Packages()
        {
            
        }

        public ICollection<PackageEntry> Entries { get; private set; } = new List<PackageEntry>();
        

        public static Packages FromFile(StreamReader stream)
        {
            stream.Requires(nameof(stream)).IsNotNull();

            var result = new Packages();

            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(stream);
            var selectNodes = xdoc.SelectNodes(@"/packages/package");
            if (selectNodes != null)
            {
                foreach (XmlNode packageNode in selectNodes)
                {
                    result.Entries.Add(ReadPackageInfo(packageNode));
                }
            }

            return result;
        }

        // <package id="Shouldly" version="2.8.3" targetFramework="net461" />
        private static PackageEntry ReadPackageInfo(XmlNode packageNode)
        {
            packageNode.Requires(nameof(packageNode)).IsNotNull();

            return new PackageEntry
            {
                PackageId = packageNode.ReadAttribute(@"id", required: true),
                VersionStringRaw = packageNode.ReadAttribute(@"version", required: true),
                TargetFramework = packageNode.ReadAttribute(@"targetFramework", required: true)
            };
        }
    }
}
