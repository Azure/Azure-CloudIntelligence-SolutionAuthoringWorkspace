namespace Microsoft.Ciqs.Saw.Deployer
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class SolutionDeployer
    {
        private string path;

        private CloudBlobClient client;

        public SolutionDeployer(string path, CloudStorageAccount account)
        {
            // replace UNIX slashes
            path = path.Replace(@"/", Path.DirectorySeparatorChar.ToString());

            if (!path.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                path += Path.DirectorySeparatorChar;
            }

            this.path = path;

            this.client = account.CreateCloudBlobClient();
        }

        public void Deploy()
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
            var result = new Dictionary<string, IList<Tuple<string, string>>>();
            result.Add(Constants.SolutionIndexContainerName, new List<Tuple<string, string>>());

            var core = @"\core";
            var assets = @"\assets\";

            foreach (string folder in Directory.GetDirectories(this.path))
            {
                var solutionName = folder.Remove(0, path.Length);
                result.Add(solutionName, new List<Tuple<string, string>>());

                var corePath = folder + core;
                var assetsPath = folder + assets;

                foreach (string file in Directory.EnumerateFiles(corePath, "*", SearchOption.AllDirectories))
                {
                    string blobName = file.Remove(0, this.path.Length).Remove(solutionName.Length, core.Length);
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