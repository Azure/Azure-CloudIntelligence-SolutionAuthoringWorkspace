using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.IO;
using System.Data;
using System.Data.SqlClient;

public class SolutionDataTable
{
    private DataTable table;
    private string sqlConnectionString;

    public SolutionDataTable(string sqlConnectionString)
    {
        this.sqlConnectionString = sqlConnectionString;
        this.table = this.MakeTable();
    }

    public void AddMetric(DateTime timestamp, string metricName, Decimal metricValue)
    {
        DataRow newRow = table.NewRow();
        newRow["InputTimestamp"] = timestamp;
        newRow["MetricName"] = metricName;
        newRow["MetricValue"] = metricValue;
        this.table.Rows.Add(newRow);
    }

    public void Commit()
    {
        this.table.AcceptChanges();

        using (var connection = new SqlConnection(sqlConnectionString))
        {
            connection.Open();
            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
            {
                bulkCopy.DestinationTableName = "dbo.SolutionData";
                bulkCopy.WriteToServer(table);
            }
        }
    }

    private DataTable MakeTable()    
    {
        DataTable solutionDataTable = new DataTable("SolutionData");

        DataColumn inputTimestamp = new DataColumn();
        inputTimestamp.DataType = System.Type.GetType("System.DateTime");
        inputTimestamp.ColumnName = "InputTimestamp";        
        solutionDataTable.Columns.Add(inputTimestamp);

        DataColumn metricName = new DataColumn();
        metricName.DataType = System.Type.GetType("System.String");
        metricName.ColumnName = "MetricName";
        solutionDataTable.Columns.Add(metricName);

        DataColumn metricValue = new DataColumn();
        metricValue.DataType = System.Type.GetType("System.Decimal");
        metricValue.ColumnName = "MetricValue";
        solutionDataTable.Columns.Add(metricValue);
        
        DataColumn[] keys = new DataColumn[] { inputTimestamp, metricName };
        solutionDataTable.PrimaryKey = keys;

        return solutionDataTable;
    }
}
