#r "Microsoft.WindowsAzure.Storage" 
#load "..\CiqsHelpers\All.csx"

using System.Net;

public static async Task<object> Run(HttpRequestMessage req, TraceWriter log)
{
    var parametersReader = await CiqsInputParametersReader.FromHttpRequestMessage(req);

    string templateUrl = parametersReader.GetParameter<string>("templateUrl");
    string storageAccountName = parametersReader.GetParameter<string>("storageAccountName");
    string storageAccountKey = parametersReader.GetParameter<string>("storageAccountKey");
    string apiEndpoint = parametersReader.GetParameter<string>("apiEndpoint");
    string storageContainerName = "site";
    string storageBlobName = "index.html";
    
    string websiteUrl = $"https://{storageAccountName}.blob.core.windows.net/{storageContainerName}/{storageBlobName}";

    WebClient client = new WebClient();
    var stream = client.OpenRead(templateUrl);
    StreamReader reader = new StreamReader(stream);
    string htmlTemplate = reader.ReadToEnd();
    
    var websiteContent = htmlTemplate.Replace("{apiEndpoint}", apiEndpoint);    
    
    var storageAccountHelper = new CiqsAzureStorageHelper(storageAccountName, storageAccountKey);
    
    storageAccountHelper.CreateBlobFromText(storageContainerName, storageBlobName, websiteContent);

    return new
    {
        websiteUrl = websiteUrl
    };
}
