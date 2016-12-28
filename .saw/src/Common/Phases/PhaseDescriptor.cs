namespace Microsoft.Ciqs.Saw.Common
{
    using System;
    
    public class PhaseDescriptor
    {
        public Type Type { get; set; }

        public string Name { get; set; }
        
        public string Description { get; set; }
                
        public string[] Dependencies { get; set; }
        
        public PhaseDescriptor(Type type, PhaseAttribute phaseAttribute)
        {
            this.Type = type;
            this.Name = phaseAttribute.Name;
            this.Description = phaseAttribute.Description;
            this.Dependencies = phaseAttribute.Dependencies?.Split(',');
        }
    }
}
