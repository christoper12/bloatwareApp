param (
    [string[]]$SelectedApps
)

$SelectedSet = @()

if ($SelectedApps) {
    $SelectedSet = $SelectedApps -split '\|' | ForEach-Object { $_.Trim().ToLower() }
}

$BaseDir = $PSScriptRoot

$LogsDir    = Join-Path (Split-Path $BaseDir -Parent) "logs"
$ModulesDir = Join-Path $BaseDir "modules"
$configPath = Join-Path $PSScriptRoot "apps.json"
$config = Get-Content $configPath -Raw | ConvertFrom-Json

# Ensure logs folder exists
if (-not (Test-Path $LogsDir)) {
    New-Item -ItemType Directory -Path $LogsDir | Out-Null
}

# Import modules (EXPLICIT PATH)
Import-Module (Join-Path $ModulesDir "Logger.psm1")        -Force

$logInfo = Get-LogInfo



Write-Log "=== Allied App Manager START ==="
Write-Log "Log directory: $($logInfo.LogDir)"

if (-not (Test-Path $configPath)) {
    throw "Config file not found: $configPath"
    Write-Log "Config file not found: $configPath"
}

try {
    $config = Get-Content $configPath -Raw | ConvertFrom-Json
}
catch {
    throw "apps.json is invalid (corrupt JSON)."
    Write-Log "apps.json is invalid (corrupt JSON)."
}

if (-not $config.applications) {
    throw "apps.json does not have 'applications' node"
    Write-Log "apps.json does not have 'applications' node"
}

function Test-AppInstalled {
    param (
        [Parameter(Mandatory)]
        [string]$DetectName
    )

    $paths = @(
        "HKLM:\Software\Microsoft\Windows\CurrentVersion\Uninstall",
        "HKLM:\Software\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall"
    )

    foreach ($basePath in $paths) {
        if (-not (Test-Path $basePath)) { continue }

        foreach ($key in Get-ChildItem $basePath -ErrorAction SilentlyContinue) {
            try {
                $displayName = Get-ItemPropertyValue `
                    -Path $key.PSPath `
                    -Name "DisplayName" `
                    -ErrorAction Stop

                if ($displayName -like "*$DetectName*") {
                    return $true
                }
            }
            catch {
                continue
            }
        }
    }

    return $false
}

function Install-App {
    param (
        [Parameter(Mandatory)] [string]$Path,
        [string]$Args,
        [Parameter(Mandatory)] [string]$Name
    )

    if (-not (Test-Path $Path)) {
        throw "Installer not found: $Path"
        Write-Log "Installer not found: $Path"
    }

    Write-Host "Installing $Name..."
    Write-Log "Installing $Name from path: $Path"

    $process = Start-Process `
        -FilePath $Path `
        -ArgumentList $Args `
        -Wait `
        -PassThru `
        -NoNewWindow

    if ($process.ExitCode -ne 0) {
        throw "Install $Name failed. ExitCode: $($process.ExitCode)"
        Write-Log "Install $Name failed. ExitCode: $($process.ExitCode)"
    }

    Write-Host "$Name Successfully installed."
    Write-Log "$Name Successfully installed."
}

foreach ($app in $config.applications) {

    Write-Log "Selected Apps from UI: $($SelectedSet -join ', ')"

    if ($SelectedSet -notcontains $app.name.ToLower()) {
        Write-Host "Skipping $($app.name) (not selected)"
        Write-Log "Skipping $($app.name) (not selected)"
        continue
    }

    Write-Host "Checking $($app.name)..."

    if (Test-AppInstalled $app.detectName) {
        Write-Host "$($app.name) Already installed."
        Write-Log "$($app.name) Already installed."
    }
    else {
        Write-Host "$($app.name) Not installed."
        Write-Log "$($app.name) Not installed."
        Install-App $app.installerPath $app.silentArgs $app.name
    }
}

Write-Log "=== Allied App Manager END ==="