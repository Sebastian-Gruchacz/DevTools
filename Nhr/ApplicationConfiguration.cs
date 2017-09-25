namespace Nhr
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Xml;

    using Conditions;
    using Helpers;

    public class ApplicationConfiguration
    {
        private ApplicationConfiguration()
        {

        }

        public ICollection<BindingOverride> Entries { get; private set; } = new List<BindingOverride>();

        public static ApplicationConfiguration FromFile(StreamReader stream)
        {
            stream.Requires(nameof(stream)).IsNotNull();

            var result = new ApplicationConfiguration();

            var namespaceTable = new NameTable();
            XmlNamespaceManager mngr = new XmlNamespaceManager(namespaceTable);
            mngr.AddNamespace(@"vs", @"urn:schemas-microsoft-com:asm.v1");

            XmlDocument xdoc = new XmlDocument(namespaceTable);
            xdoc.Load(stream);

            // TODO: Consider assemblyBinding[appliesTo], generate subsets? Check other nodes, especially <probing>

            var selectNodes = xdoc.SelectNodes(@"/configuration/runtime/vs:assemblyBinding/vs:dependentAssembly", mngr);
            if (selectNodes != null)
            {
                foreach (XmlNode node in selectNodes)
                {
                    result.Entries.Add(ReadOverrideInfo(node, mngr));
                }
            }

            return result;
        }

        private static BindingOverride ReadOverrideInfo(XmlNode node, XmlNamespaceManager mngr)
        {
            node.Requires(nameof(node)).IsNotNull();

            XmlNode assemblyIdentityNode = node.SelectSingleNode(@"vs:assemblyIdentity", mngr);
            XmlNode bindingRedirectNode = node.SelectSingleNode(@"vs:bindingRedirect", mngr);
            XmlNode codeBaseNode = node.SelectSingleNode(@"vs:codeBase", mngr);
            XmlNode publisherPolicyNode = node.SelectSingleNode(@"vs:publisherPolicy", mngr);

            if (codeBaseNode != null)
            {
                throw new NotSupportedException(@"assemblyBinding/dependentAssembly/codeBase");
            }
            if (publisherPolicyNode != null)
            {
                throw new NotSupportedException(@"assemblyBinding/dependentAssembly/publisherPolicyNode");
            }
            if (assemblyIdentityNode == null)
            {
                throw new ConfigurationErrorsException("Missing mandatory <assemblyIdentityNode> node.");
            }
            if (bindingRedirectNode == null)
            {
                throw new ConfigurationErrorsException("Missing mandatory <bindingRedirectNode> node.");
            }

            string assemblyId = assemblyIdentityNode.ReadAttribute(@"name", required: true);
            if (string.IsNullOrWhiteSpace(assemblyId))
            {
                throw new ConfigurationErrorsException("<assemblyIdentityNode> node misses mandatory 'name' attribute.");
            }

            var oldVersion = bindingRedirectNode.ReadAttribute(@"oldVersion", required: true);
            var newVersion = bindingRedirectNode.ReadAttribute(@"newVersion", required: true);

            if (string.IsNullOrWhiteSpace(oldVersion))
            {
                throw new ConfigurationErrorsException("<bindingRedirectNode> node misses mandatory 'oldVersion' attribute.");
            }
            if (string.IsNullOrWhiteSpace(newVersion))
            {
                throw new ConfigurationErrorsException("<bindingRedirectNode> node misses mandatory 'newVersion' attribute.");
            }

            return new BindingOverride
            {
                AssemblyName = assemblyId,
                BindingRedirection = new BindingRedirection(oldVersion, newVersion)
            };
        }
    }
}
