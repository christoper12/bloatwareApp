function Remove-App {
    param(
        [string]$AppName,
        [string]$WingetId
    )

    Write-Log "Attempting to uninstall $AppName"

    # 1. Winget by ID (MOST RELIABLE)
    if ($WingetId -and (Get-Command winget -ErrorAction SilentlyContinue)) {

        winget uninstall --id $WingetId `
            --silent `
            --accept-source-agreements `
            --disable-interactivity `
            --force

        if ($LASTEXITCODE -eq 0) {
            Write-Log "Uninstalled via winget ID: $WingetId"
            return
        }
    }

    Write-Log "Winget uninstall failed for $AppName, trying registry fallback" "WARN"

    # 2. Registry uninstall (SMART MATCH)
    $uninstallKeys = @(
        "HKLM:\Software\Microsoft\Windows\CurrentVersion\Uninstall\*",
        "HKLM:\Software\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\*"
    )

    foreach ($key in $uninstallKeys) {

        Get-ItemProperty $key -ErrorAction SilentlyContinue | Where-Object {
            $_.DisplayName -and
            $_.DisplayName.Replace("+","").Replace(" ","") -like "*$($AppName.Replace(" ",""))*"
        } | ForEach-Object {

            if ($_.UninstallString) {

                Write-Log "Executing uninstall for $($_.DisplayName)"

                $cmd = $_.UninstallString

                # MSI
                if ($cmd -match "MsiExec") {
                    $cmd += " /qn /norestart"
                }
                # EXE (NSIS / Inno)
                else {
                    $cmd += " /S"
                }

                Start-Process "cmd.exe" -ArgumentList "/c $cmd" -Wait
                Write-Log "Uninstall executed for $($_.DisplayName)"
                return
            }
        }
    }

    Write-Log "Unable to uninstall $AppName (no supported method found or completion pending reboot)" "WARN"
}
