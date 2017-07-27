## Instructions

#### Embedded Power BI 
[Open the dashboard in a new window]({Outputs.solutionDashboardUrl}).  Refresh the browser to see newest data.

#### Quick links
* [Power BI Desktop .pbix file]({PatternAssetBaseUrl}/dashboards/PolarBearDesktop.pbix) (*Edge
    might change the extension to .zip*)

* [polarbear.csv file]({PatternAssetBaseUrl}/polarbear.csv)

* [3DMap.xlsx file]({PatternAssetBaseUrl}/3DMap.xlsx)

**Connecting Power BI Desktop to Azure SQL Database**
* Open the Power BI Desktop report and choose **Edit Queries** > **Data Source Settings** > **Change Source** and then paste the Server and Database names from the information below and Click **OK**
* Close the Data Source Settings by clicking **Close** and then click **Refresh** which will prompt you for your Username and Password (Note: Choose **Database** and not Windows)
* Copy and paste your Username and enter the Password (One you entered when deploying the Tutorial) and click **Connect**
    
**Azure SQL Database**: Stores the streaming data in table format for access by embedded Power BI.
	
		Server: {Outputs.sqlServer}.database.windows.net
		Database: {Outputs.sqlDatabase}
		Username: {Outputs.sqlServerUsername}
		Password: \<Password provided at provision time\>

##Source data

[U.S. Geological Survey Polar Bear Mark-Recapture Records, Alaska Portion of the Southern Beaufort Sea, 2001-2010](https://alaska.usgs.gov/products/data.php?dataid=7)

The data in this tutorial is part of Ecological Society of America journal article [Polar bear population dynamics in the southern Beaufort Sea during a period of sea ice decline](http://onlinelibrary.wiley.com/doi/10.1890/14-1129.1/abstract;jsessionid=971635022786A4B837B4B8EBDCFCF422.f02t03)