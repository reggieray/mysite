Write-Output "Building site with Statiq Web..."

$confirmation = Read-Host "Build preview? (y/n)"
if ($confirmation -eq 'y') {
    Write-Output "Building site with preview server..."
    dotnet run -- preview
} else {
    Write-Output "Building site..."
    dotnet run
}