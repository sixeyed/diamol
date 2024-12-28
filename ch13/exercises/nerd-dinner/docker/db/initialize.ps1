param(
    [Parameter(Mandatory=$true)]
    [string] $sa_password,
    [string] $db_name='NerdDinner'
)

# start the service
$serviceName='MSSQL$' + $env:MSSQL_INSTANCE_NAME
Write-Output "Starting SQL Server service: $serviceName"
Start-Service $serviceName

$instance=".\$($env:MSSQL_INSTANCE_NAME)"
if ($sa_password) {
    Write-Host 'Changing SA login credentials'
    $sqlcmd = "ALTER LOGIN sa with password='$sa_password'; ALTER LOGIN sa ENABLE;"
    Invoke-SqlCmd -Query $sqlcmd -ServerInstance $instance  -TrustServerCertificate
}
else {
    Write-Host 'WARNING: SA password not supplied in $sa_password; using default'
}

Write-Host 'Started SQL Server.'

# attach data files if they exist: 
$mdfPath = "c:\data\${db_name}_Primary.mdf"
if ((Test-Path $mdfPath) -eq $true) {
    $sqlcmd = "IF DB_ID('${db_name}') IS NULL BEGIN CREATE DATABASE ${db_name} ON (FILENAME = N'$mdfPath')"
    $ldfPath = "c:\data\${db_name}_Primary.ldf"
    if ((Test-Path $mdfPath) -eq $true) {
        $sqlcmd =  "$sqlcmd, (FILENAME = N'$ldfPath')"
    }
    $sqlcmd = "$sqlcmd FOR ATTACH; END"
    Write-Verbose "Invoke-Sqlcmd -Query $($sqlcmd) -ServerInstance '.\SQLEXPRESS'"
    Invoke-Sqlcmd -Query $sqlcmd -ServerInstance $instance -TrustServerCertificate
}

# deploy or upgrade the database:
$SqlPackagePath = "$env:SQLPACKAGE_HOME\SqlPackage.exe"
& $SqlPackagePath  `
    /sf:${db_name}.dacpac `
    /a:Script /op:create.sql /p:CommentOutSetVarDeclarations=true `
    /tsn:${instance} /tdn:${db_name} /tu:sa /tp:$sa_password /ttsc:True

$SqlCmdVars = "DatabaseName=${db_name}", "DefaultFilePrefix=${db_name}", "DefaultDataPath=c:\data\", "DefaultLogPath=c:\data\"  
Invoke-Sqlcmd -InputFile create.sql -Variable $SqlCmdVars -ServerInstance $instance -TrustServerCertificate -Verbose

echo 'done' > /init.done
Write-Verbose "Started SQL Server."

while ($true) {
    Start-Sleep -Seconds 60

    Get-WinEvent -FilterHashtable @{ LogName = 'Application'; ProviderName = 'MSSQL*'; StartTime = (Get-Date).AddSeconds(-30) } -ErrorAction SilentlyContinue |
        Select-Object TimeCreated, LevelDisplayName, Message
}