#load "..\CiqsHelpers\All.csx"
#load "SolutionDataTable.csx"

using System;
using System.Configuration;
using System.Net;

public static async Task<object> Run(HttpRequestMessage req, TraceWriter log)
{
    var parametersReader = await CiqsInputParametersReader.FromHttpRequestMessage(req);
    string sqlConnectionString = parametersReader.GetParameter<string>("sqlConnectionString"); 

    var eggCount = int.Parse(ConfigurationManager.AppSettings.Get("EggCount"));
    var sexRatioString = ConfigurationManager.AppSettings.Get("SexRatio");
    
    var experimentCount = int.Parse(parametersReader.GetParameter<string>("experimentCount")); 
 
    double sexRatio = 0;
    
    var parts = sexRatioString.Split(':');
    
    try
    {
        sexRatio = Double.Parse(parts[0])/Double.Parse(parts[1]);
    }
    catch (Exception)
    {
        return req.CreateResponse(HttpStatusCode.BadRequest, $"Invalid sex ratio provided: {sexRatioString}");
    }         
    
 
    var dataTable = new SolutionDataTable(sqlConnectionString);
    var now = DateTime.Now;   
    
    Random rnd = new Random();
    
    while (experimentCount-- > 0)
    {
        var roosterCount = 0;        
        
        for (int i = 0; i < eggCount; i++)
        {
            var r = rnd.NextDouble();
            if (r / (1 - r) < sexRatio)
            {
                roosterCount++;
            }
        } 
        
        var henCount = eggCount - roosterCount;        
        var metricTimestamp = now.AddDays(-experimentCount);
        
        dataTable.AddMetric(metricTimestamp, "Hens", henCount);
        dataTable.AddMetric(metricTimestamp, "Roosters", roosterCount);
    }
    
    dataTable.Commit();
        
    return null;
}
