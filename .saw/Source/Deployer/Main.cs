using System;
using System.Linq;
using System.Threading;
using Newtonsoft.Json;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;

class Solution
{
    static void Main(String[] args)
    {
        var root = Environment.GetEnvironmentVariable("SAW_ROOT");

        Console.WriteLine($"Root: {root}");

        string s = "DefaultEndpointsProtocol=https;AccountName=myciqspatterns;AccountKey=iFfdstmCl9I9hcfl+cCduGY5/NJbzroHZoZ0LNPUUdK5P7bfBchMdX1DwXTcmVk2Hb16k+q0XX6j2cJUpYeo+Q==;";
        CloudStorageAccount account = CloudStorageAccount.Parse(s);
        CloudBlobClient client = account.CreateCloudBlobClient();

        var mySolutionsPath = root + @"\MySolutions";
        // Replace Unix slashes.
        mySolutionsPath = mySolutionsPath.Replace(@"/", Path.DirectorySeparatorChar.ToString());

        if (!mySolutionsPath.EndsWith(Path.DirectorySeparatorChar.ToString()))
        {
            mySolutionsPath += Path.DirectorySeparatorChar;
        }

        var core = @"\core";
        var assets = @"\assets\";

        var containers = client.ListContainers();
        foreach (var container in containers)
        {
            Console.WriteLine($"Deleting {container.Name}");
            container.Delete();
        }

        CloudBlobContainer caqsPatternsContainer = client.GetContainerReference(@"caqs-patterns");

        do
        {
            try
            {
                caqsPatternsContainer.Create();
                break;
            }
            catch(StorageException e)
            {
                if (e.RequestInformation.HttpStatusCode != 409)
                {
                    Console.WriteLine(e.RequestInformation.HttpStatusCode);
                    throw;
                }
            }

            Thread.Sleep(500);
        } while (true);


        foreach (string folder in Directory.GetDirectories(mySolutionsPath))
        {
            foreach (string file in Directory.EnumerateFiles(folder + core, "*", SearchOption.AllDirectories))
            {
                string blobName = file.Remove(0, mySolutionsPath.Length);
                blobName = blobName.Remove(blobName.IndexOf(core), core.Length);
                Console.WriteLine(blobName);

                CloudBlockBlob blockBlob = caqsPatternsContainer.GetBlockBlobReference(blobName);
                using (var fileStream = System.IO.File.OpenRead(file))
                {
                    blockBlob.UploadFromStream(fileStream);
                }
            }
        }

        // Create asset containers
        foreach (string folder in Directory.GetDirectories(mySolutionsPath))
        {
            var assetsRoot = folder + assets;

            var solutionName = folder.Remove(0, mySolutionsPath.Length);
            CloudBlobContainer solutionContainer = client.GetContainerReference(solutionName);
            solutionContainer.Create();
            var permissions = solutionContainer.GetPermissions();
            permissions.PublicAccess = BlobContainerPublicAccessType.Blob;
            solutionContainer.SetPermissions(permissions);

            foreach (string file in Directory.EnumerateFiles(assetsRoot, "*", SearchOption.AllDirectories))
            {
                string blobName = file.Remove(0, assetsRoot.Length);
                Console.WriteLine($"{solutionName} - {blobName}");

                CloudBlockBlob blockBlob = solutionContainer.GetBlockBlobReference(blobName);
                using (var fileStream = System.IO.File.OpenRead(file))
                {
                    blockBlob.UploadFromStream(fileStream);
                } 
            }
        }


    }
}
