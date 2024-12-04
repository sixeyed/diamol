Write-Host "`n** Downloading SQL Server:`n$env:MSSQL_DOWNLOAD_URL"
Invoke-WebRequest -Uri $env:MSSQL_DOWNLOAD_URL -OutFile 'sqlexpress.exe'

Write-Host "`n** Extracting SQL Server distribution"
Start-Process -FilePath 'sqlexpress.exe' -Wait -PassThru -ArgumentList '/qs', '/x:setup'

# https://learn.microsoft.com/en-us/sql/database-engine/install-windows/install-sql-server-from-the-command-prompt?view=sql-server-2017
Write-Host "`n** Installing SQL Server; instance: $env:MSSQL_INSTANCE_NAME; SA password length: $($env:MSSQL_SA_PASSWORD.Length)"
.\setup\setup.exe /QUIET /ACTION=Install `
                  /INSTANCENAME=$env:MSSQL_INSTANCE_NAME /FEATURES=SQLEngine `
                  /UpdateEnabled=0 `
                  /SQLSVCSTARTUPTYPE=Automatic /SQLSVCACCOUNT='NT AUTHORITY\System' `
                  /TCPENABLED=1 /NPENABLED=0 `
                  /SUPPRESSPRIVACYSTATEMENTNOTICE /IACCEPTSQLSERVERLICENSETERMS `
                  /SQLSYSADMINACCOUNTS="user manager\containeradministrator" "user manager\containeruser" "NT AUTHORITY\ANONYMOUS LOGON" `
                  /SECURITYMODE=SQL /SAPWD=$env:MSSQL_SA_PASSWORD

Write-Host "`n** Removing distribution files"
Remove-Item -Path 'sqlexpress.exe', 'setup' -Recurse -Force

$serviceName='MSSQL$' + $env:MSSQL_INSTANCE_NAME
Write-Host "`n** Stopping SQL Server service: $serviceName"
Stop-Service $serviceName

$regRootPath="HKLM:\SOFTWARE\Microsoft\Microsoft SQL Server\$($env:MSSQL_VERSION_CODE).$($env:MSSQL_INSTANCE_NAME)\MSSQLServer"
Write-Host "`n** Configuring SQL Server; reg path: $regRootPath"
Set-ItemProperty -Path "$regRootPath\SuperSocketNetLib\Tcp\IPAll" -Name 'TcpDynamicPorts' -Value ''
Set-ItemProperty -Path "$regRootPath\SuperSocketNetLib\Tcp\IPAll" -Name 'TcpPort' -Value 1433
Set-ItemProperty -Path $regRootPath -Name 'LoginMode' -Value 2

Write-Host "`n** Done."
