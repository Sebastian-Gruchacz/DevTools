namespace Nhr.Interfaces
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Contains information about referenced library
    /// </summary>
    [DebuggerDisplay(@"{Name} ({Version})")]
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

        /// <summary>
        /// Required Framework setting for System references
        /// </summary>
        public string MetaRequiredFramework { get; set; }
    }
}