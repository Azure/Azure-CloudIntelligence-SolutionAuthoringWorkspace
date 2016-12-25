namespace Microsoft.Ciqs.Saw.Cli
{
    using System;
    using System.Configuration;
    using System.IO;
    using Microsoft.Ciqs.Saw.Deployer;
    using Microsoft.Ciqs.Saw.Builder;
    using Microsoft.WindowsAzure.Storage;

    class Program
    {
        private static void PrintUsage(bool isInvalid = false)
        {
            if (isInvalid)
            {
                Console.WriteLine("Invalid command.");
            }    
        }
        
        static int Main(string[] args)
        {
            if (args.Length == 0)
            {
                Program.PrintUsage();
                return 0;
            }
            
            var root = Path.GetFullPath(Environment.GetEnvironmentVariable("SAW_ROOT"));
            var solutionsRoot = Path.GetFullPath(Path.Combine(root, ConfigurationManager.AppSettings["SolutionsDirectory"]));
            
            switch (args[0])
            {
                case "build":
                    var packagesDirectory = Path.GetFullPath(Environment.GetEnvironmentVariable("PACKAGES_DIRECTORY"));
                    SolutionBuilder builder = new SolutionBuilder(solutionsRoot, packagesDirectory);
                    builder.Build();
                    break;
                case "deploy":
                    string storageConnectionString = ConfigurationManager.AppSettings["SolutionStorageConnectionString"];
                    CloudStorageAccount account = CloudStorageAccount.Parse(storageConnectionString);
                    SolutionDeployer deployer = new SolutionDeployer(solutionsRoot, account);
                    deployer.Deploy();                    
                    break;
                default:
                    Program.PrintUsage(true);
                    return 1;
            }
            
            return 0;
        }
    }
}
