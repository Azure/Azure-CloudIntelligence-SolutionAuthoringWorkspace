---
layout: default
title: FAQ
navigation_weight: 1
---
# Frequently Asked Questions
## Common Deployment Failures
This section describes some common CIQS deployment errors you may encounter, and provides information to resolve the errors. For more information on Azure Resource Management (ARM) errors, please see [Troubleshoot common Azure deployment errors with Azure Resource Manager](https://docs.microsoft.com/en-us/azure/azure-resource-manager/resource-manager-common-deployment-errors).

### ArmValidationFailed

This error code indicating that ARM pre-flight validation fails.

### Conflict

| Common error message | Mitigation/Resolution |
| ------------ | ------------- |
| *The maximum number of Free ServerFarms allowed in a Subscription is 10.* | Quota related issue. You can follow the instructions on [How to resolve quota issues](#how-to-resolve-quota-issue). If the problem persists, please [contact CIQS support](#how-to-contact-CIQS-support).

Once the issue is resolved, you can [retry the provisioning step](#how-to-retry-a-failed-provisioning-step). |
| *Not enough available reserved instance servers to satisfy this request. Currently 0 instances are available.* | Quota related issue. You can follow the instructions on [How to resolve quota issues](#how-to-resolve-quota-issue). If the problem persists, please [contact CIQS support](#how-to-contact-CIQS-support).

Once the issue is resolved, you can [retry the provisioning step](#how-to-retry-a-failed-provisioning-step). |

### BadRequest

| Common error message | Mitigation/Resolution |
| ------------ | ------------- |
| User SubscriptionId '99999999-9999-9999-9999-999999999999' does not have cores left to create resource 'spark-cluster'. Required: 32, Available: 28. | Quota related issue. You can follow the instructions on [How to resolve quota issues](#how-to-resolve-quota-issue). If problem persists, please [contact CIQS support](#how-to-contact-CIQS-support). Once the issue is resolved, you can [retry the provisioning step](#how-to-retry-a-failed-provisioning-step). |
| The region is not enabled for this subscription, please contact support for more information. | This is subscription restriction issue. You can try to [contact Azure support](#how-to-submit-azure-support-request) or [contact your subscription admin](#how-to-contact-subscription-admin). |

### ExceededMaxAccountCount

### How to submit Azure support request?

### How to contact CIQS support?

### How to contact subscription admin?

### How to resolve quota issues?

### How to retry a failed provisioning step?
