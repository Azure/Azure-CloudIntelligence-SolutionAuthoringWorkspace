namespace Microsoft.Ciqs.Saw.Cli
{
    using System;
    using System.Configuration;
    using Microsoft.Ciqs.Saw.Deployer;
    using Microsoft.WindowsAzure.Storage;

    class Program
    {
        static void Main(string[] args)
        {
            var root = Environment.GetEnvironmentVariable("SAW_ROOT");

            var solutionsRoot = root + ConfigurationManager.AppSettings["SolutionsDirectory"];

            Console.WriteLine($"Root: {solutionsRoot}");

            string storageConnectionString = ConfigurationManager.AppSettings["SolutionStorageConnectionString"];
            CloudStorageAccount account = CloudStorageAccount.Parse(storageConnectionString);

            SolutionDeployer deployer = new SolutionDeployer(solutionsRoot, account);
            deployer.Deploy();
        }
    }
}
