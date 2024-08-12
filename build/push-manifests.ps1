param(
    [string]$Filter=$null,
    [switch]$Images=$true,
    [switch]$Chapters=$false
)

try {
    $collection='images'
    if ($Chapters) {
        $collection='chapters'
    }
    $composeFile="compose-${collection}.yml"

    $allImages=$(yq e '.services.[].image' $composeFile)
    $imageList = $allImages
    if ($Filter) {
        $imageList = $allImages | where {$_.Contains($Filter)}
    }
    
    foreach ($image in $imageList)
    {    
        # TODO - add other OS & archs
        docker manifest create --amend $image `
            "$($image)-linux-arm64"`
            "$($image)-linux-amd64"
        
        docker manifest push $image
        docker pull $image
    }
}

finally {
    popd
}