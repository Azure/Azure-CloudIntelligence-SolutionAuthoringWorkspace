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
            
            var command = args[0];
            
            Action buildLambda = () => {
                var packagesDirectory = Path.GetFullPath(Environment.GetEnvironmentVariable("PACKAGES_DIRECTORY"));
                SolutionBuilder builder = new SolutionBuilder(solutionsRoot, packagesDirectory);
                builder.Build();                
            };
            
            Action deployLambda = () => {
                string storageConnectionString = ConfigurationManager.AppSettings["SolutionStorageConnectionString"];
                CloudStorageAccount account = CloudStorageAccount.Parse(storageConnectionString);
                SolutionDeployer deployer = new SolutionDeployer(solutionsRoot, account);
                deployer.Deploy();                                    
            };
            
            switch (args[0])
            {
                case "build":
                    buildLambda();
                    break;
                case "deploy":
                    buildLambda();
                    deployLambda();
                    break;
                default:
                    Program.PrintUsage(true);
                    return 1;
            }
            
            return 0;
        }
    }
}
