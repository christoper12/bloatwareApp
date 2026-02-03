param (
    [string[]]$SelectedApps
)

$SelectedSet = @()
if ($SelectedApps) {
    $SelectedSet = $SelectedApps | ForEach-Object { $_.Trim().ToLower() }
}
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
Import-Module (Join-Path $ModulesDir "AppUninstaller.psm1")-Force

$logInfo = Get-LogInfo

function Test-PendingReboot {
    $paths = @(
        "HKLM:\SOFTWARE\Microsoft\Windows\CurrentVersion\Component Based Servicing\RebootPending",
        "HKLM:\SOFTWARE\Microsoft\Windows\CurrentVersion\WindowsUpdate\Auto Update\RebootRequired",
        "HKLM:\SYSTEM\CurrentControlSet\Control\Session Manager\PendingFileRenameOperations"
    )

    foreach ($path in $paths) {
        if (Test-Path $path) {
            return $true
        }
    }
    return $false
}

Write-Log "=== Allied App Manager START ==="
Write-Log "Log directory: $($logInfo.LogDir)"

# Global execution mode flag
$global:RebootPending = $false

if (Test-PendingReboot) {
    Write-Log "System reboot is pending. Continuing execution in degraded mode." "WARN"
    $global:RebootPending = $true
}

# ------------------------------------------------
# Load bloatware config
# ------------------------------------------------
$bloatConfig = Join-Path $ConfigDir "bloatware-apps.json"

if (-not (Test-Path $bloatConfig)) {
    Write-Log "Bloatware config not found: $bloatConfig" "ERROR"
    Write-Log "=== Allied App Manager END ==="
    exit 1
}

$bloatware = Get-Content $bloatConfig | ConvertFrom-Json

# ------------------------------------------------
# Uninstall bloatware apps
# ------------------------------------------------
foreach ($bloat in $bloatware) {

    Write-Log "Processing bloatware: $bloat"

    if ($SelectedSet -contains $bloat.ToLower()) {
        try {
            Write-Log "Selected for uninstall: $bloat"
            Remove-App -AppName $bloat
        }
        catch {
            Write-Log "Fatal error during uninstall: $_" "ERROR"
        }
    }
    else {
        Write-Log "Skipping $bloat (not selected)"
    }

    # Re-check reboot state AFTER each uninstall
    if (-not $global:RebootPending -and (Test-PendingReboot)) {
        Write-Log "Reboot became pending after uninstalling $bloat" "WARN"
        $global:RebootPending = $true
    }
}

# ------------------------------------------------
# Execution end
# ------------------------------------------------
if ($global:RebootPending) {
    Write-Log "Execution completed with pending reboot. System cleanup will finalize after restart." "WARN"
}
else {
    Write-Log "Execution completed without pending reboot."
}

Write-Log "=== Allied App Manager END ==="