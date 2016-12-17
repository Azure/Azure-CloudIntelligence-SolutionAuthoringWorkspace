#load "..\CiqsHelpers\All.csx"

using System.Configuration;
using System.Text.RegularExpressions;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

private static Tuple<string, string> GetDocumentDbEndpointAndAccessKey()
{
    var documentDbConnectionStringSetting = ConfigurationManager.ConnectionStrings["DocumentDb"];    
    var connectionString = documentDbConnectionStringSetting.ConnectionString;  
    Regex r = new Regex(@"^AccountEndpoint=(.*);AccountKey=([^;]*);?$", RegexOptions.IgnoreCase);
    
    Match m = r.Match(connectionString);
    if (!m.Success)
    {
        throw new Exception("Can't parse the connection string.");
    }

    return new Tuple<string, string>(m.Groups[1].Value, m.Groups[2].Value);
}

private static async Task<Database> CreateOrGetDatabase(DocumentClient client, string databaseId)
{
    var databases = client.CreateDatabaseQuery().Where(db => db.Id == databaseId).ToArray();
    if (databases.Any())
    {
        return databases.First();
    }
    
    return await client.CreateDatabaseAsync(new Database { Id = databaseId });
}

public static async Task<object> Run(HttpRequestMessage req, TraceWriter log)
{
    var credentials = GetDocumentDbEndpointAndAccessKey();
    var parametersReader = await CiqsInputParametersReader.FromHttpRequestMessage(req);
    string databaseName = parametersReader.GetParameter<string>("databaseName");

    var client = new DocumentClient(new Uri(credentials.Item1), credentials.Item2);
    await CreateOrGetDatabase(client, databaseName);
    
    return new {
        databaseName = databaseName
    };
}
