function Remove-App {
    param(
        [Parameter(Mandatory)]
        [string]$AppName
    )

    Write-Log "Attempting to uninstall (ALL USERS best-effort): $AppName"

    # ==================================================
    # 1. APPX — TRUE ALL USERS
    # ==================================================
    try {
        $appx = Get-AppxPackage -AllUsers |
            Where-Object {
                $_.Name -like "*$AppName*" -or
                $_.PackageFamilyName -like "*$AppName*"
            }

        if ($appx) {
            foreach ($pkg in $appx) {
                Write-Log "Removing AppX (AllUsers): $($pkg.PackageFullName)"
                Remove-AppxPackage -Package $pkg.PackageFullName -AllUsers -ErrorAction Stop
            }
            return
        }
    }
    catch {
        Write-Log "AppX uninstall failed: $_" "WARN"
    }

    # ==================================================
    # 2. WINGET — MACHINE-WIDE (ADMIN REQUIRED)
    # ==================================================
    if (Get-Command winget -ErrorAction SilentlyContinue) {

        Write-Log "Trying winget uninstall (machine-wide best effort)"

        $null = winget uninstall `
            --name "$AppName" `
            --exact `
            --silent `
            --force `
            --accept-source-agreements `
            --disable-interactivity 2>&1

        if ($LASTEXITCODE -eq 0) {
            Write-Log "Winget uninstall succeeded (machine-wide or user-scope)"
            return
        }
        else {
            Write-Log "Winget uninstall failed or app not machine-wide" "WARN"
        }
    }

    # ==================================================
    # 3. REGISTRY — HKLM (TRUE ALL USERS)
    # ==================================================
    $hklmRoots = @(
        "HKLM:\Software\Microsoft\Windows\CurrentVersion\Uninstall",
        "HKLM:\Software\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall"
    )

    foreach ($root in $hklmRoots) {
        if (-not (Test-Path $root)) { continue }

        Get-ChildItem $root -ErrorAction SilentlyContinue | ForEach-Object {
            try {
                $dn = $_.GetValue("DisplayName")
                $us = $_.GetValue("UninstallString")

                if ($dn -and $us -and $dn -like "*$AppName*") {

                    Write-Log "Registry uninstall (HKLM / AllUsers): $dn"

                    Start-Process "cmd.exe" `
                        -ArgumentList "/c $us /quiet /norestart" `
                        -Wait `
                        -NoNewWindow

                    return
                }
            }
            catch {
                Write-Log "Invalid registry entry skipped" "WARN"
            }
        }
    }

    # ==================================================
    # 4. REGISTRY — HKCU (CURRENT USER ONLY, LAST RESORT)
    # ==================================================
    $hkcuRoot = "HKCU:\Software\Microsoft\Windows\CurrentVersion\Uninstall"

    if (Test-Path $hkcuRoot) {
        Get-ChildItem $hkcuRoot -ErrorAction SilentlyContinue | ForEach-Object {
            try {
                $dn = $_.GetValue("DisplayName")
                $us = $_.GetValue("UninstallString")

                if ($dn -and $us -and $dn -like "*$AppName*") {

                    Write-Log "Registry uninstall (HKCU / current user): $dn"

                    Start-Process "cmd.exe" `
                        -ArgumentList "/c $us /quiet /norestart" `
                        -Wait `
                        -NoNewWindow

                    return
                }
            }
            catch {}
        }
    }

    Write-Log "Unable to uninstall $AppName using any ALL-USER compatible method" "WARN"
}

Export-ModuleMember -Function Remove-App
