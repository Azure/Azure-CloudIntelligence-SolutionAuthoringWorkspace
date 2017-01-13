namespace Microsoft.Ciqs.Saw.Common
{
    using System;
    [AttributeUsage(AttributeTargets.Property)]
    public class ParameterAttribute : Attribute 
    {
        public string Description { get; set; }
        public bool Required { get; set; }
        public bool Secure { get; set; }
        
        public ParameterAttribute(string description = null)
        {
            this.Required = true;
            this.Description = description;
        }
    }
}
