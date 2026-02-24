param (
    [string[]]$SelectedApps
)

$SelectedSet = @()

if ($SelectedApps) {
    $SelectedSet = $SelectedApps -split '\|' | ForEach-Object { $_.Trim().ToLower() }
}

$BaseDir = $PSScriptRoot
$LogsDir    = Join-Path (Split-Path $BaseDir -Parent) "logs"
$ModulesDir = Join-Path $BaseDir "Modules"
$ConfigDir    = Join-Path (Split-Path $BaseDir -Parent) "config"
$configPath = Join-Path $ConfigDir "esential-apps.json"
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
        Write-Log "Installer not found: $Path"
        throw "Installer not found: $Path"
    }

    $extension = [System.IO.Path]::GetExtension($Path).ToLower()

    Write-Host "Installing $Name..."
    Write-Log "Installing $Name from path: $Path"

    # ============================
    # CLICKONCE INSTALLER
    # ============================
    if ($extension -eq ".application") {

        Write-Log "$Name detected as ClickOnce application"

        # ClickOnce MUST be launched via shell
        Start-Process -FilePath $Path

        Write-Host "$Name ClickOnce installer launched."
        Write-Log "$Name ClickOnce installer launched successfully."

        return
    }

    # ============================
    # MSI INSTALLER
    # ============================
    if ($extension -eq ".msi") {

        Write-Log "$Name detected as MSI installer"

        $msiArgs = "/i `"$Path`" $Args"

        Write-Log "Executing: msiexec.exe $msiArgs"

        $process = Start-Process `
            -FilePath "msiexec.exe" `
            -ArgumentList $msiArgs `
            -Wait `
            -PassThru
    }
    else {

        # ============================
        # EXE INSTALLER
        # ============================
        Write-Log "$Name detected as EXE installer"


        if ([string]::IsNullOrWhiteSpace($Args)) {
            Write-Log "Executing: $Path"

            $process = Start-Process `
                -FilePath $Path `
                -Wait `
                -PassThru
        }
        else {
            Write-Log "Executing: $Path $Args"

            $process = Start-Process `
                -FilePath $Path `
                -ArgumentList $Args `
                -Wait `
                -PassThru
        }
    }

    if ($process.ExitCode -ne 0) {
        Write-Log "Install $Name failed. ExitCode: $($process.ExitCode)"
        throw "Install $Name failed. ExitCode: $($process.ExitCode)"
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

    $installerExt = [System.IO.Path]::GetExtension($app.installerPath).ToLower()

    Write-Host "Processing $($app.name)..."
    Write-Log "Processing $($app.name) (installer: $installerExt)"

    # ============================
    # CLICKONCE APPLICATION
    # ============================
    if ($installerExt -eq ".application") {

        Write-Host "$($app.name) is ClickOnce application"
        Write-Log "$($app.name) detected as ClickOnce application"

        # ClickOnce handles install / update itself
        Install-App $app.installerPath $null $app.name
        continue
    }

    # ============================
    # STANDARD INSTALLER (EXE / MSI)
    # ============================
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