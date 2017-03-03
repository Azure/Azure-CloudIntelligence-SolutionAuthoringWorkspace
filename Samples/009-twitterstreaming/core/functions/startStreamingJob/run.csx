#load "..\CiqsHelpers\All.csx"

using Microsoft.Azure;
using Microsoft.Azure.Management.StreamAnalytics;
using Microsoft.Azure.Management.StreamAnalytics.Models;

public static async Task Run(HttpRequestMessage req, TraceWriter log)
{
    var parametersReader = await CiqsInputParametersReader.FromHttpRequestMessage(req);
    string subscriptionId = parametersReader.GetParameter<string>("subscriptionId");
    string authorizationToken = parametersReader.GetParameter<string>("accessToken");
    string resourceGroupName = parametersReader.GetParameter<string>("resourceGroupName");
    string saJobName = parametersReader.GetParameter<string>("saJobName");

    await StartStreamingJob(subscriptionId, authorizationToken, resourceGroupName, saJobName);
}

private static async Task StartStreamingJob(string subscriptionId, string authorizationToken, string resourceGroupName, string jobName)
{
    TokenCloudCredentials credentials = new TokenCloudCredentials(subscriptionId, authorizationToken);
    using (StreamAnalyticsManagementClient streamClient = new StreamAnalyticsManagementClient(credentials))
    {
        JobStartParameters jobStartParameters = new JobStartParameters
        {
            OutputStartMode = OutputStartMode.JobStartTime
        };

        LongRunningOperationResponse response = streamClient.StreamingJobs.Start(resourceGroupName, jobName, jobStartParameters);        
        while (response.Status != OperationStatus.Succeeded)
        {
            var uri = new Uri(response.OperationStatusLink);            
            response = await streamClient.GetLongRunningOperationStatusAsync(response.OperationStatusLink);
            if (response.Status == OperationStatus.Failed)
            {
                throw new Exception($"Start SA job: {jobName} failed");
            }
            await Task.Delay(1000);
        }
    }
}
