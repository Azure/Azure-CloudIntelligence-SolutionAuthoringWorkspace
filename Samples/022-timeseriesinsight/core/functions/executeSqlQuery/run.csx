#load "..\CiqsHelpers\All.csx"

using System.Data.SqlClient;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;

public static async Task<object> Run(HttpRequestMessage req, TraceWriter log)
{
    var parametersReader = await CiqsInputParametersReader.FromHttpRequestMessage(req);
    string sqlConnectionString = parametersReader.GetParameter<string>("sqlConnectionString"); 
    string sqlQuery = parametersReader.GetParameter<string>("sqlQuery");  
    SqlConnection conn = new SqlConnection(sqlConnectionString);
    Server server = new Server(new ServerConnection(conn));
    server.ConnectionContext.ExecuteNonQuery(sqlQuery);

    return null;
}
