---
layout: default
title: FAQ
navigation_weight: 1
---
# Frequently Asked Questions
## Common Deployment Failures
This section describes some common CIQS deployment error codes you may encounter, and provides information to resolve the errors. For more information on Azure Resource Management (ARM) errors, please see [Troubleshoot common Azure deployment errors with Azure Resource Manager](https://docs.microsoft.com/en-us/azure/azure-resource-manager/resource-manager-common-deployment-errors).

### ArmValidationFailed
This error code indicating that ARM pre-flight validation fails. ARM pre-flight validation could fail for various reasons; Some of them are actionable/retriable, such as [quota increase](#how-to-resolve-quota-issue); Some of them are non-recoverable, in which case users need to re-deploy the solution or [contact CIQS support](#how-to-contact-CIQS-support).

| Sub Code | Detailed Message | Mitigation/Resolution |
| -------- | ------------ | ------------- |
| *QuotaExceeded* | *Operation results in exceeding quota limits of Core. Maximum allowed: 100, Current in use: 86, Additional requested: 18. Please read more about quota increase at http://aka.ms/corequotaincrease.* | Quota related issue. You can follow the instructions on [How to resolve quota issues](#how-to-resolve-quota-issue). If the problem persists, please [contact CIQS support](#how-to-contact-CIQS-support). Once the issue is resolved, you can [retry the provisioning step](#how-to-retry-a-failed-provisioning-step). |
| *BadRequest** | *Operation results in exceeding quota limits of Core. Maximum allowed: 20, Current in use: 8, Additional requested: 16.* | Quota related issue. You can follow the instructions on [How to resolve quota issues](#how-to-resolve-quota-issue). If the problem persists, please [contact CIQS support](#how-to-contact-CIQS-support). Once the issue is resolved, you can [retry the provisioning step](#how-to-retry-a-failed-provisioning-step). |
| *DisallowedProvider* | *The operation is not permitted for namespace 'Microsoft.Storage'. List of permitted provider namespaces is 'Microsoft.Authorization,Microsoft.Features,microsoft.insights,Microsoft.NotificationHubs,Microsoft.Resources,Microsoft.Sql,microsoft.support,microsoft.visualstudio,Microsoft.Web,SuccessBricks.ClearDB* | This error indicates that your subscription does not have access to the resource 'Microsoft.Storage', or you do not have permission to register 'Microsoft.Storage'. In this case you can ask the subscription admin to register the resource to unblock your deployment. Please go to Azure portal (portal.azure.com) -> Subscriptions -> select the subscription -> Resource providers -> Microsoft.Storage. Once the resource provider is registered, you can [retry the provisioning step]. |

### Conflict

| Common error message | Mitigation/Resolution |
| ------------ | ------------- |
| *The maximum number of Free ServerFarms allowed in a Subscription is 10.* | Quota related issue. You can follow the instructions on [How to resolve quota issues](#how-to-resolve-quota-issue). If the problem persists, please [contact CIQS support](#how-to-contact-CIQS-support). Once the issue is resolved, you can [retry the provisioning step](#how-to-retry-a-failed-provisioning-step). |
| *Not enough available reserved instance servers to satisfy this request. Currently 0 instances are available.* | Quota related issue. You can follow the instructions on [How to resolve quota issues](#how-to-resolve-quota-issue). If the problem persists, please [contact CIQS support](#how-to-contact-CIQS-support). Once the issue is resolved, you can [retry the provisioning step](#how-to-retry-a-failed-provisioning-step). |

### BadRequest

| Common error message | Mitigation/Resolution |
| ------------ | ------------- |
| *User SubscriptionId '99999999-9999-9999-9999-999999999999' does not have cores left to create resource 'spark-cluster'. Required: 32, Available: 28.* | Quota related issue. You can follow the instructions on [How to resolve quota issues](#how-to-resolve-quota-issue). If problem persists, please [contact CIQS support](#how-to-contact-CIQS-support). Once the issue is resolved, you can [retry the provisioning step](#how-to-retry-a-failed-provisioning-step). |
| *The region is not enabled for this subscription, please contact support for more information.* | This is subscription restriction issue. You can try to [contact Azure support](#how-to-submit-azure-support-request) or [contact your subscription admin](#how-to-contact-subscription-admin). |

### ExceededMaxAccountCount

| Common error message | Mitigation/Resolution |
| ------------ | ------------- |
| *The maximum number of Free ServerFarms allowed in a Subscription is 10.* | Quota related issue. You can follow the instructions on [How to resolve quota issues](#how-to-resolve-quota-issue). If the problem persists, please [contact CIQS support](#how-to-contact-CIQS-support). Once the issue is resolved, you can [retry the provisioning step](#how-to-retry-a-failed-provisioning-step). |

### MaxOnDemandHdiCoresReached

| Common error message | Mitigation/Resolution |
| ------------ | ------------- |
| *The maximum number of Free ServerFarms allowed in a Subscription is 10.* | Quota related issue. You can follow the instructions on [How to resolve quota issues](#how-to-resolve-quota-issue). If the problem persists, please [contact CIQS support](#how-to-contact-CIQS-support). Once the issue is resolved, you can [retry the provisioning step](#how-to-retry-a-failed-provisioning-step). |

### QuotaExceeded

| Common error message | Mitigation/Resolution |
| ------------ | ------------- |
| *The maximum number of Free ServerFarms allowed in a Subscription is 10.* | Quota related issue. You can follow the instructions on [How to resolve quota issues](#how-to-resolve-quota-issue). If the problem persists, please [contact CIQS support](#how-to-contact-CIQS-support). Once the issue is resolved, you can [retry the provisioning step](#how-to-retry-a-failed-provisioning-step). |

### SubscriptionQuotaExceeded

| Common error message | Mitigation/Resolution |
| ------------ | ------------- |
| *The maximum number of Free ServerFarms allowed in a Subscription is 10.* | Quota related issue. You can follow the instructions on [How to resolve quota issues](#how-to-resolve-quota-issue). If the problem persists, please [contact CIQS support](#how-to-contact-CIQS-support). Once the issue is resolved, you can [retry the provisioning step](#how-to-retry-a-failed-provisioning-step). |

### OperationNotAllowed

| Common error message | Mitigation/Resolution |
| ------------ | ------------- |
| *The maximum number of Free ServerFarms allowed in a Subscription is 10.* | Quota related issue. You can follow the instructions on [How to resolve quota issues](#how-to-resolve-quota-issue). If the problem persists, please [contact CIQS support](#how-to-contact-CIQS-support). Once the issue is resolved, you can [retry the provisioning step](#how-to-retry-a-failed-provisioning-step). |

### AccountNameInvalid

| Common error message | Mitigation/Resolution |
| ------------ | ------------- |
| *The maximum number of Free ServerFarms allowed in a Subscription is 10.* | Quota related issue. You can follow the instructions on [How to resolve quota issues](#how-to-resolve-quota-issue). If the problem persists, please [contact CIQS support](#how-to-contact-CIQS-support). Once the issue is resolved, you can [retry the provisioning step](#how-to-retry-a-failed-provisioning-step). |

### 15021

| Common error message | Mitigation/Resolution |
| ------------ | ------------- |
| *The maximum number of Free ServerFarms allowed in a Subscription is 10.* | Quota related issue. You can follow the instructions on [How to resolve quota issues](#how-to-resolve-quota-issue). If the problem persists, please [contact CIQS support](#how-to-contact-CIQS-support). Once the issue is resolved, you can [retry the provisioning step](#how-to-retry-a-failed-provisioning-step). |

### 40632

| Common error message | Mitigation/Resolution |
| ------------ | ------------- |
| *The maximum number of Free ServerFarms allowed in a Subscription is 10.* | Quota related issue. You can follow the instructions on [How to resolve quota issues](#how-to-resolve-quota-issue). If the problem persists, please [contact CIQS support](#how-to-contact-CIQS-support). Once the issue is resolved, you can [retry the provisioning step](#how-to-retry-a-failed-provisioning-step). |

### 45120

| Common error message | Mitigation/Resolution |
| ------------ | ------------- |
| *The maximum number of Free ServerFarms allowed in a Subscription is 10.* | Quota related issue. You can follow the instructions on [How to resolve quota issues](#how-to-resolve-quota-issue). If the problem persists, please [contact CIQS support](#how-to-contact-CIQS-support). Once the issue is resolved, you can [retry the provisioning step](#how-to-retry-a-failed-provisioning-step). |

### 45122

| Common error message | Mitigation/Resolution |
| ------------ | ------------- |
| *Certain regions do not support Data Warehouse provisioning. Please try another region. For more information, please contact Microsoft support. (Code: 45122, ResourceType: Microsoft.Sql/servers/databases, ResourceName: sqlservername)* | * Redeploy with another region |

### SkuNotAvailable

| Common error message | Mitigation/Resolution |
| ------------ | ------------- |
| *The requested tier for resource 'resourcePath' is currently not available in location 'westus' for subscription '99999999-9999-9999-9999-999999999999'. Please try another tier or deploy to a different location. (Code: SkuNotAvailable, ResourceType: Microsoft.Compute/virtualMachines, ResourceName: resourceName)* | * Use [Azure CLI](https://docs.microsoft.com/en-us/cli/azure/overview?view=azure-cli-latest) command `az vm image --all` to check regional availability for your subscription; * Redeploy the solution with another tier or in another region * If the problem persists, please [contact CIQS support](#how-to-contact-CIQS-support) |


### How to submit Azure support request?

### How to contact CIQS support?

### How to contact subscription admin?

### How to resolve quota issues?

### How to retry a failed provisioning step?
