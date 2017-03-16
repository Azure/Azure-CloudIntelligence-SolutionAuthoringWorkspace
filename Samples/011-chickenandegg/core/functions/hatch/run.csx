using System;
using System.Configuration;
using System.Net;

public static async Task<object> Run(HttpRequestMessage req, TraceWriter log)
{
    var eggCount = int.Parse(ConfigurationManager.AppSettings.Get("EggCount"));
    var sexRatioString = ConfigurationManager.AppSettings.Get("SexRatio");
    
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
    
    var roosterCount = 0;
    
    Random rnd = new Random();
    
    for (int i = 0; i < eggCount; i++)
    {
        var r = rnd.NextDouble();
        if (r / (1 - r) < sexRatio)
        {
            roosterCount++;
        }
    } 
    
    return new
    {
        roosters = roosterCount,
        hens = eggCount - roosterCount
    };
}
