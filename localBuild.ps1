Write-Output "Installing WYAM..."
dotnet tool install -g Wyam.Tool

$confirmation = Read-Host "Build preview?:"
if ($confirmation -eq 'y') {
    Write-Output "Building site preview..."
    wyam
    wyam preview
}else{
    Write-Output "Building site..."
    wyam
}