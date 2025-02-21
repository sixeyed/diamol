param(
    [string]$Filter=$null,
    [switch]$Images=$true,
    [switch]$Chapters=$false,
    [switch]$Delete=$true,
    [switch]$Pull=$false
)

$ErrorActionPreference = 'Continue'

if ($env:BUILD_IMAGES) {
    $Images = [bool]::Parse($env:BUILD_IMAGES)
}
if ($env:BUILD_CHAPTERS) {
    $Chapters = [bool]::Parse($env:BUILD_CHAPTERS)
}
if ($env:FILTER) {
    $Filter = $env:FILTER
}

try {
        
    echo '------------------'
    echo 'Build info'
    echo '------------------'
    echo "Images = $Images"
    echo "Chapters = $Chapters"
    echo "Filter = $Filter"
    echo '------------------'

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
    
    $variants = @(
        "linux-arm64",
        "linux-amd64",
        "windows-ltsc2019-amd64",
        "windows-ltsc2022-amd64",
        "windows-ltsc2025-amd64"
    )

    $manifestMediaType='application/vnd.docker.distribution.manifest.v2+json'
    
    foreach ($image in $imageList)
    {   
        echo "* Processing image: $image"
        $variantList = @()
        foreach ($variant in $variants) {
            $ref = "$($image)-$variant"
            $manifest = docker manifest inspect $ref | ConvertFrom-Json
            if ($null -ne $manifest -and $manifest.mediaType -eq $manifestMediaType) {
                $variantList += $ref
                echo "** Image variant found. Will add to manifest list: $ref"
                docker buildx imagetools inspect $ref
            }
            else {
                echo "** Image variant NOT found. Skipping: $ref"
            }
        }

        if ($Delete) {
            docker manifest rm $image
        }

        docker manifest create --amend $image @variantList
        docker manifest push $image

        if ($Pull) {
            docker pull $image
        }
    }
}

finally {
    popd
}