# Embedded Power BI Dashboard Partial Pattern

Embedded Power BI Partial Pattern enables CIQS solution authors to quickly and seamlessly display deployed solution dashboard on a website deployed in customer's subscription. Previously, end users would have to download Power BI Desktop file for the solution, open it in Power BI Desktop, and follow multi-step directions to connect the dashboard to the SQL Server database deployed as part of the solution. This friction point is eliminated by hosting the dashboard in Embedded Power BI workspace collection, displaying the dashboard on a website deployed to end-user's subscription, and automatically connecting Embedded Power BI Dashboard with SQL Server database provisioned during the solution deployment.

# Prerequisites

To integrate with Embedded Power BI Dashboard Partial Pattern, the following resources are needed.

1. Power BI Desktop file (file extension .pbix) with SQL Server **Direct Query** data source.

2. Azure SQL Server and database.

# How to Integrate

The first step is to add your Power BI Desktop to your solution's assets. We recommend that you create `\assets\dashboards` directory and place your PBIX file there.

Next, open the Manifest file of your solution. Add the snippet below to your solution's provisioning steps.

```xml
<PartialPattern name="embeddedpbi" title="Deploy Solution Dashboard">
  <Parameters>
    <Parameter hidden="true" name="pbixFileUrl" defaultValue="{PatternAssetBaseUrl}/dashboards/solution/dashboard.pbix" />
    <Parameter hidden="true" name="sqlServer" defaultValue="{Outputs.sqlServer}" />
    <Parameter hidden="true" name="sqlDatabase" defaultValue="{Outputs.sqlDatabase}" />
    <Parameter hidden="true" name="sqlServerUsername" defaultValue="{Outputs.sqlServerUsername}" />
    <Parameter hidden="true" name="sqlServerPassword" defaultValue="{Outputs.sqlServerPassword}" />
  </Parameters>
</PartialPattern>
```

Be sure to set `defaultValue` properties appropriately.

# References
