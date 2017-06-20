---
layout: default
title: Solution authoring
navigation_weight: 3
---
# Solution authoring
## Solution manifest
### LocationProvidedFor
<<<<<<< HEAD
In CIQS deployment creation page, users are asked to select one location/region for each deployment as shown below.

![Select location for CIQS deployment]({{ site.baseurl }}\images\location.png)
=======
In CIQS deployment creation page, users are asked to select one location/region for each deployment as shown below. 

[Select location for CIQS deployment]({{ site.baseurl }}\images\location.png)
>>>>>>> 460416d2bbba8dc91e174a4c05590e6da8b1adef

The selected location is used by the Resource Group provisioning and most of the time, is also used for underlying Azure resources provisionings accessed with `[resourceGroup().location]` signature in ARM templates. For example:

```json
{
<<<<<<< HEAD
	"name": "[variables('adlStoreName')]",
	"apiVersion": "[variables('adlsApiVersion')]",
	"type": "Microsoft.DataLakeStore/accounts",
	"location": "[resourceGroup().location]"
=======
"name": "[variables('adlStoreName')]",
"apiVersion": "[variables('adlsApiVersion')]"
"type": "Microsoft.DataLakeStore/accounts",
"location": "[resourceGroup().location]"
>>>>>>> 460416d2bbba8dc91e174a4c05590e6da8b1adef
}
```

Given [limited regional availability](https://azure.microsoft.com/en-us/regions/services/) of Azure, some Azure services, such as **Data Factory** (microsoft.datafactory/datafactories), **Application Insights** (microsoft.insights/components) or **Data Lake Store** (microsoft.datalakestore/accounts), have very limited regions available. In this case, hardcoding the region in ARM template would be the **recommended** way to ensure better user experience. For example:

```json
{
	"name": "[variables('adlStoreName')]",
	"apiVersion": "[variables('adlsApiVersion')]",
	"type": "Microsoft.DataLakeStore/accounts",
	"location": "East US 2"
}
```

At the meantime, **LocationProvidedFor** must be specified in that particular ARM deployment step in the **Manifest.xml**, so that CIQS will **stop checking** the regional availability for those service(s). For example below, it signifies the CIQS deployment engine that, "_location has been **hardcoded** for 'microsoft.datalakestore/accounts', and please ignore 'microsoft.datalakestore/accounts' when rendering the location dropdown list_":

```xml
<ArmDeployment source="arm\CreateADLS.json" title="Create ADLS" >
	<LocationProvidedFor>
		<ResourceType>microsoft.datalakestore/accounts</ResourceType>
	</LocationProvidedFor>
</ArmDeployment>
```

### LocationsToExclude
__&lt;LocationsToExclude/&gt;__ tag allows pattern authors to hide locations from CIQS location dropdown. This is very useful especially when some region(**s**) is known to cause deployment failures.

To use this feature, please specify `<LocationsToExclude/>` within `<Template/>` in your solution **Manifest.xml**.

Here is an example of how it is used:
```xml
<Template>
  <LocationsToExclude>
    <Location>Japan East</Location>
  </LocationsToExclude>
</Template>
```

### Parameters
#### Parameter

#### Parameter Resolver
Parameter resolver in CIQS allows solution authors to specify parameters as variable in solution source files (e.g. **Manifest.xml**, markdown files, etc.), of which values can be resolved as input, output or constant strings.

In CIQS solution source files, text enclosed with `{` and `}` will be interpreted as parameter variables, and will be resolved as the corresponding values **if** available. Table below shows all parameter variables available in CIQS.

| Variable Name | Description |
| ------------ | ------------- |
| _{PatternAssetBaseUrl}_ | Base URL for asset files |
| _{ResourceGroup.Name}_ | Resource group name |
| _{ProjectName}_ | Project name |
| _{UserId}_ | User ID, e.g. johnjames@contoso.com |
| _{UserDisplayName}_ | User displayed name, e.g. John James |
| _{SubscriptionId}_ | Subscription ID |
| _{Location}_ | Location as string, e.g. West US |
| _{Inputs.`ParameterName`}_ | Resolved as the value of the input parameter named `ParameterName` from previous provisioning steps |
| _{Outputs.`ParameterName`}_ | Resolved as the value of the output parameter named `ParameterName` from previous provisioning steps |
| _{Constants.LinuxMachineNameRegex}_ | Linux machine host name regex |
| _{Constants.LinuxMachineNameRegexDescription}_ | Linux machine host name regex description |
| _{Constants.WindowsMachineNameRegex}_ | Windows machine name regex |
| _{Constants.WindowsMachineNameRegexDescription}_ | Windows machine name regex description |

> Authors are **highly recommended** to use `{Constants.LinuxMachineNameRegex}` or `{Constants.WindowsMachineNameRegex}` to enforce validation on the VM name input. For example:

```xml
  <Parameter name="vmName" regex="{Constants.LinuxMachineNameRegex}">
    <ExtraDescription>{Constants.LinuxMachineNameRegexDescription}</ExtraDescription>
  </Parameter>
```

> **Note**: It is **NOT** recommended to relay an input parameter as an output without any change in ARM templates; _**{Inputs.`ParameterName`}**_ variable offers you the flexibility to use any input value in subsequent steps within the solution.

#### Credential
`Credential` is a special type of `Parameter` in CIQS. It wires up different set of rules so that corresponding credential rules are applied to corresponding provisioned Azure resources, such as SQL Server/Datawarehouse, Virtual Machine, HDInsight clusters, etc.

> Solutions that ask for user name and password inputs are **highly recommended** to use `Credential` to wrap the credential inputs.

To use this feature,  please specify `<Credential/>` within `<Parameters/>` in your pattern **Manifest.xml**.

* **Documentation**

  * **Attributes**

    | Name | Description |
    | ------------ | ------------- |
    | *type*: `string` | The credential type. The current supported types are: `sql`, `sqlwithoutodbc`, `linuxvm`, `windowsvm`, `hdi`, **AND** any combination of them seperated by "`,`" |
    | *username*: `string` | **The name of** the username parameter defined in the provision step |
    | *password*: `string` | **The name of** the password parameter defined in the provision step |

    > According to [ODBC 3.0 spec](https://msdn.microsoft.com/en-us/library/ms161962.aspx), `[ ] { } ( ) , ; ? * ! @ \ | ' " = :` and **space character** are not permitted in SqlClient, OLE DB or ODBC connection strings; By default, ODBC rules are enforced with `sql` type, because ODBC connections are widely used in CIQS solutions. To ignore ODBC restriction in `sql` inputs, please use **`sqlwithoutodbc`** instead.

* **Examples**

  A simple use case would be specifying the credential with a single type:
  ```xml
  <Parameters>
   <Credential type="sql" username="sqlServerUserName" password="sqlServerPassword" />
  </Parameters>
  ```

  A more complex use case is to combine multiple type of credentials together; It is used when the credential is applied to more than one Azure resources:
  ```xml
  <Parameters>
   <Credential type="sql,linuxvm,hdi" username="userName" password="password" />
  </Parameters>
  ```

### Guide
`<Guide>` tag allows pattern authors to use the content (`README.md`) in their "Solution How To Guide" without duplicating the same information in the  `Manifest.xml` and `Summary.md`. When using `<Guide>` tag in the Manifest.xml, CIQS will parse the pre-defined tags in the md file (specified by “src” attribute) to grab the content that used to be grabbed from the Prerequisites \ EstimatedTime \ Summary …etc. tags in the `Manifest.xml`. That is to say, `<Guide>` tag is used in both `Manifest.xml` and markdown files for different purposes with different syntaxes.

* **Usage in Manifest.xml**

  A typical use case would be specifying the raw markdown url in the "Solution How To Guide":
  ```xml
  <Guide src="https://github.com/Azure/cortana-intelligence-energy-demand-forecasting/raw/master/Automated%20Deployment%20Guide/README.md" format="markdown"/>
  ```

  It's also possible to specify a markdown file in the `core` folder in case the content in the "Solution How To Guide" has not yet been published:
  ```xml
  <Guide src="README.md" format="markdown"/>
  ```

* **Supported types in markdown file (README.md)**

  In markdown file (README.md) – *tags around sections*:

   | Type | Required? | Tag | Counterpart in `Manifest.xml`  |
   | ---- | -------- | --- | ----------- |
   | Prerequisites | N – will default to manifest (Not required in manifest as well) | `<Guide type="Prerequisites"></Guide>` | `<Prerequisites src="Prereqs.md" format="markdown"/>` |
   | EstimatedTime | Y | `<Guide type="EstimatedTime"></Guide>` | `<EstimatedTime>10 Minutes</Estimates>` |
   | Summary | Y | `<Guide type="Summary"></Guide>` | `<Summary src="Summary.md" format="markdown"/>` |
   | Description | Y | `<Guide type="Description"></Guide>` | `<Description>Short description of the solution.</Description>` |
   | PostDeploymentGuidance | N | `<Guide type=”PostDeploymentGuidance” url=”[POST_DEPLOYMENT_GUIDANCE_URL]”/>` | N/A |
   | Disclaimer | N – will default to standard disclaimer | `<Guide type="Disclaimer"></Guide>` | N/A |

* **Supported sections in Post Deployment Guidance md file**

  > Post Deployment Guidance will be generated as links in the deployment "Done" step.

  In Post Deployment Guidance md file – *tags around sections*:

   | Section | Required? | Heading |
   | ---- | -------- | --- |
   | Visualization | N | `# Visualization` |
   | Customization | N | `# Customization` |
   | Scaling | N | `# Scaling` |
   | Security | N | `# Security` |

* **Examples**

  Main README.md: [https://github.com/Azure/cortana-intelligence-energy-demand-forecasting/blob/master/Automated%20Deployment%20Guide/README.md](https://github.com/Azure/cortana-intelligence-energy-demand-forecasting/blob/master/Automated%20Deployment%20Guide/README.md)

  Post-Deployment Guidance: [https://github.com/Azure/cortana-intelligence-energy-demand-forecasting/blob/master/Automated%20Deployment%20Guide/Energy%20Forecast%20Solution%20Post%20Deployment%20Instructions.md](https://github.com/Azure/cortana-intelligence-energy-demand-forecasting/blob/master/Automated%20Deployment%20Guide/Energy%20Forecast%20Solution%20Post%20Deployment%20Instructions.md)


### Ingredients
`<Ingredients>` tag allows pattern authors to specify Azure services they are using in the pattern. Ingredients will be translated to both "SERVICE USED" and "TAGS" sections on the CI Gallery page.
For example:
  ```xml
  <Ingredients>
    <Ingredient>Web</Ingredient>
    <Ingredient>EventHub</Ingredient>
    <Ingredient>StreamAnalytics</Ingredient>
    <Ingredient>Sql</Ingredient>
    <Ingredient>StorageAccount</Ingredient>
    <Ingredient>MachineLearning</Ingredient>
    <Ingredient>DataFactory</Ingredient>
    <Ingredient>PowerBi</Ingredient>
  </Ingredients>
  ```
It will be rendered as below in the CI Gallery:
![]({{ site.baseurl }}/images/ingredient-in-ci-gallery.png)

#### Available Ingredients

| Ingredient | Full service name | Link |
| --- | --- | --- |
| EventHub | Azure Event Hubs | https://azure.microsoft.com/en-us/services/event-hubs/ |
| StreamAnalytics | Azure Stream Analytics | https://azure.microsoft.com/en-us/services/stream-analytics/ |
| MachineLearning | Azure Machine Learning | https://azure.microsoft.com/en-us/services/machine-learning/ |
| DataFactory | Azure Data Factory | https://azure.microsoft.com/en-us/services/data-factory/ |
| HDInsight | Azure HDInsight | https://azure.microsoft.com/en-us/services/hdinsight/ |
| StorageAccount | Azure Blob Storage | https://azure.microsoft.com/en-us/services/storage/ |
| Sql | Azure Sql | https://azure.microsoft.com/en-us/services/sql-database/ |
| PowerBi | PowerBI | https://powerbi.microsoft.com/ |
| CognitiveServicesLuis | Azure Cognitive Service - LUIS | https://azure.microsoft.com/en-us/services/cognitive-services/language-understanding-intelligence-service/ |
| CognitiveServicesSpeech | Azure Bing Speech API | https://azure.microsoft.com/en-us/services/cognitive-services/speech/ |
| DataLakeAnalytics | Azure Data Lake Analytics | https://azure.microsoft.com/en-us/services/data-lake-analytics/ |
| DataLakeStore | Azure Data Lake Store | https://azure.microsoft.com/en-us/services/data-lake-store/ |
| DocumentDb | Azure Document DB | https://azure.microsoft.com/en-us/services/documentdb/ |
| Search | Azure Search Service | https://azure.microsoft.com/en-us/services/search/ |
| SqlDw | Azure SQL DataWarehouse | https://azure.microsoft.com/en-us/services/sql-data-warehouse/ |
| VirtualMachine | Azure Virtual Machine | https://azure.microsoft.com/en-us/services/virtual-machines/ |
| Web | Azure App Service | https://azure.microsoft.com/en-us/services/app-service/ |
| AppInsights | Application Insights | https://azure.microsoft.com/en-us/services/application-insights/ |
| Batch | Azure Batch | https://azure.microsoft.com/en-us/services/batch/ |
| AnalysisServices | Azure Analysis Services | https://azure.microsoft.com/en-us/services/analysis-services/ |
| TimeSeriesInsights | Time Series Insights | https://azure.microsoft.com/en-us/services/time-series-insights/ |

Please contact [CIQS On-Call](mailto:ciqsoncall@microsoft.com) if you want to put any Azure service that is not covered in the table above.

## Provisioning steps
### ArmDeployment
### Manual
Manual steps are most typically used to display post-deployment instructions. For example:
```xml
<Manual title="Done">
  <Instructions src="Instructions.md" format="markdown" />
</Manual>
```

CIQS has no restrictions as to where *Manual* steps can appear inside the __&lt;ProvisioningSteps&gt;__ section of the *Manifest*, nor is there any limit on their number.

The platform guarantees that no automated activities will be injected prior to the *Manual* steps that appear in the very beginning. This allows collecting all input parameters immediately after a deployment is created and performing the rest of the process without interruptions.

The two examples below demonstrate slightly different ways of collecting input parameters with *Manual* steps. The first one uses an ARM template as a *parameter source* and produces a configuration page exactly like __&lt;ArmDeployment&gt;__, except no deployment occurs afterwards. Instead, all parameters enter the **{Inputs.*}** variable pool.

```xml
<Manual parameterSource="armTemplate.json" title="Setup SQL server account">
  <Parameters>
    <Credential type="sql" username="sqlServerUsername" password="sqlServerPassword" />
  </Parameters>
</Manual>
```
If no **parameterSource** attribute is provided, all parameters need to be defined explicitly. This allows collecting arbitrary parameters and use them in various contexts like, for instance, as inputs of the  **Functions**.
```xml
<Manual title="Configure Twitter listener">
  <Parameters>
    <Parameter name="twitterKeywords" description="Twitter topics" type="string"
      defaultValue="@MicrosoftR,@OpenAtMicrosoft,@Azure,#CortanaIntelligence">
      <ExtraDescription>
        Comma-separated list of words, phrases, #hashtags and @mentions
      </ExtraDescription>
    </Parameter>
    <Parameter name="oauthConsumerKey" description="Consumer key (API key)" type="string" />
    <Parameter name="oauthConsumerSecret" description="Consumer secret (API secret)" type="string" />
    <Parameter name="oauthToken" description="Access token" type="string" />
    <Parameter name="oauthTokenSecret" description="Access token secret" type="string" />
  </Parameters>
</Manual>
```
Finally, if all parameters required to perform an ARM deployment are already present in the variable pool, _{Inputs.*}_ or _{Outputs.*}_, it is not necessary to explicitly define them as part of the __&lt;ArmDeployment&gt;__. Instead, use **autoResolveParameters**. For example, assuming that a *Manual* step has been used to collect parameters for *armTemplate.json*, the subsequent __&lt;ArmDeployment&gt;__ provisioning step can be defined as follows:

```xml
<ArmDeployment source="armTemplate.json" autoResolveParameters="true" title="Deploying..." />
```

A demonstration of this technique can be found in the [twitterstreaming](https://github.com/Azure/Azure-CortanaIntelligence-SolutionAuthoringWorkspace/tree/master/Samples/009-twitterstreaming) SAW sample.

### Functions
Below is a quick overview on things you need to do in order to move your web-jobs into Azure functions on CIS. The high-level steps are as follows: 
- Create a ```functions``` folder in ```core``` folder of your solution.
- Create an individual folder for each function that can be invoked. 
- Each function folder will contain a projects.json & a run.csx (for C#).
    - The ```projects.json``` is used to indicate Nuget packages that you will be consuming as part of the function. 
    - The ```run.csx``` file will be the entry point when your function is invoked and will contain all your source. 
The next few sections highlight typical scenarios to consider while migrating your source over to functions.

#### Referencing App Settings or Passing Arguments to your Function
You might have parts of your webjob that references app settings that you loaded with any run-time arguments that you might have needed as part of the webjob. The good news is that you no longer have to store these in app settings and you can simply pass it as arguments when invoking the function via CIS. 

When migrating the web job source over, you might find blocks like the following: 
```csharp
        string sqlServer = ConfigurationManager.AppSettings["SqlDwServerName"];
```
Instead of reading these arguments from the app settings, read them from the incoming ```HttpRequestMessage``` passed in as part of invoking the function. i.e. 
```csharp
    string sqlServer = parametersReader.GetParameter<string>("SqlDwServerName");
```
Also, be sure to pass in the relevant string as an argument when invoking the function in your ```Manifest.xml```:
```xml
    <Function name="initStoredProcs" title="Initialize stored procedures">
      <Parameters>
        <Parameter hidden="true" name="SqlDwServerName" type="string" defaultValue="{Outputs.SqlDwServerName}" /> 
      </Parameters>
    </Function>
```
You might need to surface the relevant parameters up as an ARM output if these are generated by a previous ARM template. 

#### Referencing Resource files from your function
If you have files stored as resources within your webjob, you can safely migrate these over to functions by moving the files/folders contained in your webjob project to the root of your function's folder. For eg. if you had files a.txt, b.csv & c.dat under a Resources folder in your webjob project for webjob "A", the path would like the following: 
```bash
A\Resources\a.txt
A\Resources\b.csv
A\Resources\c.dat
```
To migrate these to functions, simply move this entire folder over to the folder for your specific function. i.e. 
```bash
core\functions\A\Resources\a.txt
core\functions\A\Resources\b.csv
core\functions\A\Resources\c.dat
```

To access these files from your function, use the ```CiqsWebHostHelper.GetFunctionWebHostPath()``` utility function to access the root folder of the specific function. For eg. to get a path reference to ```a.txt```:

```csharp
    var fileName = "a.txt";
    var functionRootPath = CiqsWebHostHelper.GetFunctionWebHostPath("CreateBlob");
    var filePath = string.Format(@"{0}\resources\{1}", functionRootPath, fileName);
```
#### Add Reference to Storage Account in Function App Declaration

This step is crucial to ensure you can view logs on the function app. Without this, the logs tab on the functions app will be effectively useless.

```xml
    <AzureFunctionApp alwaysOn="true">
      <AppSettings>
        <Add key="AzureWebJobsStorage" value="DefaultEndpointsProtocol=https;AccountName={Outputs.storageAccountName};AccountKey={Outputs.storageAccountKey}" />
        <Add key="AzureWebJobsDashboard" value="DefaultEndpointsProtocol=https;AccountName={Outputs.storageAccountName};AccountKey={Outputs.storageAccountKey}" />
      </AppSettings>
    </AzureFunctionApp>
```

#### Remove Unused Storage Accounts
Adding a function app will create a storage account for you. Consider referencing this storage account for your solution to avoid additional costs and reduce solution complexity.

#### Migrate Package References
Ensure you move any package references you may have in your webjob from ```packages.config``` to a new ```projects.json``` file in the function root. For eg. the following line in ```packages.config```
```
  <package id="Microsoft.Azure.KeyVault.Core" version="1.0.0" targetFramework="net45" />
```
would map to:
```json
{
  "frameworks": {
    "net45":{
      "dependencies": {
        "Microsoft.Azure.KeyVault.Core": "1.0.0"
      }
    }
  }
}
```
#### FAQs
##### How do I view logs for function invocations ? 
- Go to your function app under the resource group.
- Select the function you wish to debug. 
- Go to ```Monitor``` to view past HTTP requests and their trace logs. 
- **Note that none of this will be visible until you pass the storage account into the AzureFunctionApp tag in the Manifest**. [Refer here for details on settings this up](#add-reference-to-storage-account-in-function-app-declaration).

##### How do I debug a function ? 
- Go to your function app under the resource group.
- Select the function you wish to debug. 
- On the right pane, select ```Test``` and enter a ```Request Body```. The request body is a json object holding parameters for your function. For eg. invoking function A with parameters : 
```chsarp
A(string v, string t, string m);
```
will have a request body of:
```json
{
    "v" : "xxxxx",
    "t" : "yyyyy",
    "m" : "zzzzz"
}
```
[More details here](https://docs.microsoft.com/en-us/azure/azure-functions/functions-test-a-function).

### WebJob
### AzureMLWebService
* **Summary**

  AzureMlWebService is a first-party provisioning step that empowers solution authors provisioning an Azure Machine Learning experiment from gallery and then deploying as a web service easily. This feature empowers pattern authors to:
  
  1) Provision an Azure ML experiment from [Cortana Intelligence Gallery](https://gallery.cortanaintelligence.com/experiments) by simply providing the `GalleryUrl`;
  
  2) Modify the **Experiment Graph** of the provisioned Azure ML experiment with a customized funciton plug-in;
  
  3) Deploy multiple Azure ML web services;
  
  4) Create high-throughput Azure ML web service as an option.

* **Documentation**
  * __&lt;AzureMlWebService/&gt;__
    
    To use this feature, please specify `<AzureMlWebService/>` within `<ProvisioningSteps/>` in your solution **Manifest.xml**.
    
    * **Attributes**
    
      | Name    | Description |
      | ------------ | -------------|
      | *title*: `string` | The title to be displayed in CIQS deployment page |
      | *hiddenParameters*: `boolean` | Set to `true` if this step is supposed to be automated; otherwise `false` | 

    * **Properties**
    
      | Name | Description |
      | ------------ | ------------- |
      | *GalleryUrl*: `string` | The url of the experiment to be provisioned on Gallery, such as: https://gallery.cortanaintelligence.com/Details/nyc-taxi-binary-classification-scoring-exp-2 |
      | *Outputs*: `object` | Allows users to overwrite the outputs; This is useful when provisioning more than one Azure ML experiments so that the outputs of each other will not collide |
      | *HighThroughputEndpoint*: `object` | Allows users to create a high-throughput Web service endpoint |
      | *ModifyExperimentGraph*: `object` | Allows users to plug-in a custom function to modify the experiment graph of the provisioned experiment |
      
      * __&lt;Outputs/&gt;__ (*Optional*)

        This tag is used to **overwrite** the default output names, so that outputs from multiple *AzureMlWebService* provisionings will not overwrite each other.

        **Attributes**

        All properties below can be used as attributes in **Camel Case**. Please see a Github sample [here](https://github.com/Azure/Azure-CortanaIntelligence-SolutionAuthoringWorkspace/blob/master/Samples/013-mlwebsvcs/core/Manifest.xml).

        **Properties**
        
        | Name | Description |
        | ------------ | ------------- |
        | *ExperimentUrl*: `string` | Overwriting the output name of the Azure ML experiment url |
        | *WebServiceApiUrl*: `string` | Overwriting the output name of the Azure ML web service api url |
        | *WebServiceApiKey*: `string` | Overwriting the output name of the Azure ML web service api key |
        | *WebServiceHelpUrl*: `string` | Overwriting the output name of the Azure ML web service help url |

      * __&lt;HighThroughputEndpoint/&gt;__ (*Optional*)

        This tag is used to create a high-throughput Web service endpoint as a result of this provisioning step.

        **Attributes**

        All properties below can be used as attributes in **Camel Case**. Please see a Github sample [here](https://github.com/Azure/Azure-CortanaIntelligence-SolutionAuthoringWorkspace/blob/master/Samples/013-mlwebsvcs/core/Manifest.xml).

        **Properties**
        
        | Name | Description |
        | ------------ | ------------- |
        | *EndpointName*: `string` | Endpoint names must be 24 character or less in length, and must be made up of lower-case letters or numbers; If not specified, the default value is "`secondep`" |
        | *ThrottleLevel*: `string` | Allowed values: `High` or `Low`; If not specified, the default value is `Low` |
        | *MaxConcurrentCalls*: `int` | The maximum concurrent calls for Azure ML Web service is between 1 and 200. See [here](https://github.com/hning86/azuremlps#new-amlwebservice) for details; If not specified, the default value is `4` |

      * __&lt;ModifyExperimentGraph/&gt;__ (*Optional*)

        This tag is used to plug-in a customized function to modify the experiment graph of the provisioned experiment.

        **Attributes**
        
        | Name | Description |
        | ------------ | ------------- |
        | *name*: `string` | **The name of** the customized function to modify the ML experiment graph |

        **Properties**
        
        | Name | Description |
        | ------------ | ------------- |
        | *Parameters*: `array` | Array of &lt;Parameter/&gt;s that are used to modify the ML experiment graph in the customized function |

        **Examples**

        The sample code in **Manifest.xml**:
        ```xml
        <AzureMlWebService title="Creating Energy Forecasting ML Web service" hiddenParameter="true">
          <GalleryUrl>https://gallery.cortanaintelligence.com/Details/975ed028d71b490b9268d35094138358</GalleryUrl>
          <ModifyExperimentGraph name="<Custom_function_name>">
            <Parameters>
              <Parameter type="string" name="sqlServer" defaultValue="{Outputs.sqlServerName}" description="SQL Server Name"/>
              <Parameter type="string" name="sqlUser" defaultValue="{Outputs.sqlServerUserName}" description="SQL Server User Name"/>
              <Parameter type="string" name="sqlPassword" defaultValue="{Outputs.sqlServerPassword}" description="SQL Server Password"/>
              <Parameter type="string" name="sqlDatabase" defaultValue="{Outputs.databaseName}" description="SQL Server Database Name"/>
            </Parameters>
          </ModifyExperimentGraph>
        </AzureMlWebService>
        ```

        Besides, a custom function named "_Custom_function_name_" need to be added into the solution alongside with the above code snippet. In this custom function, all you need to do is to get the "_graphJsonObject_" parameter and then return the modified value as the function output.
        ```c#
        /* Kudos to Richin Jain <rijai@microsoft.com> for contributing the sample code */
        
        #load "..\CiqsHelpers\All.csx"
        #r "System.Web"
        #r "System.Web.Extensions"
        
        using System.Text;
        using System.Web.Script.Serialization;

        public static async Task<object> Run(HttpRequestMessage req, TraceWriter log)
        {
            var parametersReader = await CiqsInputParametersReader.FromHttpRequestMessage(req);
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            dynamic graphJsonObject = serializer.Deserialize<object>(parametersReader.GetParameter<string>("graphJsonObject"));

            /* Do whatever is needed on the graph JsonObject and return the graph JsonObject */
            string sqlServerName = parametersReader.GetParameter<string>("sqlServer") + ".database.windows.net,1433"; 
            string sqlServerUserName = parametersReader.GetParameter<string>("sqlUser"); 
            string sqlServerPassword = parametersReader.GetParameter<string>("sqlPassword"); 
            string databaseName = parametersReader.GetParameter<string>("sqlDatabase"); 
            var moduleNodes = graphJsonObject["ModuleNodes"];
            foreach (var moduleNode in moduleNodes)
            {
                var moduleParameters = moduleNode["ModuleParameters"];
                foreach (var moduleParameter in moduleParameters)
                {
                    string parameterName = moduleParameter["Name"];
                    switch (parameterName)
                    {
                        case "Database Server Name":
                            moduleParameter["Value"] = sqlServerName;
                            break;
                        case "Server User Account Name":
                            moduleParameter["Value"] = sqlServerUserName;
                            break;
                        case "Server User Account Password":
                            moduleParameter["Value"] = sqlServerPassword;
                            break;
                        case "Database Name":
                            moduleParameter["Value"] = databaseName;
                            break;
                    }
                }
            }
            
            /* Return the modified graph JsonObject as the function output */
            return graphJsonObject;
        }
        ```

    * **Default Outputs**
    
      | Name | Description
      | ------------ | ------------- |
      | *experimentUrl*: `string` | The Azure ML experiment url |
      | *webServiceApiUrl*: `string` | The Azure ML web service api url |
      | *webServiceApiKey*: `string` | The Azure ML web service api key |
      | *webServiceHelpUrl*: `string` | The Azure ML web service help url |
      | *mlListExperimentsUrl*: `string` | The Azure ML experiments list url |

* **Examples**
  
  Please see a Github sample [here](https://github.com/Azure/Azure-CortanaIntelligence-SolutionAuthoringWorkspace/tree/master/Samples/013-mlwebsvcs).
  
  The simplest use case would be provisioning a single Azure ML experiment and deploying a default Web service endpoint:
    ```xml
    <AzureMlWebService title="Copying and experiment from Gallery and deploy as Web Service" hiddenParameters ="true">
      <GalleryUrl>https://gallery.cortanaintelligence.com/Details/nyc-taxi-binary-classification-scoring-exp-2</GalleryUrl>
    </AzureMlWebService>
    ```
  A slightly more complex use case would be creating a high-throughput Web service endpoint for the experiment:
    ```xml
    <AzureMlWebService title="Copying and experiment from Gallery and deploy as Web Service" hiddenParameters ="true">
      <GalleryUrl>https://gallery.cortanaintelligence.com/Details/nyc-taxi-binary-classification-scoring-exp-2</GalleryUrl>
      <HighThroughputEndpoint>
        <EndpointName>HighThroughputEndpoint</EndpointName>
        <ThrottleLevel>High</ThrottleLevel>
        <MaxConcurrentCalls>100</MaxConcurrentCalls>
      </HighThroughputEndpoint>
    </AzureMlWebService>
    ```
  Another common use case would be provisioning multiple Azure ML experiments, with output overwritten to avoid conflicts:
  ```xml
  <AzureMlWebService title="Copying NYC taxi xperiment from Gallery and deploy as Web Service" hiddenParameters ="true">
      <GalleryUrl>https://gallery.cortanaintelligence.com/Details/nyc-taxi-binary-classification-scoring-exp-2</GalleryUrl>
      <Outputs experimentUrl="mlFunctionEndpoint1" webServiceApiUrl="mlFunctionApiUrl1" webServiceApiKey="mlFunctionApiKey1" />
    </AzureMlWebService>
    <AzureMlWebService title="Copying connected car Experiment from Gallery and deploy as Web Service" hiddenParameters ="true">
      <GalleryUrl>https://gallery.cortanaintelligence.com/Details/connected-cars-aml-v2-noreader-scoring-exp-2</GalleryUrl>
      <Outputs>
        <ExperimentUrl>mlFunctionEndpoint2</ExperimentUrl>
        <WebServiceApiUrl>mlFunctionApiUrl2</WebServiceApiUrl>
        <WebServiceApiKey>mlFunctionApiKey2</WebServiceApiKey>
        <WebServiceHelpUrl>mlFunctionHelpUrl2</WebServiceHelpUrl>
      </Outputs>
    </AzureMlWebService>
    <AzureMlWebService title="Copying predictive maintenance Experiment from Gallery and deploy as Web Service" hiddenParameters ="true">
      <GalleryUrl>https://gallery.cortanaintelligence.com/Details/bcae226bc74a4cbbb0ff700ac97448bf</GalleryUrl>
    </AzureMlWebService>
  ```

### SolutionDashboard
### WebJobDeployment
