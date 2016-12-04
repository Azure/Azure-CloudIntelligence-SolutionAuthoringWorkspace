using System;
using Newtonsoft.Json;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

class Solution
{
    static void Main(String[] args)
    {
        Console.WriteLine("Hello World!");
        
        string s = "DefaultEndpointsProtocol=https;AccountName=stgskx2h7bcta3de;AccountKey=3R4HLkPqO/JSVXZJ1OOBbGwxd4ner+B92q8jxRo/9ajDAicld02B3Zz4AL+8GU3ZXcA0mdPeubvdmiTaBwoRuw==;";
        CloudStorageAccount account = CloudStorageAccount.Parse(s);
        CloudBlobClient client = account.CreateCloudBlobClient();
        CloudBlobContainer container = client.GetContainerReference("mycontainer");
        container.CreateIfNotExists(BlobContainerPublicAccessType.Blob);
    }
}
