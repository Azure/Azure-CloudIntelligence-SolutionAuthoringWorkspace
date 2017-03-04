namespace Microsoft.Ciqs.Saw.Phases
{
    using System;
    using System.Collections.Generic;
    using System.IO;    
    using Microsoft.Ciqs.Saw.Common;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;
    using Microsoft.WindowsAzure.Storage.Shared.Protocol;
    using Newtonsoft.Json;

    [Phase("deploy", "deploy solution(s) into Azure Storage account (CIQS)", Dependencies="build")]
    public class SolutionDeployerPhase : IPhase
    {
        public const string SolutionIndexContainerName = @"caqs-patterns";
        public const string SawDataPublicContainer = @"saw-data-public";

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
            this.UpdateCorsSettings();
            var blobs = this.GetBlobs();

            var tmpFile = Path.GetTempFileName();
            blobs.Add(SawDataPublicContainer, new List<Tuple<string, string>> {
                new Tuple<string, string>("lastUpload", tmpFile)
            });            
            using (StreamWriter streamWriter = File.AppendText(tmpFile)) {
                streamWriter.WriteLine(JsonConvert.SerializeObject(
                    new {
                        datetime = DateTime.UtcNow
                    }));
            }

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
                permissions.PublicAccess = container.Name.Equals(SolutionIndexContainerName) 
                    ? BlobContainerPublicAccessType.Off : BlobContainerPublicAccessType.Blob;
                container.SetPermissions(permissions);

                this.UploadFiles(container, blob.Value);
            }
            
            if (File.Exists(tmpFile))
            {
                File.Delete(tmpFile);                
            }
        }

        private void UpdateCorsSettings()
        {
            var corsProperties = this.client.GetServiceProperties();
            
            if (corsProperties.Cors.CorsRules.Count == 0) 
            {
                var allowCiqs = new CorsRule()
                {
                    AllowedHeaders = { "*" },
                    AllowedOrigins = { "https://localhost:44302", "https://caqsint.azure.net", "https://start.cortanaintelligence.com" },
                    AllowedMethods = CorsHttpMethods.Get,
                    ExposedHeaders = { "*" },
                    MaxAgeInSeconds = 1
                };
                corsProperties.Cors.CorsRules.Add(allowCiqs);
                this.client.SetServiceProperties(corsProperties);
                Console.WriteLine("CORS settings for the Blob Storage successfully updated.");
            }
        }

        private IDictionary<string, IList<Tuple<string, string>>> GetBlobs()
        {
            var path = this.SolutionsDirectory;
            var solutionSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            
            if (this.Solutions != null)
            {
                solutionSet.UnionWith(this.Solutions);
            }
            
            if (!path.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                path += Path.DirectorySeparatorChar;
            }
            
            var result = new Dictionary<string, IList<Tuple<string, string>>>();
            result.Add(SolutionIndexContainerName, new List<Tuple<string, string>>());

            var core = @"\core";
            var assets = @"\assets\";

            foreach (string folder in Directory.GetDirectories(path))
            {
                var solutionName = folder.Remove(0, path.Length).ToLower();
                
                if (this.Solutions != null && !solutionSet.Contains(solutionName))
                {
                    continue;
                }
                
                result.Add(solutionName, new List<Tuple<string, string>>());

                var corePath = folder + core;
                var assetsPath = folder + assets;

                foreach (string file in Directory.EnumerateFiles(corePath, "*", SearchOption.AllDirectories))
                {
                    string blobName = solutionName + file.Remove(0, path.Length).Remove(0, solutionName.Length + core.Length);                    
                    result[SolutionIndexContainerName].Add(new Tuple<string, string>(blobName, file));
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
            if (files == null || files.Count == 0)
            {
                return;
            }
            
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
