param (
    [Parameter(Mandatory=$true)][string]$ARMOutput
    )

#region Convert from json
$json = $ARMOutput | convertfrom-json
#endregion

Write-Host " Krok 1"

Write-Output -InputObject ('Connection string {0} ' -f $json.NamespaceConnectionString.value)

Write-Host " Krok 2"

$constring = $json.NamespaceConnectionString.value

Write-Host "##vso[task.setvariable variable=AzureServiceBus.ConnectionString]$constring"

Write-Host " Krok 3"