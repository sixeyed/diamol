param(
    [string]$Filter=$null,
    [switch]$Images=$true,
    [switch]$Chapters=$false
)


$collections = @(
    'images',
    'chapters'
)

$oses = @(
    'linux',
    'windows'
)

foreach ($collection in $collections) {
    foreach ($os in $oses) {
        $compose="compose-$collection"
        $composeFile="${compose}.yml"
        $osFile="${compose}-${os}.yml"
        $tagsFile="${compose}-tags.yml"

        $composeFiles = @(
            '-f', $composeFile,
            '-f', $osFile,
            '-f', $tagsFile
        )

        $listFile = "image-lists/$collection-$os.json"
        $services = docker compose $composeFiles config --services | ConvertTo-Json        
        $services > $listFile
    }
}