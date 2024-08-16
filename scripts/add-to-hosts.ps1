param (
	[string] $domain,
	[string] $ip='127.0.0.1'
)

if ($ip -eq 'localhost') {
	$ip='127.0.0.1'
}

$hostsFile='C:/windows/system32/drivers/etc/hosts'
if ($IsLinux -or $IsMacOS) {
	$hostsFile='/etc/hosts'
}

Add-Content -Value "$ip  $domain" -Path $hostsFile