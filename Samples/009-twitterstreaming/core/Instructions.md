## Instructions

[Open the dashboard in a new window]({Outputs.solutionDashboardUrl}).  Refresh the browser to see newest data.

#### Quick links
* [Stream Analysis with Azure ML machine learning web service API manual]({Outputs.webServiceHelpUrl})
* [Machine Learning Experiment]({Outputs.experimentUrl})
* [Power BI Desktop .pbix file]({PatternAssetBaseUrl}/dashboards/StreamingTweetsDesktop.pbix) (*Edge
    might change the extension to .zip*)

**Connecting Power BI Desktop to Azure SQL Database**
* Open the Power BI Desktop report and choose **Edit Queries** > **Data Source Settings** > **Change Source** and then paste the Server and Database names from the information below and Click **OK**
* Close the Data Source Settings by clicking **Close** and then click **Refresh** which will prompt you for your Username and Password (Note: Choose **Database** and not Windows)
* Copy and paste your Username and enter the Password (One you entered when deploying the Tutorial) and click **Connect**
    
**Azure SQL Database**: Stores the streaming data in table format for access by embedded Power BI.
	
		Server: {Outputs.sqlServerName}.database.windows.net
		Database: {Outputs.sqlDatabaseName}
		Username: {Outputs.sqlServerUsername}
		Password: \<Password provided at provision time\>
		
**Change Keywords in Function app settings**
[Open the Function App](https://ms.portal.azure.com/?flight=1#blade/WebsitesExtension/FunctionsIFrameBlade/id/%2Fsubscriptions%2F{SubscriptionId}%2FresourceGroups%2F{ProjectName}%2Fproviders%2FMicrosoft.Web%2Fsites%2F{Outputs.functionAppName})

 * Click on **Platform features** > **Application setting** > scroll to find **App settings** and then change the values for **TWITTER_KEYWORDS**
 * Click **Save** at the top of the App settings blade
 * Open the Database in SQL Server Management Studio or Visual Studio and open a **New Query**
 * Enter ```truncate table TweetScore``` and click **Execute** (This will remove all the records from you previous key words)