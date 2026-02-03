function Get-WingetIndex {

    $index = @{}

    if (-not (Get-Command winget -ErrorAction SilentlyContinue)) {
        Write-Log "Winget not found" "WARN"
        return $index
    }

    try {
        $json = winget list --accept-source-agreements --output json 2>$null |
                ConvertFrom-Json
    }
    catch {
        Write-Log "Winget JSON parse failed" "WARN"
        return $index
    }

    foreach ($pkg in $json) {
        if ($pkg.Name -and $pkg.Id) {
            if (-not $index.ContainsKey($pkg.Name)) {
                $index[$pkg.Name] = $pkg.Id
            }
        }
    }

    Write-Log "Winget index loaded: $($index.Count)"

    return $index
}

Export-ModuleMember -Function Get-WingetIndex
