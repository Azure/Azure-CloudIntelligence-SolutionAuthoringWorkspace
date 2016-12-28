namespace Microsoft.Ciqs.Saw.Common
{
    using System;
    
    public class PhaseDescriptor
    {
        public string Name { get; set; }
        
        public Type Type { get; set; }
        
        public string[] Dependencies { get; set; }
    }
}
