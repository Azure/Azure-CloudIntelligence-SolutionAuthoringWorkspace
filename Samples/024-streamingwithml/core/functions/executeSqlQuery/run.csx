using System.Configuration;
using System.Data.SqlClient;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;

public static object Run(HttpRequestMessage req, TraceWriter log)
{
    string sqlQuery = @"
        CREATE TABLE [dbo].[TweetScore]
        (
            [timestamp] [datetime] NOT NULL,
	        [topic] [nvarchar](100) NOT NULL,	
	        [score] [float] NOT NULL,
	        [sentiment] [char](8) NOT NULL,
	        [text] [nvarchar](200) NOT NULL,
            [id] [bigint] NOT NULL,
	        [retweet_count] [int] NULL,
	        [time_zone] [nvarchar](100) NULL,
	        [lang] [nvarchar](100) NULL
        )";

    var sqlConnectionString = ConfigurationManager.ConnectionStrings["SqlConnectionString"].ConnectionString;

    SqlConnection conn = new SqlConnection(sqlConnectionString);
    Server server = new Server(new ServerConnection(conn));
    server.ConnectionContext.ExecuteNonQuery(sqlQuery);

    return null;
}
