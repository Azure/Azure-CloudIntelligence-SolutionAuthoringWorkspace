# How to connect to data source in Power BI desktop?
## Step 1
Download and open the sample **pbix** file. You can download Power BI desktop from [**here**](https://powerbi.microsoft.com/en-us/desktop/) to a Windows desktop if you don’t have one.
## Step 2
**Close** any prompted dialog if you are asked for data credential, because in this tutorial we are going to change the data source and credential away from default configuration to what you have just provisioned.
## Step 3
Click "**Edit Queries**" and select "**Data source settings**".
![](images/pbi3.png)
## Step 4
In the "**Data source settings**" dialog, choose the default data source as such, and click on "**Change source...**".
![](images/pbi4.png)
## Step 5
From the "**Change Source...**" popup, replace the "**Server**" and "**Database**" input as the post-deployment instruction tells and leave everything else as default. for example:
![](images/pbi5.png)
The "**Server**" name and "**Database**" name are usually displayed in the post-deployment instructions, for example:
![](images/pbi5-2.png)
## Step 6
Click "**OK**" to exit changing the data source.
![](images/pbi6.png)
## Step 7
Then we are ready to configure the credentials for the new data source we just added.
Navigate again to the data source settings dialog by clicking "**Edit Queries**", and then selecting "**Data source settings**" as in [**Step 3**](#step-3). This time click "**Edit Permissions...**" button.
![](images/pbi7.png)
## Step 8
In the "**Edit Permissions...**" popup, within the "**Credentials**" section, click the "**Edit...**" button.
![](images/pbi8.png)
## Step 9
From the "**Credentials**" popup, choose "**Database**" and type in the "**User name**" and "**Password**" as instructed.
![](images/pbi9.png)
The "**User name**" and "**Password**" are the ones you were asked to provide in the middle of the provisioning workflow (Remember? 😊).
![](images/pbi9-2.png)
## Step 10
Click "**Save**" to finish configuring the "**Credentials**" and click "**OK**" to finish editing the permissions. Leave other settings as default and click "**Close**" to complete the "**Data source settings**".
## Step 11
Finally, click "**Refresh**" button or the "**Apply changes**" button to load data from the data source into your report; Power BI report views with real-time streaming data is ready for you!
![](images/pbi11.png)
You can always create your own custom views in Power BI reports. More details about Report View in Power BI desktop is available [**here**](https://docs.microsoft.com/en-us/power-bi/desktop-report-view).
Below is a sample output from [**Twitter Streaming with Azure Machine Learning**](https://gallery.azure.ai/Solution/Twitter-Stream-Analysis-with-Azure-Machine-Learning) solution template.
![](images/pbi11-2.png)
If you have any question or feedback, please [**contact us**](mailto:ciqsoncall@microsoft.com). Enjoy and have fun!
