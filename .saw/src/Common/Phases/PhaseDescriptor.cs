namespace Microsoft.Ciqs.Saw.Common
{
    using System;
    
    public class PhaseDescriptor
    {
        public Type Type { get; set; }

        public string Name { get; set; }
                
        public string[] Dependencies { get; set; }
        
        public PhaseDescriptor(Type type, SawPhaseAttribute sawPhaseAttribute)
        {
            this.Type = type;
            this.Name = sawPhaseAttribute.Name;
            this.Dependencies = sawPhaseAttribute.Dependencies.Split(',');
        }
    }
}
