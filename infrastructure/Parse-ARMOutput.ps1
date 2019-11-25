param (
    [Parameter(Mandatory=$true)][string]$ARMOutput
    )

#region Convert from json
$json = $ARMOutput | convertfrom-json
#endregion

#region Parse ARM Template Output
Write-Output -InputObject ('Connection string {0} ' -f $json.NamespaceConnectionString.value)
#endregion