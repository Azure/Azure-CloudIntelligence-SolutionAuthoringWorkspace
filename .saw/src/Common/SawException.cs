namespace Microsoft.Ciqs.Saw.Common
{
    using System;

    public class SawException: Exception
    {
        public SawException(string message): base(message)
        {
        }
    }
}
