param(
    [string]$Filter=$null,
    [switch]$Images=$true,
    [switch]$Chapters=$false
)

try {
    $info = docker version -f json | ConvertFrom-Json
    $env:DOCKER_BUILD_OS = $info.Server.Os.ToLower()
    $env:DOCKER_BUILD_CPU = $info.Server.Arch.ToLower()

    $env:OS_VERSION_TAG=''
    if ($env:DOCKER_BUILD_OS -eq 'windows') {
        # TODO - determine
        $env:OS_VERSION_TAG='-ltsc2022'
    }

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
        build --pull $Filter

    docker compose `
        -f $composeFile `
        -f $osFile `
        -f $tagsFile `
        push $Filter
}

finally {
    popd
}