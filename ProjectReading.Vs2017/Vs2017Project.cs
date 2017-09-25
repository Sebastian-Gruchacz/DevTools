namespace ProjectReading.Vs2017
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Linq;

    using Microsoft.Build.Evaluation;
    using Microsoft.Build.Tasks;

    using Nhr.Interfaces;

    internal class Vs2017Project : IProjectData
    {
        public string Name { get; private set; }

        public string Path { get; private set; }

        public ICollection<ProjectReference> ProjectReferences { get; private set; } = new List<ProjectReference>();

        public ICollection<LibraryReference> References { get; private set; } = new List<LibraryReference>();

        public Vs2017Project(Project proj)
        {
            if (!System.IO.Path.IsPathRooted(proj.FullPath))
            {
                throw new ConfigurationException("Project path is not rooted: " + proj.FullPath);
            }

            this.Path = proj.FullPath;
            this.Name = System.IO.Path.GetFileNameWithoutExtension(proj.FullPath);
            this.GetReferences(proj);
            this.GetProjectReferences(proj);
        }

        private void GetProjectReferences(Project proj)
        {
            string thisProjectDirectory = System.IO.Path.GetDirectoryName(this.Path);
            if (string.IsNullOrWhiteSpace(thisProjectDirectory))
            {
                throw new ConfigurationException("Invalid project path: " + this.Path);
            }

            foreach (var item in proj.Items.Where(i => i.ItemType.Equals(@"ProjectReference")))
            {
                var declaredPath = item.EvaluatedInclude;
                var mappedPath = System.IO.Path.Combine(thisProjectDirectory, declaredPath);
                var unescappedPath = new FileInfo(mappedPath).FullName;

                ProjectReference projRef;
                if (File.Exists(unescappedPath))
                {
                    projRef = new ProjectReference
                    {
                        Found = true,
                        Path = unescappedPath
                    };
                }
                else
                {
                    projRef = new ProjectReference
                    {
                        Found = false,
                        Path = declaredPath
                    };
                }

                this.ProjectReferences.Add(projRef);
            }
        }

        private void GetReferences(Project proj)
        {
            foreach (var item in proj.Items.Where(i => i.ItemType.Equals(@"Reference")))
            {
                this.References.Add(new LibraryReference
                {
                    Name = this.ExtractLibraryName(item.EvaluatedInclude),
                    Version = this.ExtractVersionInfo(item.EvaluatedInclude),
                    Hint = item.DirectMetadata.SingleOrDefault(meta => meta.Name.Equals(@"HintPath"))?.EvaluatedValue
                });
            }
        }

        private Version ExtractVersionInfo(string includeString)
        {
            var includeParts = includeString.Split(',');
            if (includeParts.Length > 1)
            {
                var pieces = includeParts[1].Split('=');
                if (pieces.Length == 2 && pieces[0].Trim().Equals(@"version", StringComparison.OrdinalIgnoreCase))
                {
                    return Version.Parse(pieces[1].Trim());
                }
            }

            return null;
        }

        private string ExtractLibraryName(string includeString)
        {
            var includeParts = includeString.Split(',');
            return includeParts[0].Trim();
        }

        string GetRelativePath(string filespec, string folder)
        {
            Uri pathUri = new Uri(filespec, UriKind.RelativeOrAbsolute);

            // Folders must end in a slash
            if (!folder.EndsWith(System.IO.Path.DirectorySeparatorChar.ToString()))
            {
                folder += System.IO.Path.DirectorySeparatorChar;
            }

            Uri folderUri = new Uri(folder);
            return Uri.UnescapeDataString(
                folderUri.MakeRelativeUri(pathUri)
                .ToString()
                .Replace('/', System.IO.Path.DirectorySeparatorChar));
        }
    }
}
