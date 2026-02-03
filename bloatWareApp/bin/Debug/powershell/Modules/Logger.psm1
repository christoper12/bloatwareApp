# =========================
# Logger.psm1
# =========================

# Base directory script
$BaseDir = $PSScriptRoot

# Root logs folder (…/logs)
$LogsRoot = Join-Path (Split-Path $BaseDir -Parent) "logs"

# Unique session id
$SessionId = "{0}_{1}" -f `
    (Get-Date -Format "yyyy-MM-dd_HH-mm-ss"), `
    ([guid]::NewGuid().ToString("N").Substring(0,6))

# Session log directory
$LogsDir = Join-Path $LogsRoot $SessionId

if (-not (Test-Path $LogsDir)) {
    New-Item -ItemType Directory -Path $LogsDir | Out-Null
}

# Log file
$Global:LogFile = Join-Path $LogsDir "install.log"

function Write-Log {
    param(
        [Parameter(Mandatory)]
        [string]$Message,

        [ValidateSet("INFO","WARN","ERROR")]
        [string]$Level = "INFO"
    )

    $timestamp = Get-Date -Format "yyyy-MM-dd HH:mm:ss"
    $entry = "[{0}][{1}] {2}" -f $timestamp, $Level, $Message

    # Output to stdout (VB.NET bisa tangkap)
    Write-Output $entry

    # Append to file (safe karena file unik per session)
    Add-Content -Path $Global:LogFile -Value $entry
}

# Expose info to caller
function Get-LogInfo {
    [PSCustomObject]@{
        SessionId = $SessionId
        LogDir    = $LogsDir
        LogFile   = $Global:LogFile
    }
}

Export-ModuleMember -Function Write-Log, Get-LogInfo
# =========================