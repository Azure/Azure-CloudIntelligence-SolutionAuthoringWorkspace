#load "..\CiqsHelpers\All.csx"

using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;

public static async Task<object> Run(HttpRequestMessage req, TraceWriter log)
{
    string ResourcePathPrefix = "resources";
    var functionRootPath = CiqsWebHostHelper.GetFunctionWebHostPath("CopyScripts");

    string[] ContainerList = { "scripts" };

    var parametersReader = await CiqsInputParametersReader.FromHttpRequestMessage(req);
    string storageName = parametersReader.GetParameter<string>("StorageName");
    string storageKey = parametersReader.GetParameter<string>("StorageKey");
    CloudBlobProxy cloudBlobProxy = new CloudBlobProxy(storageName, storageKey);
    
    ContainerList.ToList().ForEach(i =>
    {
        var sourceFolder = string.Format(@"{0}\{1}\\{2}\\", functionRootPath, ResourcePathPrefix, i);
        var filesInDirectory = Directory.GetFiles(sourceFolder, "*", SearchOption.AllDirectories);
        UploadFiles(cloudBlobProxy, filesInDirectory, i, sourceFolder);
    });
    
    return null;
}

private static void UploadFiles(CloudBlobProxy cloudBlobProxy, string[] filePathList, string containerName, string sourceFolder)
{
    if (filePathList == null) throw new ArgumentNullException(nameof(filePathList));
    if (containerName == null) throw new ArgumentNullException(nameof(containerName));
    if (sourceFolder == null) throw new ArgumentNullException(nameof(sourceFolder));

    Parallel.For(0,
        filePathList.Count(),
        new ParallelOptions
        {
            MaxDegreeOfParallelism = 10
        },
        i =>
        {
            var filePath = filePathList.ElementAt(i);
            var blobName = filePath.Split(new[] {sourceFolder}, StringSplitOptions.None)[1];
            cloudBlobProxy.UploadBlobFromFilePath(containerName, blobName, filePath);
        });
}


class CloudBlobProxy
{
    /// <summary>Cloud storage account.</summary>
    private readonly CloudStorageAccount _account;

    /// <summary>
    /// </summary>
    /// <param name="storageName">Storage Account Name to associate with Cloud Storage Account.</param>
    /// <param name="storageKey">Storage Account's Access key.</param>
    public CloudBlobProxy(string storageName, string storageKey)
    {
        if (string.IsNullOrWhiteSpace(storageName))
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(storageName));
        if (string.IsNullOrWhiteSpace(storageKey))
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(storageKey));

        var storageCredentials = new StorageCredentials(storageName, storageKey);
        _account = new CloudStorageAccount(storageCredentials, true);
    }

    /// <summary>
    ///     Upload a Block Blob to BlobService using a local file path.
    /// </summary>
    /// <param name="destinationContainer">Container to which file is to be uploaded.</param>
    /// <param name="destinationBlobName">Blob name on upload.</param>
    /// <param name="filePath">Local file path.</param>
    public void UploadBlobFromFilePath(string destinationContainer, string destinationBlobName, string filePath)
    {
        if (string.IsNullOrWhiteSpace(destinationBlobName))
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(destinationBlobName));
        if (string.IsNullOrWhiteSpace(destinationContainer))
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(destinationContainer));
        if (string.IsNullOrWhiteSpace(filePath))
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(filePath));

        var cloudBlobClient = _account.CreateCloudBlobClient();
        var destinationBlobContainer = cloudBlobClient.GetContainerReference(destinationContainer);
        destinationBlobContainer.CreateIfNotExists();

        var destinationBlob = destinationBlobContainer.GetBlockBlobReference(destinationBlobName);
        using (var fileStream = System.IO.File.OpenRead(filePath))
        {
            destinationBlob.UploadFromStream(fileStream);
        }
    }
}
