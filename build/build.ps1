param(
    [string]$Filter=$null,
    [switch]$Images=$true,
    [switch]$Chapters=$false
)

$ErrorActionPreference = 'Continue'

try {
    $info = docker version -f json | ConvertFrom-Json
    $env:DOCKER_BUILD_OS = $info.Server.Os.ToLower()
    $env:DOCKER_BUILD_CPU = $info.Server.Arch.ToLower()

    $env:OS_VERSION_TAG=''
    if ($env:DOCKER_BUILD_OS -eq 'windows') {
        $env:WINDOWS_VERSION='ltsc2019'
        $env:WINDOWS_VERSION_CODE='1809'
        $winver=(Get-Item "HKLM:SOFTWARE\Microsoft\Windows NT\CurrentVersion").GetValue('DisplayVersion')
        echo "** winver: $winver **"
        # yuck - https://www.gaijin.at/en/infos/windows-version-numbers
        $version=[System.Environment]::OSVersion.Version.ToString()
        echo "** version: $version **"
        if ($version -eq '10.0.22631.0' -or $version -eq '10.0.20348.0') {
            $env:WINDOWS_VERSION = $env:WINDOWS_VERSION_CODE = 'ltsc2022'
        } elseif ($version -eq '10.0.26100.0') {
            $env:WINDOWS_VERSION = $env:WINDOWS_VERSION_CODE ='ltsc2025'
        }
        $env:OS_VERSION_TAG="-$env:WINDOWS_VERSION"
    }

    echo '------------------'
    echo 'OS info'
    echo '------------------'
    echo "DOCKER_BUILD_OS = $env:DOCKER_BUILD_OS"
    echo "DOCKER_BUILD_CPU = $env:DOCKER_BUILD_CPU"
    echo "WINDOWS_VERSION = $env:WINDOWS_VERSION"
    echo "WINDOWS_VERSION_CODE = $env:WINDOWS_VERSION_CODE"
    echo "OS_VERSION_TAG = $env:OS_VERSION_TAG"
    echo '------------------'

    $collection='images'
    if ($Chapters) {
        $collection='chapters'
    }
    $compose="compose-$collection"
    $composeFile="${compose}.yml"
    $osFile="${compose}-$($env:DOCKER_BUILD_OS).yml"
    $tagsFile="${compose}-tags.yml"

    # Windows dependency
    if ($env:DOCKER_BUILD_OS -eq 'windows') {
        docker compose -f $composeFile -f $osFile -f $tagsFile build --pull git-windows
        docker compose -f $composeFile -f $osFile -f $tagsFile push git-windows
    }

    docker compose `
        -f $composeFile `
        -f $osFile `
        -f $tagsFile `
        build --pull #$Filter

    docker compose `
        -f $composeFile `
        -f $osFile `
        -f $tagsFile `
        push #$Filter
}

finally {
    popd
}