pushd "${PSScriptRoot}/../images"

try {
    $images=$(yq e '.services.[].image' docker-compose-build-linux.yml)
    foreach ($image in $images)
    {    
        # TODO - add other OS & archs
        docker manifest create --amend $image `
            "$($image)-linux-arm64"
        
        docker manifest push $image
    }
}

finally {
    popd
}