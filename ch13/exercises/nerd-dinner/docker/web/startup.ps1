

$key = 'ENV_DB_CONNECTION_STRING'
$configFilePath = '/inetpub/wwwroot/config/connectionStrings.config'
Write-Host "Updating config file: $configFilePath"
(Get-Content $configFilePath).Replace($key, [Environment]::GetEnvironmentVariable($key)) | Set-Content $configFilePath

$appSettingsKeys = @(
    'ENV_IPINFODB_KEY'
    'ENV_BINGMAPS_KEY'
)
$configFilePath = '/inetpub/wwwroot/config/appSettings.config'
$content = Get-Content $configFilePath
foreach ($key in $appSettingsKeys) {
    $content = $content.Replace($key, [Environment]::GetEnvironmentVariable($key))
}
Write-Host "Updating config file: $configFilePath"
$content | Set-Content $configFilePath

Write-Host "Starting ServiceMonitor for W3SVC"
& /ServiceMonitor.exe w3svc