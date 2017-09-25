namespace Nhr.Interfaces
{
    using System.Diagnostics;

    /// <summary>
    /// The content file.
    /// </summary>
    [DebuggerDisplay(@"{Name}")]
    public class ContentFile
    {
        public string Name { get; set; }

        public string Extension { get; set; }

        public string FullName => string.Join(".", this.Name, this.Extension);

        public string Path { get; set; }

        public bool Exists { get; set; }
    }
}