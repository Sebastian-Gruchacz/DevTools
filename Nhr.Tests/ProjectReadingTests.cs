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
            string testPath = @"C:\SRC\StructuredEcoSystem\DTViewerBackend\src\ResultTracker.WebAPI\ResultTracker.WebAPI.csproj";
            ProjectLoader loader = new ProjectLoader();
            IProjectData projInfo = loader.TryLoadingProject(testPath);
        }
    }
}
