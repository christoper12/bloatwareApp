Import-Module "$PSScriptRoot\Logger.psm1"

function Install-App {
    param(
        [string]$AppId,
        [string]$InstallerPath
    )

    if (Get-Command winget -ErrorAction SilentlyContinue) {
        Write-Log "Installing $AppId via winget"
        winget install --id $AppId -e --silent --accept-package-agreements --accept-source-agreements
    }
    elseif (Test-Path $InstallerPath) {
        Write-Log "Installing via installer $InstallerPath"
        Start-Process -FilePath $InstallerPath -ArgumentList "/quiet /norestart" -Wait
    }
    else {
        Write-Log "Installer not found for $AppId" "ERROR"
    }
}

Export-ModuleMember -Function Install-App