namespace Microsoft.Ciqs.Saw.Common
{
    using System;
    [AttributeUsage(AttributeTargets.Class)]
    public class SawPhaseAttribute : Attribute 
    {
        public string Name { get; private set; }
        
        public string Dependencies { get; set; }
                
        public SawPhaseAttribute(string name)
        {
            this.Name = name;
        }
    }
}
