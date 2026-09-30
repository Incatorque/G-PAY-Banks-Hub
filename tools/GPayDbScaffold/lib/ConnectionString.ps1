# Resolve connection strings from User Secrets or appsettings*.json

function Get-GPayProjectPaths {
    param([string]$RepoRoot)
    if (-not $RepoRoot) {
        $RepoRoot = (Resolve-Path (Join-Path $PSScriptRoot '..\..\..')).Path
    }
    [pscustomobject]@{
        RepoRoot          = $RepoRoot
        DomainProject     = Join-Path $RepoRoot 'src\GPay.Banking.Domain\GPay.Banking.Domain.csproj'
        EfProject         = Join-Path $RepoRoot 'src\GPay.Banking.EntityFrameworkCore\GPay.Banking.EntityFrameworkCore.csproj'
        HostProject       = Join-Path $RepoRoot 'src\GPay.Banking.HttpApi.Host\GPay.Banking.HttpApi.Host.csproj'
        HostDir           = Join-Path $RepoRoot 'src\GPay.Banking.HttpApi.Host'
        ModelsDir         = Join-Path $RepoRoot 'src\GPay.Banking.Domain\BankHub'
        DbContextPath     = Join-Path $RepoRoot 'src\GPay.Banking.EntityFrameworkCore\EntityFrameworkCore\BankingDbContext.cs'
        UserSecretsId     = '1dfc7671-0456-42f6-a87c-85cb1515e89b'
    }
}

function Get-GPayUserSecretsPath {
    param([string]$UserSecretsId)
    Join-Path $env:APPDATA "Microsoft\UserSecrets\$UserSecretsId\secrets.json"
}

function Read-JsonFileSafe {
    param([string]$Path)
    if (-not (Test-Path -LiteralPath $Path)) { return $null }
    try {
        return Get-Content -LiteralPath $Path -Raw -Encoding UTF8 | ConvertFrom-Json
    } catch {
        Write-GPayLog 'WARN' "Failed to parse JSON: $Path — $($_.Exception.Message)"
        return $null
    }
}

function Get-ConnectionStringsFromObject {
    param($Json)
    $result = [ordered]@{}
    if (-not $Json) { return $result }

    $props = $Json.PSObject.Properties
    foreach ($p in $props) {
        if ($p.Name -match '^ConnectionStrings:(.+)$') {
            $result[$Matches[1]] = [string]$p.Value
        }
    }

    if ($Json.ConnectionStrings) {
        foreach ($p in $Json.ConnectionStrings.PSObject.Properties) {
            $result[$p.Name] = [string]$p.Value
        }
    }
    return $result
}

function Get-GPayConnectionStringCandidates {
    param($Paths)

    $candidates = @()

    $secretsPath = Get-GPayUserSecretsPath -UserSecretsId $Paths.UserSecretsId
    $secretsJson = Read-JsonFileSafe -Path $secretsPath
    $fromSecrets = Get-ConnectionStringsFromObject -Json $secretsJson
    foreach ($key in $fromSecrets.Keys) {
        $val = $fromSecrets[$key]
        if ([string]::IsNullOrWhiteSpace($val)) { continue }
        $candidates += [pscustomobject]@{
            Source   = 'UserSecrets'
            Name     = $key
            Value    = $val
            FilePath = $secretsPath
            Display  = "User Secrets → $key"
        }
    }

    $appsettingsFiles = @(
        (Join-Path $Paths.HostDir 'appsettings.json'),
        (Join-Path $Paths.HostDir 'appsettings.Development.json'),
        (Join-Path $Paths.HostDir 'appsettings.secrets.json')
    ) | Where-Object { Test-Path -LiteralPath $_ }

    foreach ($file in $appsettingsFiles) {
        $json = Read-JsonFileSafe -Path $file
        $fromApp = Get-ConnectionStringsFromObject -Json $json
        foreach ($key in $fromApp.Keys) {
            $val = $fromApp[$key]
            if ([string]::IsNullOrWhiteSpace($val)) { continue }
            $candidates += [pscustomobject]@{
                Source   = 'AppSettings'
                Name     = $key
                Value    = $val
                FilePath = $file
                Display  = "$(Split-Path $file -Leaf) → $key"
            }
        }
    }

    return $candidates
}

function Select-GPayConnectionString {
    param($Paths)

    while ($true) {
        Write-Host ''
        Write-Host 'Connection string source:' -ForegroundColor White
        Write-Host '  [1] User Secrets'
        Write-Host '  [2] App settings'
        Write-Host '  [3] Paste'
        $choice = Read-Host 'Select (1-3)'

        if ($choice -eq '3') {
            $manual = Read-Host 'Connection string'
            if ([string]::IsNullOrWhiteSpace($manual)) {
                Write-GPayLog 'WARN' 'Empty — try again.'
                continue
            }
            return [pscustomobject]@{
                Source = 'Manual'; Name = 'Custom'; Value = $manual.Trim()
                FilePath = $null; Display = 'Manual paste'
            }
        }

        $preferredSource = switch ($choice) {
            '1' { 'UserSecrets' }
            '2' { 'AppSettings' }
            default {
                Write-GPayLog 'WARN' 'Enter 1, 2, or 3.'
                $null
            }
        }
        if (-not $preferredSource) { continue }

        $filtered = @(Get-GPayConnectionStringCandidates -Paths $Paths |
            Where-Object { $_.Source -eq $preferredSource })

        if ($filtered.Count -eq 0) {
            Write-Host "None found in $preferredSource. Pick another source." -ForegroundColor Yellow
            continue
        }

        if ($filtered.Count -eq 1) {
            Write-GPayLog 'OK' "Using: $($filtered[0].Display)"
            Write-Host ("  " + (Mask-ConnectionString $filtered[0].Value)) -ForegroundColor DarkGray
            return $filtered[0]
        }

        Write-Host ''
        Write-Host 'Pick a connection string:' -ForegroundColor White
        for ($i = 0; $i -lt $filtered.Count; $i++) {
            Write-Host ("  [{0}] {1}" -f ($i + 1), $filtered[$i].Display)
            Write-Host ("      {0}" -f (Mask-ConnectionString $filtered[$i].Value)) -ForegroundColor DarkGray
        }
        $pick = Read-Host 'Number'
        if ($pick -match '^\d+$') {
            $idx = [int]$pick - 1
            if ($idx -ge 0 -and $idx -lt $filtered.Count) {
                Write-GPayLog 'OK' "Using: $($filtered[$idx].Display)"
                return $filtered[$idx]
            }
        }
        Write-GPayLog 'WARN' 'Invalid number — try again.'
    }
}

function Mask-ConnectionString {
    param(
        [string]$Value,
        [switch]$Full
    )
    if ([string]::IsNullOrWhiteSpace($Value)) { return '(empty)' }

    $masked = $Value
    $masked = [regex]::Replace($masked, '(?i)(Password|Pwd)\s*=\s*[^;]*', '$1=****')
    $masked = [regex]::Replace($masked, '(?i)(User ID|UID)\s*=\s*[^;]*', '$1=***')
    $masked = [regex]::Replace($masked, '(?i)(AccountKey|Account Key)\s*=\s*[^;]*', '$1=****')
    $masked = [regex]::Replace($masked, '(?i)(SharedAccessSignature|Shared Access Signature)\s*=\s*[^;]*', '$1=****')
    $masked = [regex]::Replace($masked, '(?i)(ClientSecret|Client Secret)\s*=\s*[^;]*', '$1=****')
    $masked = [regex]::Replace($masked, '(?i)(AccessToken|Access Token)\s*=\s*[^;]*', '$1=****')
    $masked = [regex]::Replace($masked, '(?i)(ApiKey|Api Key)\s*=\s*[^;]*', '$1=****')

    if (-not $Full -and $masked.Length -gt 160) {
        $masked = $masked.Substring(0, 157) + '...'
    }
    return $masked
}
