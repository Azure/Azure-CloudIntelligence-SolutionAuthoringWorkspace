#load "..\CiqsHelpers\All.csx"

using System;
using System.Configuration;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.IO;
using System.Data;
using System.Data.SqlClient;


private static DataTable MakeTable()    
{        
    DataTable hensAndRoosters = new DataTable("HensAndRoosters");

    DataColumn inputTimestamp = new DataColumn();
    inputTimestamp.DataType = System.Type.GetType("System.DateTime");
    inputTimestamp.ColumnName = "InputTimestamp";        
    hensAndRoosters.Columns.Add(inputTimestamp);

    DataColumn metricName = new DataColumn();
    metricName.DataType = System.Type.GetType("System.String");
    metricName.ColumnName = "MetricName";
    hensAndRoosters.Columns.Add(metricName);

    DataColumn metricValue = new DataColumn();
    metricValue.DataType = System.Type.GetType("System.Decimal");
    metricValue.ColumnName = "MetricValue";
    hensAndRoosters.Columns.Add(metricValue);
    
    DataColumn[] keys = new DataColumn[] { inputTimestamp, metricName };
    hensAndRoosters.PrimaryKey = keys;

    return hensAndRoosters;
}

public static async Task<object> Run(HttpRequestMessage req, TraceWriter log)
{
    var parametersReader = await CiqsInputParametersReader.FromHttpRequestMessage(req);
    string sqlConnectionString = parametersReader.GetParameter<string>("sqlConnectionString"); 
    var experimentCount = int.Parse(parametersReader.GetParameter<string>("experimentCount")); 

    SqlConnection conn = new SqlConnection(sqlConnectionString);

    conn.Open();

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
    
    var table = MakeTable();
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
        
        var day = new System.TimeSpan(experimentCount, 0, 0, 0);

        DataRow rowHens = table.NewRow();
        rowHens["InputTimestamp"] = now.Subtract(day);
        rowHens["MetricName"] = "Hens";
        rowHens["MetricValue"] = henCount;

        DataRow rowRoosters = table.NewRow();
        rowRoosters["InputTimestamp"] = now.Subtract(day);
        rowRoosters["MetricName"] = "Roosters";
        rowRoosters["MetricValue"] = roosterCount;
        
        table.Rows.Add(rowHens);
        table.Rows.Add(rowRoosters);
    }

    table.AcceptChanges();

    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn))
    {
        bulkCopy.DestinationTableName = "dbo.SolutionData";
        bulkCopy.WriteToServer(table);
    }

    conn.Close();
    
    return null;
}
