# Frequently Asked Questions
## Common Deployment Failures
This section describes some common CIQS deployment errors you may encounter, and provides information to resolve the errors. For more information on Azure Resource Management (ARM) errors, please see [Troubleshoot common Azure deployment errors with Azure Resource Manager](https://docs.microsoft.com/en-us/azure/azure-resource-manager/resource-manager-common-deployment-errors) for more details.

### AuthorizationFailed
### Unauthorized
### InvalidTemplateDeployment
| Sample messages | Mitigation or resolution |
| ------------ | ------------- |
| AuthorizationFailed: The client 'bekeh@contoso.com' with object id 'eac01513-cbdf-42a6-8b81-ba4ed3eb6414' does not have authorization to perform action 'Microsoft.Resources/subscriptions/resourcegroups/write' over scope '/subscriptions/abcd1234-ef56-ghijklm78901/resourcegroups/bokehrg'. | <ul><li>[Contact your subscription admin](#how-to-contact-subscription-admin)</li><li>Once you are granted proper authorization, you can [retry the provisioning step](#how-to-retry-a-failed-provisioning-step)</li></ul> |
| RequestDisallowedByPolicy: Resource 'ivrbottevbe7nvhqta2' was disallowed by policy. Policy identifiers: "policyDefinitionId":"/providers/Microsoft.Authorization/policyDefinitions/..." | <ul><li>[Contact your subscription admin](#how-to-contact-subscription-admin)</li><li>Once you are granted proper authorization, you can [retry the provisioning step](#how-to-retry-a-failed-provisioning-step)</li></ul> |

### DeploymentInterrupted
"DeploymentInterrupted" is a CIQS failure code, indicating a catastrophic human intevention is detected during the automatic provisionings, causing solution deployment to fail. If this is the case, please consider to [contact CIQS support](#how-to-contact-ciqs-support).

### ResourceGroupNotFound
### ResourceGroupBeingDeleted
"ResourceGroupNotFound" and "ResourceGroupBeingDeleted" happen when someone manual deletes the resource group from Azure portal in the middle of solution provisioning. In this case, the CIQS solution deployment status will be signified as "Interrupted". For more information, please see [Troubleshoot common Azure deployment errors with Azure Resource Manager](https://docs.microsoft.com/en-us/azure/azure-resource-manager/resource-manager-common-deployment-errors).

### StorageAccountOperationInProgress
This error indicates a write operation for the storage account is in progress. In this case, the CIQS solution deployment status will be signified as "Interrupted". You can [retry the provisioning step](#how-to-retry-a-failed-provisioning-step); if it doesn't help, please consider [contact CIQS support](#how-to-contact-ciqs-support) or [contact Azure support](#how-to-submit-azure-support-request). For more information on Storage Resource Manager, please see [Common Error Codes for Storage Resource Manager](https://docs.microsoft.com/en-us/rest/api/storagerp/srp_error_codes_common).

### ArmValidationFailed
This error code indicates that ARM pre-flight validation fails. ARM pre-flight validation could fail for various reasons: Some of them are actionable/retriable, such as [quota exceeded](#how-to-request-quota-increase); Some of them are non-recoverable; in this case, please [contact Azure support](#how-to-submit-azure-support-request) or [contact CIQS support](#how-to-contact-ciqs-support).

| Error code | Sample messages | Mitigation or resolution |
| -------- | ------------ | ------------- |
| <ul><li>*QuotaExceeded*<li>*MaxStorageAccountsCountPerSubscriptionExceeded*</li></li></ul> | <ul><li>*Operation results in exceeding quota limits of Core. Maximum allowed: 100, Current in use: 86, Additional requested: 18. Please read more about quota increase at http://aka.ms/corequotaincrease.*</li><li>*Subscription abcd1234-ef56-ghijklm78901 already contains 199 storage accounts and the maximum allowed is 200.*</li></ul> | <ul><li>If you are aware of the service type, you can delete some unused service from the Azure portal to release some quota</li><li>If you would like to increase the quota, you can follow the instructions to [request quota increase](#how-to-request-quota-increase). Once the quota is increased, you can [retry the provisioning step](#how-to-retry-a-failed-provisioning-step)</li><li>If the problem persists, please [contact CIQS support](#how-to-contact-ciqs-support)</li></ul> |
| *BadRequest* | *Operation results in exceeding quota limits of Core. Maximum allowed: 20, Current in use: 8, Additional requested: 16.* | <ul><li>If you are aware of the service type, you can delete some unused service from Azure portal to release some quota</li><li>If you would like to increase the quota, you can follow the instructions to [request quota increase](#how-to-request-quota-increase). Once the quota is increased, you can [retry the provisioning step](#how-to-retry-a-failed-provisioning-step)</li><li>If the problem persists, please [contact CIQS support](#how-to-contact-ciqs-support)</li></ul> |
| *DisallowedProvider* | *The operation is not permitted for namespace 'Microsoft.Storage'. List of permitted provider namespaces is 'Microsoft.Authorization, Microsoft.Features, Microsoft.insights, Microsoft.NotificationHubs, Microsoft.Resources, Microsoft.Sql'* | <ul><li>This error indicates that your subscription does not have access to the resource 'Microsoft.Storage', or you do not have permission to register 'Microsoft.Storage'. In this case you can [ask the subscription admin](#how-to-contact-subscription-admin) to [register the resource](#how-to-register-azure-resource-provider) to unblock your deployment</li><li>You can also go to Azure portal (portal.azure.com) -> Subscriptions -> select the subscription -> Resource providers -> Microsoft.Storage to manually register. Once the resource provider is registered, you can [retry the provisioning step](#how-to-retry-a-failed-provisioning-step)</li></ul> |

For errors not listed in the table, please [contact CIQS support](#how-to-contact-ciqs-support) for help.

### Conflict

| Sample messages | Mitigation or resolution |
| ------------ | ------------- |
| The maximum number of Free ServerFarms allowed in a Subscription is 10. | <ul><li>If you are aware of the service type, you can delete some unused service from Azure portal to release some quota</li><li>If you would like to increase the quota, you can follow the instructions to [request quota increase](#how-to-request-quota-increase). Once the quota is increased, you can [retry the provisioning step](#how-to-retry-a-failed-provisioning-step)</li><li>If the problem persists, please [contact CIQS support](#how-to-contact-ciqs-support)</li></ul> |
| *Not enough available reserved instance servers to satisfy this request. Currently 0 instances are available.* | <ul><li>If you are aware of the service type, you can delete some unused service from Azure portal to release some quota</li><li>If you would like to increase the quota, you can follow the instructions to [request quota increase](#how-to-request-quota-increase). Once the quota is increased, you can [retry the provisioning step](#how-to-retry-a-failed-provisioning-step)</li><li>If the problem persists, please [contact CIQS support](#how-to-contact-ciqs-support)</li></ul> |

For errors not listed in the table, please [contact CIQS support](#how-to-contact-ciqs-support) for help.

### BadRequest

| Sample messages | Mitigation or resolution |
| ------------ | ------------- |
| *User SubscriptionId 'abcd1234-ef56-ghijklm78901' does not have cores left to create resource 'spark-cluster'. Required: 32, Available: 28.* | <ul><li>If you would like to increase the quota, you can follow the instructions to [request quota increase](#how-to-request-quota-increase). Once the quota is increased, you can [retry the provisioning step](#how-to-retry-a-failed-provisioning-step)</li><li>If the problem persists, please [contact CIQS support](#how-to-contact-ciqs-support)</li></ul> |
| *The region is not enabled for this subscription, please contact support for more information.* | <ul><li>[Contact Azure support](#how-to-submit-azure-support-request)</li><li>[Contact your subscription admin](#how-to-contact-subscription-admin)</li></ul> |
| *RegionCapabilityNotAvailable, Region capability not available for region 'West Central US' and Subscription ID 'abcd1234-ef56-ghijklm78901'. (Code: BadRequest, ResourceType: Microsoft.HDInsight/clusters, ResourceName: hdicluster)* | <ul><li>This is a similar issue to [SkuNotAvailable](#skunotavailable), in which case the underlying compute sku is not available for some region in that subscription</li></ul> |
| *MarketplacePurchaseEligibilityFailed, Marketplace is not available in your Subscription/Azure account's regions: VN. Please switch to an Azure account from an allowed region. (Code: BadRequest, Offer with PublisherId: microsoft-ads, OfferId: windows-data-science-vm)* | <ul><li>This is also similar to [SkuNotAvailable](#skunotavailable), the region used is not allowed to deploy certain VM images.</li></ul> |

For errors not listed in the table, please [contact CIQS support](#how-to-contact-ciqs-support) for help.

### 40652
| Sample messages | Mitigation or resolution |
| ------------ | ------------- |
| *Cannot move or create server. Subscription 'abcd1234-ef56-ghijklm78901' will exceed server quota. (Code: 40652, ResourceType: Microsoft.Sql/servers, ResourceName: sqlservername)* | <ul><li>If you are aware of the service type, you can delete some unused service from Azure portal to release some quota</li><li>If you would like to increase the quota, you can follow the instructions to [request quota increase](#how-to-request-quota-increase). Once the quota is increased, you can [retry the provisioning step](#how-to-retry-a-failed-provisioning-step)</li><li>If the problem persists, please [contact CIQS support](#how-to-contact-ciqs-support)</li></ul> |

### ExceededMaxAccountCount

| Sample messages | Mitigation or resolution |
| ------------ | ------------- |
| *The maximum number of Free ServerFarms allowed in a Subscription is 10.* | <ul><li>If you are aware of the service type, you can delete some unused service from Azure portal to release some quota</li><li>If you would like to increase the quota, you can follow the instructions to [request quota increase](#how-to-request-quota-increase). Once the quota is increased, you can [retry the provisioning step](#how-to-retry-a-failed-provisioning-step)</li><li>If the problem persists, please [contact CIQS support](#how-to-contact-ciqs-support)</li></ul> |

For errors not listed in the table, please [contact CIQS support](#how-to-contact-ciqs-support) for help.

### MaxOnDemandHdiCoresReached

| Sample messages | Mitigation or resolution |
| ------------ | ------------- |
| *Unable to create or update an on-demand HDInsight cluster with 12 cores for subscription abcd1234-ef56-ghijklm78901. The Azure Data Factory quota for on-demand HDInsight cores for this subscription is 40, whereas 36 are currently reserved. If you would like to increase this quota, create a new Azure support request with issue type 'Quota' and quota type 'Data Factory'.* | <ul><li>You can  delete some unused <a href="https://ms.portal.azure.com/#blade/HubsExtension/Resources/resourceType/Microsoft.DataFactory%2FdataFactories" target="_blank">linked services</a> that use on-demand HDI clusters, to release some quota</li><li>If you would like to increase the quota, create a new Azure support request with issue type 'Quota' and quota type 'Data Factory'. Learn more about [how to resolve quota issues here](#how-to-request-quota-increase). Once the quota is increased, you can [retry the provisioning step](#how-to-retry-a-failed-provisioning-step)</li></ul> |

For errors not listed in the table, please [contact CIQS support](#how-to-contact-ciqs-support) for help.

### SubscriptionQuotaExceeded

| Sample messages | Mitigation or resolution |
| ------------ | ------------- |
| *The regional account quota for the specified subscription has been reached. (Code: SubscriptionQuotaExceeded, ResourceType: Microsoft.Batch/batchAccounts, ResourceName: batchname)* | <ul><li>If you are aware of the service type, you can delete some unused service from Azure portal to release some quota</li><li>You can follow the instructions to [request quota increase](#how-to-request-quota-increase). Once the quota is increased, you can [retry the provisioning step](#how-to-retry-a-failed-provisioning-step)</li><li>If the problem persists, please [contact CIQS support](#how-to-contact-ciqs-support)</li></ul> |

For errors not listed in the table, please [contact CIQS support](#how-to-contact-ciqs-support) for help.

### AccountLimitReached

| Sample messages | Mitigation or resolution |
| ------------ | ------------- |
| *The maximum number of allowed accounts was reached. (Code: AccountLimitReached, ResourceType: Sendgrid.Email/accounts, ResourceName: emailaccountname)* | <ul><li>If you are aware of the service type, you can delete unused accounts from Azure portal to release some quota</li><li>You can follow the instructions to [request quota increase](#how-to-request-quota-increase). Once the quota is increased, you can [retry the provisioning step](#how-to-retry-a-failed-provisioning-step)</li><li>If the problem persists, please [contact CIQS support](#how-to-contact-ciqs-support)</li></ul> |

### OperationNotAllowed

| Sample messages | Mitigation or resolution |
| ------------ | ------------- |
| *Operation results in exceeding quota limits of Core. Maximum allowed: 4, Current in use: 0, Additional requested: 8.* | <ul><li>If you are aware of the service type, you can delete some unused service from Azure portal to release some quota</li><li>If you would like to increase the quota, you can follow the instructions to [request quota increase](#how-to-request-quota-increase). Once the quota is increased, you can [retry the provisioning step](#how-to-retry-a-failed-provisioning-step)</li><li>If the problem persists, please [contact CIQS support](#how-to-contact-ciqs-support)</li></ul> |

For errors not listed in the table, please [contact CIQS support](#how-to-contact-ciqs-support) for help.

### MissingRegistrationForLocation

| Sample messages | Mitigation or resolution |
| ------------ | ------------- |
| *The subscription is not registered for the resource type 'components' in the location 'brazilsouth'. Please re-register for this provider in order to have access to this location. (Code: MissingRegistrationForLocation)* | <ul><li>[Register Azure resource provider manually](#how-to-register-azure-resource-provider)</li><li>[Contact Azure support](#how-to-submit-azure-support-request)</li><li>[Contact your subscription admin](#how-to-contact-subscription-admin)</li></ul> |

### SkuNotAvailable

| Sample messages | Mitigation or resolution |
| ------------ | ------------- |
| *The requested tier for resource 'resourcePath' is currently not available in location 'westus' for subscription 'abcd1234-ef56-ghijklm78901'. Please try another tier or deploy to a different location. (Code: SkuNotAvailable, ResourceType: Microsoft.Compute/virtualMachines, ResourceName: resourceName)* | <ul><li>Use [Azure CLI](https://docs.microsoft.com/en-us/cli/azure/overview?view=azure-cli-latest) command `az vm image --all` to check regional availability for your subscription</li><li>Redeploy the solution with another tier or in another region</li><li>If the problem persists, please [contact CIQS support](#how-to-contact-ciqs-support)</li></ul> |

### Provisioning timeout
| Error code | Sample messages | Mitigation or resolution |
| -------- | ------------ | ------------- |
| *OSProvisioningTimedOut* | *Deployment abcd1234-ef56-ghijklm78901 failed at becuase of The resource operation completed with terminal provisioning state 'Failed'. (Code: ResourceDeploymentFailure, ResourceType: Microsoft.Compute/virtualMachines, ResourceName: dsvmsql2)- OS Provisioning for VM 'dsvm' did not finish in the allotted time. The VM may still finish provisioning successfully. Please check provisioning state later. (Code: OSProvisioningTimedOut)* | <ul><li>[Retry the provisioning step](#how-to-retry-a-failed-provisioning-step)</li><li>If the problem persists, please [contact CIQS support](#how-to-contact-ciqs-support)</li></ul> |
| *VMExtensionProvisioningTimeout* | *Provisioning of VM extension 'Microsoft.EnterpriseCloud.Monitoring' has timed out. Extension installation may be taking too long, or extension status could not be obtained.* | <ul><li>[Retry the provisioning step](#how-to-retry-a-failed-provisioning-step)</li><li>If the problem persists, please [contact CIQS support](#how-to-contact-ciqs-support)</li></ul> |

For errors not listed in the table, please [contact CIQS support](#how-to-contact-ciqs-support) for help.

### 45120
Code '45120' usually indicates resource naming conflicts. The table lists commonly seen error messages.

| Sample messages | Mitigation or resolution |
| ------------ | ------------- |
| *The name 'churn.database.windows.net' already exists. Choose a different name.  (Code: 45120, ResourceType: Microsoft.Sql/servers, ResourceName: churn)* | <ul><li>This usually indicates a solution bug, please [contact CIQS support](#how-to-contact-ciqs-support) for help</li><li>To mitigate the issue and unblock your deployment, you can re-deploy with another name or delete the existing instance if unused</li></ul> |
| *Deployment abcd1234-ef56-ghijklm78901 failed at becuase of The name 'servernamesrv' already exists. Choose a different name. (Code: 45120, ResourceType: Microsoft.Sql/servers, ResourceName: servernamesrv).* | <ul><li>From Azure portal (https://portal.azure.com), delete the existing resource 'servernamesrv', and then [retry the provisioning step](#how-to-retry-a-failed-provisioning-step)</li><li>Create a new deployment with a different name</li><li>If the problem persists, please [contact CIQS support](#how-to-contact-ciqs-support)</li></ul> |

For errors not listed in the table, please [contact CIQS support](#how-to-contact-ciqs-support) for help.

### 45122
| Sample messages | Mitigation or resolution |
| ------------ | ------------- |
| *Australia East, Japan West, West India and France regions do not support Data Warehouse provisioning. Please try another region. For more information, please contact Microsoft support.* | <ul><li>Create a new deployment with another region.</li><li>[Contact Azure support](#how-to-submit-azure-support-request)</li></ul> |

### Invalid useruser or password
| Error code | Sample messages | Mitigation or resolution |
| -------- | ------------ | ------------- |
| *15021* | *Invalid value given for parameter Password. Specify a valid parameter value. (Code: 15021, ResourceType: Microsoft.Sql/servers, ResourceName: resourceName)* | [Contact CIQS support](#how-to-contact-ciqs-support) |
| *40616* | *'admin' is not a valid login name in this version of SQL Server.* | [Contact CIQS support](#how-to-contact-ciqs-support) |
| *BadRequest* | *Uppercase characters in cluster username are not allowed for version 3.5.* | [Contact CIQS support](#how-to-contact-ciqs-support) |

For errors not listed in the table, please [contact CIQS support](#how-to-contact-ciqs-support) for help.

### WebJobFailure
TBA

### FunctionFailure
TBA

### How to submit Azure support request?
In Azure portal (https://portal.azure.com), click "Help + support" from the right panel, and then click on "+ New support request". You can find "Help + support" from the question mark on the top right:

![](images/faq-5.png)

Follow instructions to create a support request. Please see a sample instruction in [How to request quota increase](#how-to-request-quota-increase). 

Another example: [Resource Manager vCPU core quota increase requests](https://docs.microsoft.com/en-us/azure/azure-supportability/resource-manager-core-quotas-request)

### How to request quota increase?

An example is available here: [Resource Manager vCPU core quota increase requests](https://docs.microsoft.com/en-us/azure/azure-supportability/resource-manager-core-quotas-request)

In the case of "[**MaxOnDemandHdiCoresReached**](#maxondemandhdicoresreached)", select issue type 'Quota' and quota type 'Data Factory':

![](images/faq-6.png)

Follow instructions and fill in problem descriptions as detailed as possible and attach files if necessary; Finally, click on "Create":

![](images/faq-7.png)

### How to contact CIQS support?

In CIQS gallery home page, you can find "Help & support" from the question mark on the top right.

![](images/faq-1.png)

In deployment detail page, you can find "Ask for help" link upon deployment failures.

![](images/faq-2.png)

Describe your issue as detailed as possible in the email body. Our on-call engineers will be ready to help you once we hear from you.

![](images/faq-3.PNG)

### How to contact subscription admin?

In Azure portal (https://portal.azure.com), click "Subscriptions" from the right panel and select your target subscription, and then click on "Access Control(IAM)". From there you will see contact info of the administrator(s) for that subscription.

![](images/faq-8.png)

### How to register Azure resource provider?

In some cases such as [*DisallowedProvider*](#armvalidationfailed) and [*MissingRegistrationForLocation*](#missingregistrationforlocation), you will need to register Azure resource provider manually in Azure portal (https://portal.azure.com). To register a resource provider, in Azure portal, **(1)** click on "Subscriptions" from the right panel and **(2)** select your target subscription. Then **(3)** click on "Resource providers".

![](images/register.png)

From "Resource providers" setting, search for the target resource provider, in this example, "Microsoft.AzureActiveDirectory", and then click "**Register**".

![](images/register2.png)

If you encountered issues in registering an Azure resource provider, such as "[AuthorizationFailed](#authorizationfailed)", please [contact your subscription admin](#how-to-contact-subscription-admin).

![](images/register3.png)

### How to retry a failed provisioning step?

Retrying a failed provisioning step is easy. In the deployment failure page, click the "retry" button. Before clicking the "retry" button, make sure that the previous failure is resolved or mitigated, such as [the quota limit has been increased](#how-to-request-quota-increase).

![](images/faq-4.png)
