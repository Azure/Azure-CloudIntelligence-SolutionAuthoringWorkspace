#load "..\CiqsHelpers\All.csx"

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

public static async Task<object> Run(HttpRequestMessage req, TraceWriter log)
{   
    var parametersReader = await CiqsInputParametersReader.FromHttpRequestMessage(req);

    string storageAccountName = parametersReader.GetParameter<string>("storageAccountName");
    string storageAccountKey = parametersReader.GetParameter<string>("storageAccountKey");
    string assets = parametersReader.GetParameter<string>("assets");
    string containerName = parametersReader.GetParameter<string>("containerName");

    var assetUrlList = assets.Split(new char[] {';', ' '}, StringSplitOptions.RemoveEmptyEntries);

    var storageCredentials = new StorageCredentials(storageAccountName, storageAccountKey);
    var storageAccount = new CloudStorageAccount(storageCredentials, true);
    var storageClient = storageAccount.CreateCloudBlobClient();

    var container = storageClient.GetContainerReference(containerName);
    container.CreateIfNotExists(BlobContainerPublicAccessType.Blob);

    log.Info($"Created container {containerName}");
    
    var blobList = new List<string>();
    
    foreach (var assetUrl in assetUrlList)
    {        
        Uri uri = new Uri(assetUrl);        
        string blobName = Path.GetFileName(uri.LocalPath);
        
        blobList.Add(blobName);

        log.Info($"Copying {assetUrl} as {blobName}");
        
        CloudBlockBlob target = container.GetBlockBlobReference(blobName);
        target.StartCopy(uri);

        while (target.CopyState.Status == CopyStatus.Pending)
        {
            target.FetchAttributes();
            Thread.Sleep(500);
        }

        if (target.CopyState.Status == CopyStatus.Success) 
        {
            log.Info($"Done copying {assetUrl} as {blobName}");
        }
        else
        {            
            throw new Exception($"Error copying {assetUrl} as {blobName}");
        }
    }

    return new {
        containerName = containerName,
        blobsCreated = string.Join("\r\n", blobList)
    };        
}
