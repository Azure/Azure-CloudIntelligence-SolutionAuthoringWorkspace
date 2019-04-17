---
layout: default
title: Tutorial
navigation_weight: 2
---
{:.no_toc}
# Tutorial

<ol id="markdown-toc">
  <li><a href="#learning-objectives" id="markdown-toc-learning-objectives" aria-label="1. Learning objectives">Learning objectives</a></li>
  <li><a href="#before-we-begin" id="markdown-toc-before-we-begin" aria-label="2. Before we begin">Before we begin</a></li>
  <li><a href="#the-samples" id="markdown-toc-the-samples" aria-label="3. The Samples">The Samples</a>    <ol>
      <li><a href="#trying-out-a-sample" id="markdown-toc-trying-out-a-sample" aria-label="3(1). Trying out a sample">Trying out a sample</a></li>
      <li><a href="#what-happens-during-a-ciqs-deployment" id="markdown-toc-what-happens-during-a-ciqs-deployment" aria-label="3(2). What happens during a CIQS deployment?">What happens during a CIQS deployment?</a>        <ol>
          <li><a href="#deployment-creation" id="markdown-toc-deployment-creation" aria-label="3(2)(1). Deployment creation">Deployment creation</a></li>
          <li><a href="#resource-provisioning" id="markdown-toc-resource-provisioning" aria-label="3(2)(2). Resource provisioning">Resource provisioning</a></li>
          <li><a href="#post-deployment-instructions" id="markdown-toc-post-deployment-instructions" aria-label="3(2)(3). Post-deployment instructions">Post-deployment instructions</a></li>
        </ol>
      </li>
    </ol>
  </li>
  <li><a href="#essential-components-of-a-ciqs-solution" id="markdown-toc-essential-components-of-a-ciqs-solution" aria-label="4. Essential components of a CIQS solution">Essential components of a CIQS solution</a></li>
  <li><a href="#deep-dive" id="markdown-toc-deep-dive" aria-label="5. Deep dive">Deep dive</a>    <ol>
      <li><a href="#chicken-and-egg-011-chickenandegg" id="markdown-toc-chicken-and-egg-011-chickenandegg" aria-label="5(1). Chicken and egg (011-chickenandegg)">Chicken and egg (011-chickenandegg)</a></li>
      <li><a href="#solution-dashboard-008-solutiondashboard" id="markdown-toc-solution-dashboard-008-solutiondashboard" aria-label="5(2). Solution dashboard (008-solutiondashboard)">Solution dashboard (008-solutiondashboard)</a></li>
    </ol>
  </li>
  <li><a href="#chicken-and-egg-on-steroids-hands-on-exercise" id="markdown-toc-chicken-and-egg-on-steroids-hands-on-exercise" aria-label="6. Chicken and egg on steroids (hands-on exercise)">Chicken and egg on steroids (hands-on exercise)</a>    <ol>
      <li><a href="#copy-011-chickenandegg-sample-into-the-solutions-directory" id="markdown-toc-copy-011-chickenandegg-sample-into-the-solutions-directory" aria-label="6(1). Copy 011-chickenandegg sample into the Solutions directory">Copy 011-chickenandegg sample into the Solutions directory</a></li>
      <li><a href="#update-title-and-description" id="markdown-toc-update-title-and-description" aria-label="6(2). Update <Title> and <Description>">Update <code class="highlighter-rouge">&lt;Title&gt;</code> and <code class="highlighter-rouge">&lt;Description&gt;</code></a></li>
      <li><a href="#update-the-first-manual-step" id="markdown-toc-update-the-first-manual-step" aria-label="6(3). Update the first <Manual> step">Update the first <code class="highlighter-rouge">&lt;Manual&gt;</code> step</a></li>
      <li><a href="#remove-the-second-azurefunctionapp-provisioning-step-from-the-manifest" id="markdown-toc-remove-the-second-azurefunctionapp-provisioning-step-from-the-manifest" aria-label="6(4). Remove the second <AzureFunctionApp> provisioning step from the Manifest">Remove the second <code class="highlighter-rouge">&lt;AzureFunctionApp&gt;</code> provisioning step from the <code class="highlighter-rouge">Manifest</code></a></li>
      <li><a href="#incorporate-and-slightly-tweak-stuff-from-008-solutiondashboard-into-your-new-solution" id="markdown-toc-incorporate-and-slightly-tweak-stuff-from-008-solutiondashboard-into-your-new-solution" aria-label="6(5). Incorporate (and slightly tweak) stuff from 008-solutiondashboard into your new solution">Incorporate (and slightly tweak) stuff from 008-solutiondashboard into your new solution</a></li>
      <li><a href="#modify-the-hatch-function-to-hatch-eggs-into-the-sql-database" id="markdown-toc-modify-the-hatch-function-to-hatch-eggs-into-the-sql-database" aria-label="6(6). Modify the hatch function to hatch eggs into the SQL database">Modify the <code class="highlighter-rouge">hatch</code> function to hatch eggs into the SQL database</a></li>
      <li><a href="#final-touches" id="markdown-toc-final-touches" aria-label="6(7). Final touches">Final touches</a></li>
    </ol>
  </li>
  <li><a href="#appendix" id="markdown-toc-appendix" aria-label="7. Appendix">Appendix</a>    <ol>
      <li><a href="#manifestxml-changes-diff" id="markdown-toc-manifestxml-changes-diff" aria-label="7(1). Manifest.xml changes (diff)">Manifest.xml changes (diff)</a></li>
      <li><a href="#solutiondatatablecsx" id="markdown-toc-solutiondatatablecsx" aria-label="7(2). SolutionDataTable.csx">SolutionDataTable.csx</a></li>
      <li><a href="#functionshatchruncsx-changes-diff" id="markdown-toc-functionshatchruncsx-changes-diff" aria-label="7(3). functions/hatch/run.csx changes (diff)">functions/hatch/run.csx changes (diff)</a></li>
    </ol>
  </li>
</ol>

## Learning objectives

* Create an authoring account and learn how to use SAW
* Explore the [Samples](https://github.com/Azure/Azure-CloudIntelligence-SolutionAuthoringWorkspace/tree/master/Samples) and get familiar with the essential components of a CIQS Solution
* Build a new Solution by reusing the code from multiple SAW samples

## Before we begin

If you don't have an authoring account yet, please create one following [these instructions](getting-started.html). For the remainder of this tutorial, we will be using the [in-browser solution authoring environment](getting-started.html#package-1-in-browser-solution-authoring-environment). Of course, [local environment](getting-started.html#package-2-local-solution-development-on-windows) can be used as well.

## The Samples

SAW comes with a collection of Samples. Most of them (with a few exceptions) aren't full-fledged solutions, but rather small reusable pieces that come in handy when building something *real*.

In your in-browser environment, expand the **Samples** directory and take a quick look at what's available.
![]({{ site.baseurl }}/images/samples.jpg)

### Trying out a sample

Say, you want to create a solution from a sample. Since this is our first solution, **001-helloworld** seems like a good choice.

Copy the sample into the **Solutions** directory and run **saw push** in the console to upload the solution into your private gallery (*My Solutions*):
![]({{ site.baseurl }}/images/push-helloworld.gif)

Now the solution is ready to be deployed! Clicking the **Deploy** link on its thumbnail will initiate a CIQS deployment.

### What happens during a CIQS deployment?

Each CIQS deployment (even the simplest one we just kicked off) can be broken down into 3 main stages.

#### Deployment creation
  
This is where you choose deployment's name, subscription and location. The name of the deployment corresponds to the name of the resource group that is created in the selected subscription/location immediately after clicking **Create**.
![]({{ site.baseurl }}/images/helloworld-create.jpg)

#### Resource provisioning

This is a sequence of fully automated provisioning steps, which sometimes (quite rarely) may be interrupted by manual steps if automation is not possible (we try *really hard* to avoid disruptive manual steps).

![]({{ site.baseurl }}/images/helloworld-deployment.jpg)

#### Post-deployment instructions

Technically, this is optional, but hardly any solution goes without it.

![]({{ site.baseurl }}/images/helloworld-ready.jpg)

Clicking the **Resource group** link opens the resource group created during the deployment in the *Azure Portal*.
![]({{ site.baseurl }}/images/resource-group-empty.jpg)

The "Hello World" deployment didn't create any resources, which can be seen in the snapshot above. However, a resource group with the same name as the CIQS deployment (**test01**) has been created. In addition, there was also one successful *Azure Resource Manager* (ARM) deployment into this resource group. Now, it's a good time to find out what triggered that ARM deployment.

## Essential components of a CIQS solution

Let's take a look inside the solution we just deployed, the **001-helloworld** sample.

![]({{ site.baseurl }}/images/essential-solution-components.jpg)

**Manifest.xml** is the core components of each CIQS solution. It defines basic attributes that describe the solution and a sequence of ```<ProvisioningSteps>``` necessary to perform a deployment. For example, here's the **Manifest** from **001-helloworld**:

```xml
<?xml version="1.0" encoding="utf-8"?>
<Template>
    <Title>Hello World Solution</Title>
    <Owner displayname="John Doe" email="john.doe@contoso.com"/>
    <PublishedOn>12/31/2016</PublishedOn>
    <ImageUrl>{PatternAssetBaseUrl}/image.png</ImageUrl>
    <Description>Something awesome.</Description>
    <Summary src="Summary.md" format="markdown"/>
    <EstimatedTime>1 Minute</EstimatedTime>
    <ProvisioningSteps>
        <ArmDeployment source="blank.json" title="Deploying a blank ARM template" />
        <Manual title="Done">
          <Instructions src="Instructions.md" format="markdown" />
        </Manual>
    </ProvisioningSteps>
</Template>
```

It should now be clear what was the source of the ARM deployment into the resource group created when we deployed this solution earlier. ```blank.json``` is an empty ARM template that creates nothing, yet it is valid and sufficient to perform a successful ARM deployment:

```json
{
    "$schema": "https://schema.management.azure.com/schemas/2015-01-01/deploymentTemplate.json#",
    "contentVersion": "1.0.0.0",   
    "parameters": {
    },    
    "variables": {        
    },    
    "resources": [        
    ],    
    "outputs": {        
    }
}
```

The ```<ArmDeployment>``` provisioning step is followed by a ```<Manual>``` step which references a Markdown file, ```Instructions.md```, containing the post-deployment instructions. In this example, it's the traditional

```
Hello world!
```

Needless to say, a solution can have as many provisioning steps as necessary. We have already seen ```<ArmDeployment>``` and ```<Manual>```. Let's examine a couple of slightly more complex samples and discover more!

## Deep dive

Let's exampine two samples and learn how to execute custom provisioning code via *Azure Functions* (```<Function>```), configure Azure Function App (App Service) with the management provisioning step ```<AzureFunctionApp>``` and, finally, how to incorporate a *Power BI* dashboard into a solution.

### Chicken and egg ([011-chickenandegg](https://github.com/Azure/Azure-CloudIntelligence-SolutionAuthoringWorkspace/tree/master/Samples/011-chickenandegg))

### Solution dashboard ([008-solutiondashboard](https://github.com/Azure/Azure-CloudIntelligence-SolutionAuthoringWorkspace/tree/master/Samples/008-solutiondashboard))

## Chicken and egg on steroids (hands-on exercise)
#### Copy 011-chickenandegg sample into the Solutions directory
Optionally, rename it and replace the image.

#### Update ```<Title>``` and ```<Description>```
Refer to [the diff below](#manifestxml-changes-diff) for some ideas.

#### Update the first ```<Manual>``` step
* edit parameter descriptions
* add a new parameter with the name ```experimentCount```; this parameter will determine the number of generated daily data points

  Optionally, add instructions (and an image) to this manual step describing what's this all about.
E.g.: 

  ```
  You are running a private egg incubator.

  ![]({{ site.baseurl }}/images/incubator.jpg)
  ```

#### Remove the second ```<AzureFunctionApp>``` provisioning step from the ```Manifest```
We will be using a SQL database to store the solution data, so AppSettings are no longer needed.

#### Incorporate (and slightly tweak) stuff from 008-solutiondashboard into your new solution

* copy ```sqlserver.json``` into the ```core``` directory of the new solution
* copy ```prepsql``` function as is under ```core/functions```
* open the .sql file included with the ```prepsql``` function and remove the entire ```INSERT``` statement along with everything underneath it (we will be generating our own data instead)
* copy the ```<ArmDeployment>```, ```<Function>``` and ```<SolutionDashboard>``` provisioning steps into the ```Manifest``` of the new solution (again, refer to [the diff](#manifestxml-changes-diff) if not sure where to place them, but it's also fine to use your *best* judgement and even make mistakes)

#### Modify the ```hatch``` function to hatch eggs into the SQL database

* copy ```project.json``` from ```prepsql``` to make sure SQL dependencies are available
* create a new C# file (.csx) inside the ```hatch``` function and give it the name ```SolutionDataTable.csx```; copy [this code](#solutiondatatablecsx) into the new file
> This class encapsulates a few simple database table operations and should be self-explanatory.
* make sure ```experimentCount``` and ```sqlConnectionString``` are passed as parameters to the ```hatch``` function (see the diff if need a hint)
* make changes to ```run.csx``` to read the input parameters, hatch eggs on a daily basis and write the counts in the the database table  via ```SolutionDataTable``` (see [sugested changes](#functionshatchruncsx-changes-diff)).

#### Final touches

* create a solution dashboard (.pbix) file.
> For now, you can take [this one](https://github.com/Azure/Azure-CloudIntelligence-SolutionAuthoringWorkspace/blob/master/Samples/015-eggincubator/assets/dashboard.pbix) and upload put it inside the ```assets``` directory of your new solution.
* add the dashboard link to the post-deployment instructions; e.g.:

  ```You can see your dashboard [here]({Outputs.solutionDashboardUrl}).```

## Appendix

### Manifest.xml changes (diff)

```diff
 <?xml version="1.0" encoding="utf-8"?>
 <Template>
-    <Title>Hatching eggs with CIQS</Title>    
+    <Title>Egg incubator simulation</Title>    
     <ImageUrl>{PatternAssetBaseUrl}/chicken.jpg</ImageUrl>
-    <Description>This sample demonstrates how to update AppSettings of the FunctionApp in a safe and reliable way.</Description>
+    <Description>Generating fictitious data and visualizing it with Power BI.</Description>
     <Summary src="Summary.md" format="markdown"/>
     <EstimatedTime>3 Minutes</EstimatedTime>
     <ProvisioningSteps>
         <Manual title="Count your eggs">
             <Parameters>
-                <Parameter name="eggCount" defaultValue="500" description="How many fertilized chicken eggs to you have?" />                
+                <Parameter name="eggCount" defaultValue="500" description="How many fertilized chicken eggs to you get each day?" />                
                 <Parameter name="sexRatio" defaultValue="1:1" description="What's the sex ratio (rooster:hen) of this chicken breed?" />
+                <Parameter name="experimentCount" defaultValue="10000" description="How long have you been running this business?">
+                    <ExtraDescription>(expressed in days)</ExtraDescription>
+                </Parameter>
             </Parameters>            
         </Manual>
+        <ArmDeployment source="sqlserver.json" title="Creating a SQL Server">
+            <Parameters>
+                <Credential type="sql" username="sqlServerUsername" password="sqlServerPassword" />
+            </Parameters>
+        </ArmDeployment>
         <AzureFunctionApp alwaysOn="false" use32BitWorkerProcess="false" servicePlanSku="B2" servicePlanTier="Basic" createStorageAccount="false">
             <AppSettings>
             <!-- Uncomment the below settings and set createStorageAccount to true to enable Azure Functions debugging features -->
@@ -23,14 +31,27 @@
                 <Add key="SexRatio" value="{Inputs.sexRatio}" />
             </AppSettings>            
         </AzureFunctionApp>
-        <Function name="hatch" title="Hatching the eggs" retriable="true" />
-        <AzureFunctionApp title="Updating chicken headcount in the AppSettings">
-            <AppSettings>
-                <Add key="Roosters" value="{Outputs.roosters}" />
-                <Add key="Hens" value="{Outputs.hens}" />
-                <Remove key="EggCount" />
-            </AppSettings>            
-        </AzureFunctionApp>
+        <Function name="prepsql" title="Preparing the SQL Database">
+            <Parameters>
+                <Parameter hidden="true" name="sqlConnectionString" 
+                    defaultValue="Server=tcp:{Outputs.sqlServer}.database.windows.net,1433;Database={Outputs.sqlDatabase};User ID={Inputs.sqlServerUsername};Password={Inputs.sqlServerPassword};Trusted_Connection=False;Encrypt=True;Connection Timeout=30" />
+            </Parameters>
+        </Function>
+        <Function name="hatch" title="Hatching the eggs" retriable="true">
+            <Parameters>
+                <Parameter name="experimentCount" hidden="true" defaultValue="{Inputs.experimentCount}" />
+                <Parameter name="sqlConnectionString" hidden="true" defaultValue="{Inputs.sqlConnectionString}" />
+            </Parameters>
+        </Function>
+        <SolutionDashboard>
+            <Parameters>
+                <Parameter hidden="true" name="pbixFileUrl" defaultValue="{PatternAssetBaseUrl}/dashboard.pbix" />
+                <Parameter hidden="true" name="sqlServer" defaultValue="{Outputs.sqlServer}" />
+                <Parameter hidden="true" name="sqlDatabase" defaultValue="{Outputs.sqlDatabase}" />
+                <Parameter hidden="true" name="sqlServerUsername" defaultValue="{Inputs.sqlServerUsername}" />
+                <Parameter hidden="true" name="sqlServerPassword" defaultValue="{Inputs.sqlServerPassword}" />
+            </Parameters>
+        </SolutionDashboard>
         <Manual title="Done">
           <Instructions src="Instructions.md" format="markdown" />
         </Manual>
         ...
```

### SolutionDataTable.csx

```csharp
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
```

### functions/hatch/run.csx changes (diff)

```diff
+#load "..\CiqsHelpers\All.csx"
+#load "SolutionDataTable.csx"
+
 using System;
 using System.Configuration;
 using System.Net;
 
 public static async Task<object> Run(HttpRequestMessage req, TraceWriter log)
 {
+    var parametersReader = await CiqsInputParametersReader.FromHttpRequestMessage(req);
+    string sqlConnectionString = parametersReader.GetParameter<string>("sqlConnectionString"); 
+
     var eggCount = int.Parse(ConfigurationManager.AppSettings.Get("EggCount"));
     var sexRatioString = ConfigurationManager.AppSettings.Get("SexRatio");
+    var experimentCount = int.Parse(parametersReader.GetParameter<string>("experimentCount")); 
 
     double sexRatio = 0;
     
@@ -20,10 +27,15 @@ public static async Task<object> Run(HttpRequestMessage req, TraceWriter log)
         return req.CreateResponse(HttpStatusCode.BadRequest, $"Invalid sex ratio provided: {sexRatioString}");
     }         
     
-    var roosterCount = 0;
+    var dataTable = new SolutionDataTable(sqlConnectionString);
+    var now = DateTime.Now;
     
     Random rnd = new Random();
 
+    while (experimentCount-- > 0)
+    {
+        var roosterCount = 0;        
+        
         for (int i = 0; i < eggCount; i++)
         {
             var r = rnd.NextDouble();
@@ -33,9 +45,14 @@ public static async Task<object> Run(HttpRequestMessage req, TraceWriter log)
             }
         }
         
-    return new
-    {
-        roosters = roosterCount,
-        hens = eggCount - roosterCount
-    };
+        var henCount = eggCount - roosterCount;        
+        var metricTimestamp = now.AddDays(-experimentCount);
+        
+        dataTable.AddMetric(metricTimestamp, "Hens", henCount);
+        dataTable.AddMetric(metricTimestamp, "Roosters", roosterCount);        
+    }
+
+    dataTable.Commit();
+    
+    return null;
 }
```

