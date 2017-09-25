namespace Nhr.Interfaces
{
    using System.Diagnostics;

    /// <summary>
    /// Contains information about project referenced from within another project
    /// </summary>
    [DebuggerDisplay(@"{Name}")]
    public class ProjectReference
    {
        /// <summary>
        /// Gets or sets name of referenced project
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets rooted path to referenced project
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether referenced project file exists
        /// </summary>
        public bool Found { get; set; }
    }
}