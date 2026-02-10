# =====================================================
# Get-Startup.ps1
# Read startup items EXACTLY like Task Manager
# OUTPUT: JSON ONLY
# =====================================================

# ---------- HELPER ----------
function Get-StartupApprovedState {
    param (
        [string]$ApprovedPath,
        [string]$Name
    )

    if (-not (Test-Path $ApprovedPath)) {
        return $true
    }

    $item = Get-ItemProperty `
        -Path $ApprovedPath `
        -Name $Name `
        -ErrorAction SilentlyContinue

    if (-not $item) {
        return $true
    }

    return ($item.$Name[0] -eq 0x02)
}

# ---------- REGISTRY RUN ----------
function Get-StartupRegistry {
    param (
        [string]$RunPath,
        [string]$ApprovedPath,
        [string]$Scope
    )

    if (-not (Test-Path $RunPath)) { return }

    $props = Get-ItemProperty $RunPath

    foreach ($p in $props.PSObject.Properties) {

        if ($p.Name -in @(
            "PSPath","PSParentPath","PSChildName",
            "PSDrive","PSProvider"
        )) { continue }

        [PSCustomObject]@{
            Name              = $p.Name
            Command           = $p.Value
            Source            = "Registry"
            Location          = $Scope
            Enabled           = Get-StartupApprovedState $ApprovedPath $p.Name

            RegistryPath      = $RunPath
            RegistryValueName = $p.Name
            FilePath          = $null
            TaskName          = $null
            TaskPath          = $null
        }
    }
}

# ---------- STARTUP FOLDER ----------
function Get-StartupFolder {
    param (
        [string]$FolderPath,
        [string]$ApprovedPath,
        [string]$Scope
    )

    if (-not (Test-Path $FolderPath)) { return }

    Get-ChildItem $FolderPath -File | ForEach-Object {

        [PSCustomObject]@{
            Name              = $_.Name
            Command           = $_.FullName
            Source            = "Startup Folder"
            Location          = $Scope
            Enabled           = Get-StartupApprovedState $ApprovedPath $_.Name

            RegistryPath      = $null
            RegistryValueName = $null
            FilePath          = $_.FullName
            TaskName          = $null
            TaskPath          = $null
        }
    }
}

# ---------- SCHEDULED TASK ----------
function Get-StartupScheduledTask {

    $approvedPath = "HKCU:\Software\Microsoft\Windows\CurrentVersion\Explorer\StartupApproved\ScheduledTasks"

    Get-ScheduledTask | ForEach-Object {

        $task = $_

        foreach ($trigger in $task.Triggers) {

            if ($trigger.TriggerType -in @("AtLogon","AtStartup")) {

                $action = $task.Actions | Select-Object -First 1

                [PSCustomObject]@{
                    Name              = $task.TaskName
                    Command           = $action.Execute
                    Source            = "Scheduled Task"
                    Location          = $trigger.TriggerType
                    Enabled           = Get-StartupApprovedState $approvedPath $task.TaskName

                    RegistryPath      = $null
                    RegistryValueName = $null
                    FilePath          = $null
                    TaskName          = $task.TaskName
                    TaskPath          = $task.TaskPath
                }
            }
        }
    }
}

# =====================================================
# COLLECT ALL STARTUP ITEMS
# =====================================================

$result = @()

# Registry Run
$result += Get-StartupRegistry `
    "HKCU:\Software\Microsoft\Windows\CurrentVersion\Run" `
    "HKCU:\Software\Microsoft\Windows\CurrentVersion\Explorer\StartupApproved\Run" `
    "Current User"

$result += Get-StartupRegistry `
    "HKLM:\Software\Microsoft\Windows\CurrentVersion\Run" `
    "HKLM:\Software\Microsoft\Windows\CurrentVersion\Explorer\StartupApproved\Run" `
    "All Users"

# Startup Folder
$result += Get-StartupFolder `
    "$env:APPDATA\Microsoft\Windows\Start Menu\Programs\Startup" `
    "HKCU:\Software\Microsoft\Windows\CurrentVersion\Explorer\StartupApproved\StartupFolder" `
    "Current User"

$result += Get-StartupFolder `
    "C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Startup" `
    "HKLM:\Software\Microsoft\Windows\CurrentVersion\Explorer\StartupApproved\StartupFolder" `
    "All Users"

# Scheduled Tasks
$result += Get-StartupScheduledTask

# =====================================================
# OUTPUT JSON (FOR VB.NET)
# =====================================================

$result | ConvertTo-Json -Depth 4