namespace Nhr.Tests
{
    using System;
    using System.IO;
    using System.Linq;
    using Nhr.Tests.Helpers;

    using Shouldly;

    using Xunit;

    public class AppConfigReadingTests
    {
        [Fact]
        public void sample_appconfig_file_shall_be_parsed_without_errors()
        {
            ApplicationConfiguration configuration;
            using (StreamReader stream = new StreamReader(TestTools.OpenResourceStream(@"Resources.app.config.test")))
            {
                configuration = ApplicationConfiguration.FromFile(stream);
            }

            configuration.ShouldNotBeNull();
        }

        [Fact]
        public void parsed_sample_appconfig_file_shall_find_correct_number_of_packages()
        {
            ApplicationConfiguration configuration;
            using (StreamReader stream = new StreamReader(TestTools.OpenResourceStream(@"Resources.app.config.test")))
            {
                configuration = ApplicationConfiguration.FromFile(stream);
            }

            configuration.Entries.Count.ShouldBe(14);
        }

        [Fact]
        public void parsed_sample_appconfig_file_shall_render_correct_entries()
        {
            ApplicationConfiguration configuration;
            using (StreamReader stream = new StreamReader(TestTools.OpenResourceStream(@"Resources.app.config.test")))
            {
                configuration = ApplicationConfiguration.FromFile(stream);
            }

            /*
             <dependentAssembly>
                <assemblyIdentity name="Microsoft.Owin" publicKeyToken="31bf3856ad364e35" culture="neutral" />
                <bindingRedirect oldVersion="0.0.0.0-3.1.0.0" newVersion="3.1.0.0" />
             </dependentAssembly>
            */
            var e1 = configuration.Entries.SingleOrDefault(e => e.AssemblyName.Equals(@"Microsoft.Owin"));
            e1.ShouldNotBeNull();
            e1.BindingRedirection.OldVersionMin.ShouldBe(Version.Parse(@"0.0.0.0"));
            e1.BindingRedirection.OldVersionMax.ShouldBe(Version.Parse(@"3.1.0.0"));
            e1.BindingRedirection.NewVersion.ShouldBe(Version.Parse(@"3.1.0.0"));
            
            /* 
              <dependentAssembly>
                <assemblyIdentity name="System.IdentityModel.Tokens.Jwt" publicKeyToken="31bf3856ad364e35" culture="neutral" />
                <bindingRedirect oldVersion="0.0.0.0-4.0.20622.1351" newVersion="4.0.20622.1351" />
              </dependentAssembly>
             */
            var e2 = configuration.Entries.SingleOrDefault(e => e.AssemblyName.Equals(@"System.IdentityModel.Tokens.Jwt"));
            e2.ShouldNotBeNull();
            e2.BindingRedirection.OldVersionMin.ShouldBe(Version.Parse(@"0.0.0.0"));
            e2.BindingRedirection.OldVersionMax.ShouldBe(Version.Parse(@"4.0.20622.1351"));
            e2.BindingRedirection.NewVersion.ShouldBe(Version.Parse(@"4.0.20622.1351"));

            /* <dependentAssembly>
                    <assemblyIdentity name="SingleAssembly" />
                    <bindingRedirect oldVersion="3.1.0.0" newVersion="3.2.0.0" />
               </dependentAssembly> */
            var e3 = configuration.Entries.SingleOrDefault(e => e.AssemblyName.Equals(@"SingleAssembly"));
            e3.ShouldNotBeNull();
            e3.BindingRedirection.OldVersionMin.ShouldBe(Version.Parse(@"3.1.0.0"));
            e3.BindingRedirection.OldVersionMax.ShouldBe(Version.Parse(@"3.1.0.0"));
            e3.BindingRedirection.NewVersion.ShouldBe(Version.Parse(@"3.2.0.0"));
        }
    }
}
