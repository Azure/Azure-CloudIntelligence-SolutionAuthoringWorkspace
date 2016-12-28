namespace Microsoft.Ciqs.Saw.Common
{
    using System;
    [AttributeUsage(AttributeTargets.Class)]
    public class PhaseAttribute : Attribute 
    {
        public string Name { get; private set; }
        
        public string Description { get; private set; }
        
        public string Dependencies { get; set; }
                
        public PhaseAttribute(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }
    }
}
