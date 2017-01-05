namespace Microsoft.Ciqs.Saw.Phases
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Ciqs.Saw.Common;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;

    [Phase("deploy", "deploy solution(s) into Azure Storage account (CIQS)", Dependencies="build")]
    public class SolutionDeployerPhase : IPhase
    {
        private CloudBlobClient client;

        [Parameter]
        public string SolutionsDirectory { get; set; }
        
        [Parameter(Required=false)]
        public string[] Solutions { get; set; }
        
        [Parameter("solution storage account connection string", Secure=true)]
        public string SolutionStorageConnectionString
        {
            set
            {
                CloudStorageAccount account = CloudStorageAccount.Parse(value);
                this.client = account.CreateCloudBlobClient();
            }
        }

        public void Run()
        {
            var blobs = this.GetBlobs();

            foreach (var blob in blobs)
            {
                var container = this.client.GetContainerReference(blob.Key);

                if (container.Exists())
                {
                    this.PurgeContainer(container);
                }
                else
                {
                    container.Create();
                }

                var permissions = container.GetPermissions();
                permissions.PublicAccess = container.Name.Equals(Constants.SolutionIndexContainerName) 
                    ? BlobContainerPublicAccessType.Off : BlobContainerPublicAccessType.Blob;
                container.SetPermissions(permissions);

                this.UploadFiles(container, blob.Value);
            }            
        }

        private IDictionary<string, IList<Tuple<string, string>>> GetBlobs()
        {
            var path = this.SolutionsDirectory;
            
            if (!path.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                path += Path.DirectorySeparatorChar;
            }
            var result = new Dictionary<string, IList<Tuple<string, string>>>();
            result.Add(Constants.SolutionIndexContainerName, new List<Tuple<string, string>>());

            var core = @"\core";
            var assets = @"\assets\";

            foreach (string folder in Directory.GetDirectories(path))
            {
                var solutionName = folder.Remove(0, path.Length);
                result.Add(solutionName, new List<Tuple<string, string>>());

                var corePath = folder + core;
                var assetsPath = folder + assets;

                foreach (string file in Directory.EnumerateFiles(corePath, "*", SearchOption.AllDirectories))
                {
                    string blobName = file.Remove(0, path.Length).Remove(solutionName.Length, core.Length);
                    result[Constants.SolutionIndexContainerName].Add(new Tuple<string, string>(blobName, file));
                }

                foreach (string file in Directory.EnumerateFiles(assetsPath, "*", SearchOption.AllDirectories))
                {
                    string blobName = file.Remove(0, assetsPath.Length);
                    result[solutionName].Add(new Tuple<string, string>(blobName, file));
                }
            }

            return result;
        }

        private void PurgeContainer(CloudBlobContainer container)
        {
            foreach (var blob in container.ListBlobs(null, true))
            {
                ((CloudBlockBlob)blob).Delete();
            }
        }

        private void UploadFiles(CloudBlobContainer container, IList<Tuple<string, string>> files)
        {
            Console.WriteLine($"Populating container {container.Name}:");
            foreach (var file in files)
            {
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(file.Item1);
                using (var fileStream = System.IO.File.OpenRead(file.Item2))
                {
                    Console.WriteLine($"Uploading {file.Item1}");
                    blockBlob.UploadFromStream(fileStream);
                }
            }
        }
    }
}
