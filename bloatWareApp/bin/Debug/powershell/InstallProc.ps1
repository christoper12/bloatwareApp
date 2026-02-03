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
Import-Module (Join-Path $ModulesDir "AppInstaller.psm1")  -Force

Write-Log "=== Allied App Manager START ==="

# ------------------------------------------------
# Install required apps (ONLY if safe)
# ------------------------------------------------

# Scan installed apps
$installedApps = Get-InstalledApps

# Load required apps config
$requiredConfig = Join-Path $ConfigDir "required-apps.json"
$requiredApps = Get-Content $requiredConfig | ConvertFrom-Json

foreach ($app in $requiredApps) {
    if (-not ($installedApps.Name -contains $app.name)) {
        Install-App -AppId $app.wingetId -InstallerPath $app.installerPath
    }
}

Write-Log "=== Allied App Manager END ==="
