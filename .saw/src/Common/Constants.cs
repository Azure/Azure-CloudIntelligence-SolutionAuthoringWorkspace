namespace Microsoft.Ciqs.Saw.Common
{
    using System;
    using System.IO;
    
    public static class Constants
    {
        public const string SolutionIndexContainerName = @"caqs-patterns";
        
        public static readonly string SawRoot = Path.GetFullPath(Environment.GetEnvironmentVariable("SAW_ROOT"));
        public static readonly string PhasesAssemblyPath = Path.Combine(Constants.SawRoot, @".saw\bin\Microsoft.Ciqs.Saw.Phases.dll");

    }
}