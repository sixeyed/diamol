$serviceName='MSSQL$' + $env:MSSQL_INSTANCE_NAME
Write-Output "Starting SQL Server service: $serviceName"
Start-Service $serviceName

if ($env:MSSQL_MSSQL_SA_PASSWORD) {
    Write-Host 'Changing SA login credentials'
    $sqlcmd = "ALTER LOGIN sa with password='$env:MSSQL_MSSQL_SA_PASSWORD'; ALTER LOGIN sa ENABLE;"
    SqlServer\Invoke-SqlCmd -Query $sqlcmd -ServerInstance ".\$($env:MSSQL_INSTANCE_NAME)" -TrustServerCertificate
}
else {
    Write-Host 'WARNING: SA password not supplied in $env:MSSQL_SA_PASSWORD; using default'
}

Write-Host 'Started SQL Server.'

while ($true) {
    Start-Sleep -Seconds 30

    Get-WinEvent -FilterHashtable @{ LogName = 'Application'; ProviderName = 'MSSQL*'; StartTime = (Get-Date).AddSeconds(-30) } -ErrorAction SilentlyContinue |
        Select-Object TimeCreated, LevelDisplayName, Message
}
