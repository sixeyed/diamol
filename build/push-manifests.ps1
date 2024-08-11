param(
    [switch]$Images=$true,
    [switch]$Chapters=$false
)

try {
    $collection='images'
    if ($Chapters) {
        $collection='chapters'
    }
    $composeFile="compose-${collection}.yml"

    $imageList=$(yq e '.services.[].image' $composeFile)
    foreach ($image in $imageList)
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