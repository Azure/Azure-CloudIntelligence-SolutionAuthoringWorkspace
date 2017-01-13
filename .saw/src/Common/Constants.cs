namespace Microsoft.Ciqs.Saw.Common
{
    using System;
    using System.Configuration;
    using System.IO;
    
    public static class Constants
    {
        public const string SolutionIndexContainerName = @"caqs-patterns";
        
        public const string SawRootEnvironmentVariableName = "SAW_ROOT";
        
        public const string PackagesDirectoryEnvironmentVariableName = "PACKAGES_DIRECTORY";
        
        public const string SolutionsDirectoryParameterName = "SolutionsDirectory";
        
        public const string PackagesDirectoryParameterName = "PackagesDirectory";
        
        public const string SolutionStorageConnectionStringParameterName = "SolutionStorageConnectionString";
        
        public static readonly string SawRoot = Path.GetFullPath(Environment.GetEnvironmentVariable(SawRootEnvironmentVariableName));                
        
        public static readonly string SolutionsDirectory = Path.IsPathRooted(ConfigurationManager.AppSettings[SolutionsDirectoryParameterName]) 
            ? ConfigurationManager.AppSettings[SolutionsDirectoryParameterName]
            : Path.GetFullPath(Path.Combine(SawRoot, ConfigurationManager.AppSettings[SolutionsDirectoryParameterName]));
            
        public static readonly string PackagesDirectory = Path.GetFullPath(Environment.GetEnvironmentVariable(PackagesDirectoryEnvironmentVariableName));
        
        public static readonly string SolutionStorageConnectionString = ConfigurationManager.AppSettings[SolutionStorageConnectionStringParameterName];
        
        public static readonly string PhasesAssemblyPath = Path.Combine(Constants.SawRoot, @".saw\bin\Microsoft.Ciqs.Saw.Phases.dll");

    }
}