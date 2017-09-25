namespace ProjectReading.Vs2017
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
    using Microsoft.Build.Evaluation;
    using Nhr.Interfaces;

    public class ProjectLoader : IProjectLoader, IDisposable
    {
        private readonly ProjectCollection _collection = new ProjectCollection();
        private readonly Dictionary<string, string> _parameters;
        private readonly string _toolsVersion = null;

        public ProjectLoader()
        {
            // https://github.com/Microsoft/msbuild/blob/60c73c914c01c62f5496c6d04683f77e967bbc68/src/Shared/BuildEnvironmentHelper.cs#L39-L92

            //Microsoft.Build.Utilities.ToolLocationHelper.
            //var msBuildFile = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
            //    ? "MSBuild.exe"
            //    : "MSBuild";
            //string dotNetCoreSdkPath = @"C:\Program Files\dotnet\sdk\NuGetFallbackFolder\microsoft.build.runtime\15.3.409\contentFiles\any\net46";

            //// workaround https://github.com/Microsoft/msbuild/issues/999
            // Environment.SetEnvironmentVariable("MSBUILD_EXE_PATH", Path.Combine(dotNetCoreSdkPath, msBuildFile));

            ////bool runningInVisualStudio;
            ////var visualStudioPath = FindVisualStudio(
            ////    new[] { processNameCommandLine, processNameCurrentProcess },
            ////    out runningInVisualStudio);

            ////string msbuildFromVisualStudioRoot = null;
            ////if (!string.IsNullOrEmpty(visualStudioPath))
            ////{
            ////    msbuildFromVisualStudioRoot = FileUtilities.CombinePaths(visualStudioPath, "MSBuild", "15.0", "Bin");
            ////}

            this._parameters = new Dictionary<string, string>
            {
                { "DesignTimeBuild", "true" },
                { "Configuration", "Debug" },
              //  { "MSBuildExtensionsPath",  dotNetCoreSdkPath },
            };
        }

        public IProjectData TryLoadingProject(string projectPath)
        {
            var knownProject = (this._collection.LoadedProjects.SingleOrDefault(p => p.FullPath.Equals(projectPath)));
            if (knownProject != null)
            {
                return new Vs2017Project(knownProject);
            }

            Project proj = new Project(projectPath, this._parameters, this._toolsVersion, this._collection);

            return new Vs2017Project(proj);
        }

        public void Dispose()
        {
            this._collection?.Dispose();
        }
    }
}