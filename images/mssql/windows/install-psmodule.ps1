Install-PackageProvider -Name NuGet -MinimumVersion 2.8.5.201 -Force
Set-PSRepository -Name PSGallery -InstallationPolicy Trusted
Install-Module -Name SqlServer -AllowClobber -Force -MinimumVersion 22.3.0
Import-Module sqlserver