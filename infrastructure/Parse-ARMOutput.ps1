param (
    [Parameter(Mandatory=$true)][string]$ARMOutput
    )

#region Convert from json
$json = $ARMOutput | convertfrom-json
#endregion

$serviceBusConnection = $json.NamespaceConnectionString.value
Write-Host "##vso[task.setvariable variable=AzureServiceBus.ConnectionString]$serviceBusConnection"

$databaseConnection = $json.DatabaseConnectionString.value
Write-Host "##vso[task.setvariable variable=ConnectionStrings.DefaultConnection]$databaseConnection"

$appServiceName = $json.AppServiceName.value
Write-Host "##vso[task.setvariable variable=fri_photos_service_name]$appServiceName"

$appFunctionName = $json.AppFunctionName.value
Write-Host "##vso[task.setvariable variable=fri_photos_function_name]$appFunctionName"

$storageAccountConnectionString = $json.AppServiceName.value
Write-Host "##vso[task.setvariable variable=AzureWebJobsStorage]$storageAccountConnectionString"
