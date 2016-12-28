namespace Microsoft.Ciqs.Saw.Common
{
    using System;
    using System.Reflection;
    
    public class ParameterDescriptor
    {
        public string Name { get; set; }
                
        public string Description { get; set; }
        
        public ParameterDescriptor(PropertyInfo propertyInfo, ParameterAttribute parameterAttribute)
        {
            
        }
    }
}
