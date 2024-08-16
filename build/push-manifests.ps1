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
        if (!$Filter.StartsWith('diamol')) {
            $Filter = "diamol/$Filter"
        }
        if (!$Filter.Contains(':')) {
            $Filter = "${Filter}:2e"
        }
        $imageList = $allImages | where {$_ -eq $Filter}
    }
    
    foreach ($image in $imageList)
    {   
        docker manifest rm $image

        # TODO - add other OS & archs
        docker manifest create --amend $image `
            "$($image)-linux-arm64" `
            "$($image)-linux-amd64" `
            "$($image)-windows-ltsc2019-amd64"
        
        docker manifest push $image
        docker pull $image
    }
}

finally {
    popd
}