namespace Microsoft.Ciqs.Saw.Common
{
    using System;
    using System.Reflection;
    
    public class ParameterDescriptor
    {
        public string Name { get; private set; }
                
        public string Description { get; private set; }
        
        public bool Required { get; private set; }
        
        public bool Secure { get; private set; }
        
        public PropertyInfo PropertyInfo { get; private set; }
        
        public ParameterDescriptor(PropertyInfo propertyInfo, ParameterAttribute parameterAttribute)
        {
            this.Name = propertyInfo.Name;
            this.Description = parameterAttribute.Description;
            this.Required = parameterAttribute.Required;
            this.Secure = parameterAttribute.Secure;
            this.PropertyInfo = propertyInfo;
        }
    }
}
