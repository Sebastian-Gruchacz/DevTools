namespace Nhr.Core.Model
{
    using System;

    public class BindingRedirection
    {
        public BindingRedirection(string oldVersion, string newVersion)
        {
            string[] oldVersions = oldVersion.Split('-');

            if (oldVersions.Length > 2)
            {
                throw new FormatException("<bindingRedirectNode>[oldVersion] cannot contain more than two version elements.");    
            }

            if (oldVersions.Length == 2)
            {
                this.OldVersionMin = Version.Parse(oldVersions[0]);
                this.OldVersionMax = Version.Parse(oldVersions[1]);
            }
            else
            {
                this.OldVersionMin = this.OldVersionMax = Version.Parse(oldVersions[0]);
            }

            this.NewVersion = Version.Parse(newVersion);
        }

        public Version NewVersion { get; set; }

        public Version OldVersionMax { get; set; }

        public Version OldVersionMin { get; set; }
    }
}