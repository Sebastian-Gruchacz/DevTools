namespace Nhr.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    /// Contains project data parsed from project file itself
    /// </summary>
    public interface IProjectData
    {
        /// <summary>
        /// Gets name of the project
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets rooted path to the project
        /// </summary>
        string Path { get;  }

        /// <summary>
        /// Gets collection of referenced projects
        /// </summary>
        ICollection<ProjectReference> ProjectReferences { get; }

        /// <summary>
        /// Gets collection of libraries referenced in the project file
        /// </summary>
        ICollection<LibraryReference> References { get; }
    }
}