namespace Nhr.Tests
{
    using ProjectReading.Vs2017;
    using Nhr.Interfaces;
    using Xunit;

    public class ProjectReadingTests
    {
        [Fact]
        public void try_loading_project_file()
        {
            string testPath = @"J:\GitHub\CuttingEdge.Conditions\CuttingEdge.Conditions\CuttingEdge.Conditions.csproj";
            ProjectLoader loader = new ProjectLoader();
            IProjectData projInfo = loader.TryLoadingProject(testPath);
        }
    }
}
