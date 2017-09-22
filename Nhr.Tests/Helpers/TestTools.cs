namespace Nhr.Tests.Helpers
{
    using System.IO;
    using System.Reflection;

    using Conditions;

    static class TestTools
    {
        public static Stream OpenResourceStream(string resourceName, Assembly assembly = null)
        {
            resourceName.Requires(nameof(resourceName)).IsNotNullOrWhiteSpace();
            
            if (assembly == null)
            {
                assembly = typeof(TestTools).Assembly;
            }

            var stream = assembly.GetManifestResourceStream(assembly.GetName().Name + '.' + resourceName);
            return stream ?? assembly.GetManifestResourceStream(resourceName);
        }
    }
}
