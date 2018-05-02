---
layout: default
title: REST API
navigation_weight: 8
---

# Programmatic REST API solution deployments

## PowerShell

```
param (
    [Parameter(Mandatory = $true)]
    [string]$template,
    [Parameter(Mandatory = $true)]
    [string]$resourceGroup,
    [Parameter(Mandatory = $true)]
    [string]$location,
    [string]$endpoint = "https://ciqs-api-westus.azurewebsites.net/",
    [string]$solutionStorageConnectionString,
    [string]$inputParametersFile
)

$ErrorActionPreference = 'Stop'

function Get-AccessToken($tenantId) {
    $cache = [Microsoft.IdentityModel.Clients.ActiveDirectory.TokenCache]::DefaultShared
    $cacheItem = $cache.ReadItems() | Where {$_.TenantId -eq $tenantId} | Select-Object -First 1
    if ($cacheItem -eq $null) {
        $cacheItem = $cache.ReadItems() | Select-Object -First 1
    }

    return $cacheItem.AccessToken
}

function WaitForDeployment {
    param
    (
        [Parameter(Mandatory = $true)]
        $Subscrption,

        [Parameter(Mandatory = $true)]
        $UniqueId
    )

    do
    {
        Start-Sleep -m 1000
        $deploymentDetails = Invoke-RestMethod "${endpoint}api/deployments/${Subscription}/${UniqueId}" -Headers $header -Method GET -ContentType "application/json"
        $deployment = $deploymentDetails.deployment
        $provisioningSteps = $deploymentDetails.provisioningSteps
        $status = $deployment.status


        if ($provisioningSteps -ne $null) {
            $currentProvisioningStep = $provisioningSteps[$deployment.currentProvisioningStep]
            $message = $currentProvisioningStep.Title
            if ($status -notlike 'ready') {
                $message = "$message..."
            } else {
                $message = "$message!"
            }
        } else {
            $message = "Deployment is being created..."
        }

        if ($oldMessage -ne $message) {
            Write-Host $message
        }

        $oldMessage = $message
    }
    while ($status -notlike 'failed' -and $status -notlike 'actionRequired' -and $status -notlike 'ready')

    return $deploymentDetails
}

$tenantId = (Get-AzureRmContext).Tenant.TenantId
$subscription = (Get-AzureRmContext).Subscription.SubscriptionId
$token = Get-AccessToken $tenantId

$header = @{
    'Content-Type'  = 'application\json'
    'Authorization' = "Bearer $token"
}

$payload = @{
    Location = $location
    Name = $resourceGroup
    Subscription = $subscription
    TemplateId = $template
}

if ($solutionStorageConnectionString) {
    $payload.SolutionStorageConnectionString = $solutionStorageConnectionString
}

Write-Host "Creating new CIQS deployment of ${template} into ${resourceGroup}..."

$body = $payload | ConvertTo-Json
$deployment = Invoke-RestMethod "${endpoint}api/deployments" -Headers $header -Method POST -Body $body -ContentType "application/json"

Write-Host "New deployment:"
$deployment

$uniqueId = $deployment.uniqueId

do {
    $deploymentDetails = WaitForDeployment -Subscrption $subscription -UniqueId $uniqueId
    $status = $deploymentDetails.deployment.status

    if ($status -like 'actionRequired') {
        Write-Host "Submitting intput parameters..."

        $body = Get-Content -Raw -Path $inputParametersFile
        $ignore = Invoke-RestMethod "${endpoint}api/deployments/${subscription}/${uniqueId}" -Headers $header -Method PUT -Body $body -ContentType "application/json"
        Write-Host "Continuing provisioning..."
    }
}
while ($status -notlike 'failed' -and $status -notlike 'ready')

if ($status -notlike 'ready')
{
    throw "Deployment failed."
}

Write-Host "Final output:"
$deploymentDetails.provisioningSteps[-1].instructions.data
```

### Example usage

```
PS F:\> Login-AzureRmAccount


Environment           : AzureCloud
Account               : anivan@microsoft.com
TenantId              : 72f988bf-86f1-41af-91ab-2d7cd011db47
SubscriptionId        : 1a827f48-3a3d-40a1-a669-0de4d9ac9c00
SubscriptionName      : Microsoft Azure Internal Consumption
CurrentStorageAccount :



PS F:\> Select-AzureRmSubscription -SubscriptionId xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx


Environment           : AzureCloud
Account               : anivan@microsoft.com
TenantId              : xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx
SubscriptionId        : xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx
SubscriptionName      : My Subscription
CurrentStorageAccount :



PS F:\> .\deploy.ps1 -template ciqs-helloworld -resourceGroup hw03 -location eastus
Creating new CIQS deployment of ciqs-helloworld into hw03...
New deployment:


uniqueId                          : xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx
name                              : hw03
templateId                        : ciqs-helloworld
templateTitle                     : ciqs-helloworld
subscription                      : xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx
location                          : eastus
dateCreated                       : 2018-05-02T17:58:30.0373513+00:00
dateLastModified                  : 0001-01-01T00:00:00+00:00
status                            : created
createdByUserEmail                : anivan@microsoft.com
createdByUserFriendlyName         : Andrew Ivanov
serializedOperationStatus         :
currentProvisioningStep           : 0
isCustomSolution                  : False
isOrphanedDeployment              : False
isDeleteRequested                 : False
isDeployedToExistingResourceGroup : False
isImmlAdsUser                     : False
environment                       : prod
tagGuid                           : xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx

Deployment is being created...
Deploying a blank ARM template...
Done!
Final output:
Hello world!
```

