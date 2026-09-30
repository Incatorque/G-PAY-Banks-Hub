# EF Core scaffold + ABP table ignore helpers

$script:DefaultAbpIgnorePrefixes = @(
    'Abp',
    'OpenIddict',
    'Saas',
    'Gdpr',
    'App'
)

# Still scaffold/review these even when IgnoreAbpTables is on
$script:AbpTableIgnoreExceptions = @()

$script:AlwaysIgnoreTables = @(
    '__EFMigrationsHistory'
)

$script:SkipScaffoldClasses = @(
    'User',           # replaced by AppUser / Identity
    'AppUser',        # hand-maintained IdentityUser
    'AbpUsers',       # ABP Identity table — do not scaffold
    'AbpUser'         # EF singularized name — do not scaffold as its own type
)

$script:ClassRenameMap = @{
    # DB table "Entity" → CustomEntity (ABP already owns Entity<T>)
    'Entity'    = 'CustomEntity'
    'Menu'      = 'Menu1'
    'Menus'     = 'Menu1'
    'AbpUser'   = 'AppUser'
    'AbpUsers'  = 'AppUser'
}

function Test-IsAbpSystemTable {
    param(
        [string]$TableName,
        [string[]]$Prefixes = $script:DefaultAbpIgnorePrefixes
    )
    if ([string]::IsNullOrWhiteSpace($TableName)) { return $false }
    if ($script:AlwaysIgnoreTables -contains $TableName) { return $true }

    foreach ($ex in @($script:AbpTableIgnoreExceptions)) {
        if ($TableName.Equals($ex, [System.StringComparison]::OrdinalIgnoreCase)) {
            return $false
        }
    }

    foreach ($p in $Prefixes) {
        if ($TableName.StartsWith($p, [System.StringComparison]::OrdinalIgnoreCase)) {
            return $true
        }
    }
    return $false
}

function New-GPaySqlConnection {
    param([string]$ConnectionString)
    # Prefer Microsoft.Data.SqlClient when available (PS 7+), else System.Data.SqlClient
    try {
        return [Microsoft.Data.SqlClient.SqlConnection]::new($ConnectionString)
    } catch {
        try {
            Add-Type -AssemblyName System.Data
            return New-Object System.Data.SqlClient.SqlConnection $ConnectionString
        } catch {
            throw "Unable to create SQL connection. Install Microsoft.Data.SqlClient or use Windows PowerShell with System.Data.SqlClient. $($_.Exception.Message)"
        }
    }
}

function Get-SqlServerTableList {
    param(
        [Parameter(Mandatory)][string]$ConnectionString,
        [switch]$IgnoreAbpTables
    )
    Write-GPayLog 'VERBOSE' 'Querying SQL Server for table/view list...'
    $tables = @()
    $conn = New-GPaySqlConnection -ConnectionString $ConnectionString
    try {
        $conn.Open()
        $cmd = $conn.CreateCommand()
        $cmd.CommandText = @"
SELECT TABLE_SCHEMA, TABLE_NAME, TABLE_TYPE
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_TYPE IN ('BASE TABLE','VIEW')
ORDER BY TABLE_TYPE, TABLE_SCHEMA, TABLE_NAME
"@
        $reader = $cmd.ExecuteReader()
        while ($reader.Read()) {
            $name = [string]$reader['TABLE_NAME']
            $schema = [string]$reader['TABLE_SCHEMA']
            $type = [string]$reader['TABLE_TYPE']
            $isAbp = Test-IsAbpSystemTable -TableName $name
            if ($IgnoreAbpTables -and $isAbp) { continue }
            $tables += [pscustomobject]@{
                Schema   = $schema
                Name     = $name
                FullName = if ($schema -eq 'dbo') { $name } else { "$schema.$name" }
                Type     = $type
                IsAbp    = $isAbp
            }
        }
        $reader.Close()
    } finally {
        if ($conn.State -ne 'Closed') { $conn.Close() }
        $conn.Dispose()
    }
    Write-GPayLog 'INFO' "Discovered $($tables.Count) table(s)/view(s) (IgnoreAbp=$IgnoreAbpTables)"
    return $tables
}

function Invoke-GPayEfScaffold {
    param(
        [Parameter(Mandatory)]$Paths,
        [Parameter(Mandatory)][string]$ConnectionString,
        [Parameter(Mandatory)][string]$OutputDir,
        [Parameter(Mandatory)][string]$ContextDir,
        [string]$ContextName = 'ScaffoldTempDbContext',
        [string[]]$Tables,
        [switch]$IgnoreAbpTables
    )

    # Resolve to absolute paths (EF accepts these; avoids relative-path confusion)
    $outAbs = if ([IO.Path]::IsPathRooted($OutputDir)) { $OutputDir } else { Join-Path (Split-Path $Paths.EfProject -Parent) $OutputDir }
    $ctxAbs = if ([IO.Path]::IsPathRooted($ContextDir)) { $ContextDir } else { Join-Path (Split-Path $Paths.EfProject -Parent) $ContextDir }
    New-Item -ItemType Directory -Force -Path $outAbs, $ctxAbs | Out-Null
    Get-ChildItem -LiteralPath $outAbs -Filter '*.cs' -ErrorAction SilentlyContinue | Remove-Item -Force
    Get-ChildItem -LiteralPath $ctxAbs -Filter '*.cs' -ErrorAction SilentlyContinue | Remove-Item -Force

    # NOTE: do NOT use $args — it is a PowerShell automatic variable and breaks & dotnet @args
    $efArgs = [System.Collections.Generic.List[string]]::new()
    $efArgs.AddRange([string[]]@(
        'ef', 'dbcontext', 'scaffold',
        $ConnectionString,
        'Microsoft.EntityFrameworkCore.SqlServer',
        '--project', $Paths.EfProject,
        '--startup-project', $Paths.HostProject,
        '--output-dir', $outAbs,
        '--context-dir', $ctxAbs,
        '--context', $ContextName,
        '--namespace', 'GPay.Banking.BankHub',
        '--context-namespace', 'GPay.Banking.EntityFrameworkCore',
        '--data-annotations',
        '--force',
        '--no-onconfiguring'
    ))

    # Optional explicit --table list (avoid when large — CLI length limits)
    $tableList = @($Tables | Where-Object { $_ })
    if ($tableList.Count -gt 0) {
        $approxLen = ($tableList | Measure-Object -Property Length -Sum).Sum + ($tableList.Count * 10)
        if ($approxLen -lt 5000) {
            Write-GPayLog 'INFO' "Scaffolding $($tableList.Count) table(s) with --table filters..."
            foreach ($t in $tableList) {
                if ($IgnoreAbpTables -and (Test-IsAbpSystemTable -TableName $t)) { continue }
                $efArgs.Add('--table')
                $efArgs.Add($t)
            }
        } else {
            Write-GPayLog 'WARN' "Too many tables for --table filters ($($tableList.Count)). Scaffolding full database."
        }
    } else {
        Write-GPayLog 'INFO' 'Scaffolding database (full schema). ABP tables removed afterward if enabled.'
    }

    Write-GPayLog 'VERBOSE' ("dotnet ef … ($($efArgs.Count) args); connection masked: " + (Mask-ConnectionString $ConnectionString))

    $prevEap = $ErrorActionPreference
    $ErrorActionPreference = 'Continue'
    try {
        $output = & dotnet @($efArgs.ToArray()) 2>&1
        foreach ($line in $output) {
            Write-GPayLog 'VERBOSE' ([string]$line)
        }
        if ($LASTEXITCODE -ne 0) {
            throw "dotnet ef dbcontext scaffold failed with exit code $LASTEXITCODE"
        }
    } finally {
        $ErrorActionPreference = $prevEap
    }

    if ($IgnoreAbpTables) {
        $removed = 0
        foreach ($f in Get-ChildItem -LiteralPath $outAbs -Filter '*.cs' -File -ErrorAction SilentlyContinue) {
            $raw = Get-Content -LiteralPath $f.FullName -Raw -Encoding UTF8
            $tableName = [IO.Path]::GetFileNameWithoutExtension($f.Name)
            if ($raw -match '\[Table\("([^"]+)"\)\]') { $tableName = $Matches[1] }
            if (Test-IsAbpSystemTable -TableName $tableName) {
                Remove-Item -LiteralPath $f.FullName -Force
                $removed++
            }
        }
        if ($removed -gt 0) {
            Write-GPayLog 'INFO' "Removed $removed scaffolded ABP/system model file(s)"
        }
    }

    $modelFiles = @(Get-ChildItem -LiteralPath $outAbs -Filter '*.cs' -File)
    $ctxFile = Get-ChildItem -LiteralPath $ctxAbs -Filter "$ContextName.cs" -File -ErrorAction SilentlyContinue
    Write-GPayLog 'OK' "Scaffold complete: $($modelFiles.Count) model file(s)"
    return [pscustomobject]@{
        ModelFiles   = $modelFiles
        ContextFile  = $ctxFile
        OutputDir    = $outAbs
        ContextDir   = $ctxAbs
    }
}
