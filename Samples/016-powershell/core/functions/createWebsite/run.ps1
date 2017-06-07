# read function input parameters passed by CIQS
$input = (Get-Content $req) -join "`n" | ConvertFrom-Json

# use AccessToken from CIQS input parameters along with environment variables created from AppSettings
# to log into the Azure account
Add-AzureRmAccount -AccessToken $input.accessToken -AccountId $env:AccountId -SubscriptionId $env:SubscriptionId

$resourceGroupName = $input.resourceGroupName
$storageAccountName = $input.storageAccountName

$storageKey = (Get-AzureRmStorageAccountKey -Name $storageAccountName -ResourceGroupName $resourceGroupName).Value[0]
$storageContext = New-AzureStorageContext -StorageAccountName $storageAccountName -StorageAccountKey $storageKey

New-AzureStorageContainer -Name "`$root" -Context $storageContext -Permission Container
Set-AzureStorageBlobContent -Context $storageContext -Container "`$root" -File $env:HOME\site\wwwroot\greet\index.html -Blob index.html

# return output values
$output = @{
    url = "https://" + $storageAccountName + ".blob.core.windows.net/index.html"
}
$output | ConvertTo-Json | Out-File $res
