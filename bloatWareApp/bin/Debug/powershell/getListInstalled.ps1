# ================================
# Bootstrap Allied App Manager
# ================================

# Resolve base directories
$BaseDir      = $PSScriptRoot
$ModulesDir   = Join-Path $BaseDir "Modules"
$ConfigDir    = Join-Path (Split-Path $BaseDir -Parent) "config"
$LogsDir      = Join-Path (Split-Path $BaseDir -Parent) "logs"

# Ensure logs folder exists
if (-not (Test-Path $LogsDir)) {
    New-Item -ItemType Directory -Path $LogsDir | Out-Null
}

# Import modules (EXPLICIT PATH)
Import-Module (Join-Path $ModulesDir "Logger.psm1")        -Force
Import-Module (Join-Path $ModulesDir "AppScanner.psm1")    -Force

$logInfo = Get-LogInfo

Write-Log "=== Power Shell App Manager START ==="
Write-Log "Log directory: $($logInfo.LogDir)"

    # Scan installed apps
    $saveIntallAppJson = Join-Path $ConfigDir "installedApp.json"
    Get-InstalledApps -AsJson -OutputPath "$saveIntallAppJson"

Write-Log "=== Power Shell App Manager END ==="
