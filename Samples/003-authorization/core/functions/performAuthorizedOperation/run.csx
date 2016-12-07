#r "Newtonsoft.Json"

#load "..\CiqsHelpers\All.csx"

using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public static async Task<object> Run(HttpRequestMessage req, TraceWriter log)
{
    var parametersReader = await CiqsInputParametersReader.FromHttpRequestMessage(req);

    string accessToken = "Bearer " + parametersReader.GetParameter<string>("accessToken");
    var subscriptionId = parametersReader.GetParameter<string>("subscriptionId");
    var resourceGroupName = parametersReader.GetParameter<string>("resourceGroup");

    var client = new HttpClient();
    client.DefaultRequestHeaders.Add("Authorization", accessToken);

    var url = $"https://management.azure.com/subscriptions/{subscriptionId}/resourcegroups/{resourceGroupName}?api-version=2016-02-01";
    
    var response = await client.GetAsync(url);

    var responseString = await response.Content.ReadAsStringAsync();
    
    if (!response.IsSuccessStatusCode)
    {   
        log.Info(responseString);
        throw new Exception("API call failed.");
    }

    dynamic deserializedResponse = JObject.Parse(responseString);
    string location = deserializedResponse.location; 

    return new
    {
        response = responseString,
        location = location
    };
}
