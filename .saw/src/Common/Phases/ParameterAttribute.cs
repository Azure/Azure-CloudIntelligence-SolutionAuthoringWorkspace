namespace Microsoft.Ciqs.Saw.Common
{
    using System;
    [AttributeUsage(AttributeTargets.Property)]
    public class ParameterAttribute : Attribute 
    {
        public bool Required { get; set; }
        public string Description { get; set; }
        
        public ParameterAttribute()
        {
            this.Required = true;
        }
    }
}
