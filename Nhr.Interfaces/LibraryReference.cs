namespace Nhr.Interfaces
{
    using System;

    /// <summary>
    /// Contains information about referenced library
    /// </summary>
    public class LibraryReference
    {
        /// <summary>
        /// Gets or sets library name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets library version (if any)
        /// </summary>
        public Version Version { get; set; }

        /// <summary>
        /// Gets or sets library hint location data (if any)
        /// </summary>
        public string Hint { get; set; }
    }
}