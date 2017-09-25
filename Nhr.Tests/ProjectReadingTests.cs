namespace Nhr.Tests
{
    using Nhr.Core.Analyze;

    using ProjectReading.Vs2017;
    using Nhr.Interfaces;
    using Xunit;

    public class ProjectReadingTests
    {
        [Fact]
        public void try_loading_project_file()
        {
            string testPath = @"J:\GitHub\CuttingEdge.Conditions\CuttingEdge.Conditions.UnitTests\CuttingEdge.Conditions.UnitTests.csproj";
            ProjectLoader loader = new ProjectLoader();
            IProjectData projInfo = loader.TryLoadingProject(testPath);

            var analyzer = new AnalyzeProject(projInfo);
            var libraryList = analyzer.GetSimpleReferencesAnalyze(loader);
        }
    }
}
