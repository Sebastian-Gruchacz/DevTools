using System.Diagnostics;

namespace Nhr.Core.Model
{
    [DebuggerDisplay(@"{AssemblyName}")]
    public class BindingOverride
    {
        public string AssemblyName { get; set; }

        public BindingRedirection BindingRedirection { get; set; }
    }
}