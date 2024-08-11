try {
    $info = docker version -f json | ConvertFrom-Json
    $env:DOCKER_BUILD_OS = $info.Server.Os.ToLower()
    $env:DOCKER_BUILD_CPU = $info.Server.Arch.ToLower()

    docker compose `
        -f compose-chapters-linux.yml `
        -f compose-chapters-linux-tags.yml `
        build --pull

    docker compose `
        -f compose-chapters-linux.yml `
        -f compose-chapters-linux-tags.yml `
        push
    
    docker compose `
        -f compose-chapters-linux.yml `
        build
}

finally {
    popd
}