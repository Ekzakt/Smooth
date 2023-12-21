param([string]$ProjectDir, [string]$ProjectPath);

$regexPattern = "<Version>(\d+\.\d+\.\d+)</Version>"
$originalContent = Get-Content $ProjectPath

$versionValue = ($originalContent -match $regexPattern)

$isDebug = Test-Path Variable:PSDebugContext

Write-Host "isDebug = " $isDebug

if ($versionValue[0] -match "\d+.\d+.\d+")
{
    $versionComponents = $Matches[0] -split '\.'

    $major = $versionComponents[0]
    $minor = $versionComponents[1]
    $revision = [int]$versionComponents[2] + 1

    $newVersion = $major + "." + $minor + "." + $revision

    Write-Host "New version set to " + $newVersion

    $newVersion = "<Version>" + $newVersion + "</Version>"

    $updatedContent = $originalContent -replace $regexPattern, $newVersion

    Set-Content -Path $ProjectPath -Value $updatedContent
}