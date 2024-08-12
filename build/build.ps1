param(
    [string]$Filter=$null,
    [switch]$Images=$true,
    [switch]$Chapters=$false
)

try {
    $info = docker version -f json | ConvertFrom-Json
    $env:DOCKER_BUILD_OS = $info.Server.Os.ToLower()
    $env:DOCKER_BUILD_CPU = $info.Server.Arch.ToLower()

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
    
    docker compose `
        -f $composeFile `
        -f $osFile `
        build #$Filter
}

finally {
    popd
}