pushd "${PSScriptRoot}/../images"

try {
    $info = docker version -f json | ConvertFrom-Json
    $env:DOCKER_BUILD_OS = $info.Server.Os.ToLower()
    $env:DOCKER_BUILD_CPU = $info.Server.Arch.ToLower()

    docker compose `
        -f docker-compose-build-linux.yml `
        -f docker-compose-build-linux-tags.yml `
        build --pull

    docker compose `
        -f docker-compose-build-linux.yml `
        -f docker-compose-build-linux-tags.yml `
        push
    
    docker compose `
        -f docker-compose-build-linux.yml `
        build
}

finally {
    popd
}