using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Ciqs.Saw.Deployer;
using Microsoft.WindowsAzure.Storage;

namespace Microsoft.Ciqs.Saw.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            var root = @"C:\Users\anivan\Downloads\patterns\MySolutions"; //Environment.GetEnvironmentVariable("SAW_ROOT");

            Console.WriteLine($"Root: {root}");

            string s = "DefaultEndpointsProtocol=https;AccountName=myciqspatterns;AccountKey=iFfdstmCl9I9hcfl+cCduGY5/NJbzroHZoZ0LNPUUdK5P7bfBchMdX1DwXTcmVk2Hb16k+q0XX6j2cJUpYeo+Q==;";
            CloudStorageAccount account = CloudStorageAccount.Parse(s);

            SolutionDeployer d = new SolutionDeployer(root, account);
            d.Deploy();

            Console.ReadKey();
        }
    }
}
