namespace Microsoft.Ciqs.Saw.Cli
{
    using System;
    using System.Configuration;
    using Microsoft.Ciqs.Saw.Deployer;
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
            
            var root = Environment.GetEnvironmentVariable("SAW_ROOT");
            
            switch (args[0])
            {
                case "deploy":
                    var solutionsRoot = root + ConfigurationManager.AppSettings["SolutionsDirectory"];
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
