namespace Nhr.Tests
{
    using System.IO;
    using System.Linq;

    using Nhr.Tests.Helpers;

    using Shouldly;

    using Xunit;
    
    public class PackagesReadingTests
    {
        [Fact]
        public void sample_packages_file_shall_be_parsed_without_errors()
        {
            Packages packages;
            using (StreamReader stream = new StreamReader(TestTools.OpenResourceStream(@"Resources.packages.xml.test")))
            {
                packages = Packages.FromFile(stream);
            }

            packages.ShouldNotBeNull();
        }

        [Fact]
        public void parsed_sample_packages_file_shall_find_correct_number_of_packages()
        {
            Packages packages;
            using (StreamReader stream = new StreamReader(TestTools.OpenResourceStream(@"Resources.packages.xml.test")))
            {
                packages = Packages.FromFile(stream);
            }

            packages.Entries.Count.ShouldBe(7);
        }

        [Fact]
        public void parsed_sample_packages_file_shall_render_correct_entries()
        {
            Packages packages;
            using (StreamReader stream = new StreamReader(TestTools.OpenResourceStream(@"Resources.packages.xml.test")))
            {
                packages = Packages.FromFile(stream);
            }

            var e1 = packages.Entries.SingleOrDefault(e => e.PackageId.Equals(@"Shouldly"));
            e1.ShouldNotBeNull();
            e1.VersionStringRaw.ShouldBe(@"2.8.3");
            e1.TargetFramework.ShouldBe(@"net461");

            var e2 = packages.Entries.SingleOrDefault(e => e.PackageId.Equals(@"xunit.abstractions"));
            e2.ShouldNotBeNull();
            e2.VersionStringRaw.ShouldBe(@"2.0.1");
            e2.TargetFramework.ShouldBe(@"net461");
        }

        // TODO: test min/max versions
    }
}
