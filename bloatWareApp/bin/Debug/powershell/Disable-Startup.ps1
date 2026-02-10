param (
    [Parameter(Mandatory)]
    [ValidateSet("Registry","Startup Folder","Scheduled Task")]
    [string]$Source,

    [Parameter(Mandatory)]
    [string]$Name,

    [string]$Location   # Current User / All Users / AtLogon
)

# ===============================
# Resolve StartupApproved Path
# ===============================

function Get-ApprovedPath {
    param ($Source, $Location)

    switch ($Source) {

        "Registry" {
            if ($Location -eq "All Users") {
                return "HKLM:\Software\Microsoft\Windows\CurrentVersion\Explorer\StartupApproved\Run"
            } else {
                return "HKCU:\Software\Microsoft\Windows\CurrentVersion\Explorer\StartupApproved\Run"
            }
        }

        "Startup Folder" {
            if ($Location -eq "All Users") {
                return "HKLM:\Software\Microsoft\Windows\CurrentVersion\Explorer\StartupApproved\StartupFolder"
            } else {
                return "HKCU:\Software\Microsoft\Windows\CurrentVersion\Explorer\StartupApproved\StartupFolder"
            }
        }

        "Scheduled Task" {
            return "HKCU:\Software\Microsoft\Windows\CurrentVersion\Explorer\StartupApproved\ScheduledTasks"
        }
    }
}

$approvedPath = Get-ApprovedPath $Source $Location

if (-not (Test-Path $approvedPath)) {
    New-Item -Path $approvedPath -Force | Out-Null
}

# ===============================
# DISABLE
# ===============================

Set-ItemProperty `
    -Path $approvedPath `
    -Name $Name `
    -Value ([byte[]](0x03,0,0,0,0,0,0,0)) `
    -Type Binary `
    -Force
