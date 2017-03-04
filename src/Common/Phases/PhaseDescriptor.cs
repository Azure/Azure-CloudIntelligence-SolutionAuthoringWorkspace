namespace Microsoft.Ciqs.Saw.Common
{
    using System;
    
    public class PhaseDescriptor
    {
        public Type Type { get; private set; }

        public string Name { get; private set; }
        
        public string Description { get; private set; }
                
        public string[] Dependencies { get; private set; }
        
        public ParameterDescriptor[] Parameters { get; private set; }
        
        public PhaseDescriptor(Type type, PhaseAttribute phaseAttribute, ParameterDescriptor[] parameters)
        {
            this.Type = type;
            this.Name = phaseAttribute.Name;
            this.Description = phaseAttribute.Description;
            this.Dependencies = phaseAttribute.Dependencies?.Split(',');
            this.Parameters = parameters;
        }
    }
}
