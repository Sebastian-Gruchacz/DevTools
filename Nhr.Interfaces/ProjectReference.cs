namespace Nhr.Interfaces
{
    /// <summary>
    /// Contains information about project referenced from within another project
    /// </summary>
    public class ProjectReference
    {
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