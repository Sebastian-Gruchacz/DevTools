namespace Nhr.Core.Analyze
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Nhr.Core.Model;
    using Nhr.Interfaces;
    using ProjectReading.Vs2017;

    public class AnalyzeProject
    {
        private readonly IProjectData project;

        public Packages Packages { get; private set; }

        public ApplicationConfiguration AppConfig { get; private set; }

        public AnalyzeProject(IProjectData project)
        {
            // TODO: recognize and resolve "new" project (no packages/app.config files, nor mentioned in csproj)
            this.project = project;
            this.Initialize();
        }

        private void Initialize()
        {
            this.ReadPackagesIfAvailable();
            this.ReadAppConfigOverridesIfAvailable();

        }

        private void ReadPackagesIfAvailable()
        {
            var packagesFile = this.project.ContentFiles.SingleOrDefault(f =>
                f.Name.Equals(@"packages", StringComparison.OrdinalIgnoreCase) &&
                f.Extension.Equals(@"config", StringComparison.OrdinalIgnoreCase));

            if (packagesFile != null && packagesFile.Exists)
            {
                using (var stream = new StreamReader(packagesFile.Path))
                {
                    this.Packages = Packages.FromFile(stream);
                }
            }
            else
            {
                this.Packages = Packages.Empty;
            }
        }

        private void ReadAppConfigOverridesIfAvailable()
        {
            var appConfig =
                this.project.ContentFiles.SingleOrDefault(
                    f => f.FullName.Equals(@"app.config", StringComparison.OrdinalIgnoreCase));
            
            if (appConfig != null && appConfig.Exists)
            {
                using (var stream = new StreamReader(appConfig.Path))
                {
                    this.AppConfig = ApplicationConfiguration.FromFile(stream);
                }
            }
            else
            {
                this.AppConfig = ApplicationConfiguration.Empty;
            }
        }

        public ReferencesAnalyze GetSimpleReferencesAnalyze(ProjectLoader loader)
        {
            var allRelatedProjects = this.LoadReferencedProjects(this.project, loader);
            

            return null;
        }

        private IEnumerable<IProjectData> LoadReferencedProjects(IProjectData parentproject, ProjectLoader loader)
        {
            var projects = new List<IProjectData>();
            
            foreach (var projectReference in parentproject.ProjectReferences)
            {
                var proj = loader.TryLoadingProject(projectReference.Path);
                projects.Add(proj);
                var subProjects = this.LoadReferencedProjects(proj, loader);
                projects.AddRange(subProjects);
            }

            return projects;
        }
    }

    public class ReferencesAnalyze
    {
    }
}
