#r "System.IO"
#r "System.Runtime"
#r "System.Threading.Tasks"

using System.Configuration;
using Microsoft.ProjectOxford;
using Microsoft.ProjectOxford.Face;

public static async Task<object> Run(HttpRequestMessage req, TraceWriter log)
{
    var cognitiveServicesAccountKey = ConfigurationManager.AppSettings["CognitiveServicesAccountKey"];    
    IFaceServiceClient faceServiceClient = new FaceServiceClient(cognitiveServicesAccountKey);
    
    string data = await req.Content.ReadAsStringAsync();
    var dataUrlSplit = data.Split(',');

    if (dataUrlSplit.Length != 2)
    {
        throw new Exception("Invalid image data.");
    }

    var base64Data = dataUrlSplit[1];
    var binData = Convert.FromBase64String(base64Data);
 
    using (var stream = new MemoryStream(binData))
    {
        var requiredFaceAttributes = new FaceAttributeType[] {
                FaceAttributeType.Age,
                FaceAttributeType.Gender,
                FaceAttributeType.Smile,
                FaceAttributeType.FacialHair,
                FaceAttributeType.HeadPose,
                FaceAttributeType.Glasses
            };
        try 
        {
            return await faceServiceClient.DetectAsync(stream, true, true, requiredFaceAttributes);            
        }
        catch (FaceAPIException e)
        {
            log.Info(e.ErrorMessage);
            throw;
        }
    }
}
