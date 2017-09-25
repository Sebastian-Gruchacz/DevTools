namespace Nhr.Core.Model
{
    using System.Diagnostics;

    [DebuggerDisplay(@"{PackageId} ({VersionStringRaw}) ")]
    public class PackageEntry
    {
        private string versionStringRaw;

        public string PackageId { get; set; }

        public string PackageName { get; set; } // To be resolved from package source

        public string VersionStringRaw
        {
            get
            {
                return this.versionStringRaw;
            }
            set
            {
                this.versionStringRaw = value;
                this.ParseVersionString(value);
            }
        }

        public PackageVersion MinVersion { get; set; }

        public PackageVersion MaxVersion { get; set; }

        public string TargetFramework { get; set; }

        // TODO: add ref to package source when discovered?

        private void ParseVersionString(string value)
        {
            
        }
    }
}