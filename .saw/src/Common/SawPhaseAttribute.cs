namespace Microsoft.Ciqs.Saw.Common
{
    using System;
    [AttributeUsage(AttributeTargets.Class)]
    public class SawPhaseAttribute : Attribute 
    {
        public string Dependencies { get; set; }
        
        private string name;
        
        public SawPhaseAttribute(string name)
        {
            this.name = name;
        }
    }
}
