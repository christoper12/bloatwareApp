Import-Module "$PSScriptRoot\Logger.psm1"

$logInfo = Get-LogInfo

function Get-InstalledApps {
    param (
        [switch]$AsJson,
        [string]$OutputPath
    )

    $apps = @()

    # --------------------------------------------------
    # 1. Registry apps (EXE / MSI)
    # --------------------------------------------------
    $paths = @(
        "HKLM:\Software\Microsoft\Windows\CurrentVersion\Uninstall\*",
        "HKLM:\Software\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\*",
        "HKCU:\Software\Microsoft\Windows\CurrentVersion\Uninstall\*"
    )

    # $paths = @(
    #     "HKLM:\Software\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\*"
    # )

    # $paths = @(
    #     "HKCU:\Software\Microsoft\Windows\CurrentVersion\Uninstall\*"
    # )

    # foreach ($path in $paths) {
    #     Get-ChildItem $path -ErrorAction SilentlyContinue | ForEach-Object {
    #         try {
    #             $props = Get-ItemProperty $_.PSPath `
    #                 -Name DisplayName, DisplayVersion, Publisher, UninstallString `
    #                 -ErrorAction Stop

    #             if ($props.DisplayName) {
    #                 $apps += [PSCustomObject]@{
    #                     Name            = $props.DisplayName
    #                     Version         = $props.DisplayVersion
    #                     Publisher       = $props.Publisher
    #                     UninstallString = $props.UninstallString
    #                     Source          = "Registry"
    #                 }
    #             }
    #         }
    #         catch {
    #             return
    #         }
    #     }
    # }

    $programsAndFeatures = foreach ($path in $paths) {
        Get-ItemProperty $path -ErrorAction SilentlyContinue |
        Where-Object {
            $_.DisplayName -and
            $_.SystemComponent -ne 1
        } |
        Select-Object DisplayName, DisplayVersion, Publisher, InstallDate, UninstallString
    }

    foreach ($program in $programsAndFeatures) {
        # Hindari duplikat nama
        if ($apps.Name -contains $program.DisplayName) { continue }

        $apps += [PSCustomObject]@{
            Name            = $program.DisplayName
            Version         = $program.DisplayVersion
            Publisher       = $program.Publisher
            UninstallString = $program.UninstallString
            Source          = "Registry"
        }
    }

    Write-Log "Registry apps scanned: $($apps.Count)"
    Write-Log "Log directory: $($logInfo.LogDir)"

    # # --------------------------------------------------
    # # 2. Microsoft Store / AppX apps (Calculator, Calendar, dll)
    # # --------------------------------------------------
    # try {
    #     Get-AppxPackage | ForEach-Object {

    #         # Hindari duplikat nama
    #         if ($apps.Name -contains $_.Name) { return }

    #         $apps += [PSCustomObject]@{
    #             Name            = $_.Name
    #             Version         = $_.Version.ToString()
    #             Publisher       = $_.Publisher
    #             UninstallString = $_.PackageFullName
    #             Source          = "AppX"
    #         }
    #     }

    #     Write-Log "AppX apps scanned"
    # }
    # catch {
    #     Write-Log "AppX scan failed: $_" "WARN"
    # }

    # Write-Log "Total apps collected: $($apps.Count)"

    # --------------------------------------------------
    # 3. JSON output
    # --------------------------------------------------
    if ($AsJson) {
        $json = $apps | ConvertTo-Json -Depth 3

        if ($OutputPath) {
            $json | Set-Content -Path $OutputPath -Encoding UTF8
            Write-Log "Exported installed apps to JSON"
        }

        return $json
    }

    return $apps
}

Export-ModuleMember -Function Get-InstalledApps
