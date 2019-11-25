param (
    [Parameter(Mandatory=$true)][string]$ARMOutput
    )

#region Convert from json
$json = $ARMOutput | convertfrom-json
#endregion


Write-Output -InputObject ('Connection string {0} ' -f $json.NamespaceConnectionString.value)
Write-Host "##vso[task.setvariable variable=AzureServiceBus.ConnectionString;]$json.NamespaceConnectionString.value"

Write-Host "Connection string je: $(AzureServiceBus.ConnectionString)"