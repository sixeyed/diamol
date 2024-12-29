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
        if ($winver -eq '23H2') {
            $env:WINDOWS_VERSION = $env:WINDOWS_VERSION_CODE = 'ltsc2022'
        } elseif ($winver -eq '24H2') {
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