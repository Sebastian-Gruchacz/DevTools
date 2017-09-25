namespace Nhr.Core.Helpers
{
    using System;
    using System.Xml;

    using Conditions;

    public static class XmlHelper
    {
        public static string ReadAttribute(this XmlNode packageNode, string attributeName, bool required = false)
        {
            packageNode.Requires(nameof(packageNode)).IsNotNull();
            attributeName.Requires(nameof(attributeName)).IsNotNullOrWhiteSpace();

            var attribute = packageNode.Attributes[attributeName];
            if (attribute == null && required)
            {
                throw new InvalidOperationException($"Required attribute '{attributeName}' was not found in the node '{packageNode.Name}'.");
            }

            return attribute?.InnerText ?? null;
        }
    }
}
