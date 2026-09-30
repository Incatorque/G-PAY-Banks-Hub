<#
.SYNOPSIS
  G-PAY EF Core DB scaffold → ABP Entity models, interactive HTML review, apply to Domain + DbContext.

.DESCRIPTION
  1. Asks for connection string source (User Secrets / appsettings / paste).
  2. Scaffolds SQL Server tables (ABP system tables always ignored).
  3. Transforms models to GPay.Banking ABP format (Entity<T>, Id override, renames).
  4. Opens a G-PAY HTML review page (SQL delta + model diff) to include/exclude tables & columns.
  5. On OK, returns to PowerShell, applies changes verbosely, and prints OK when done.

.EXAMPLE
  pwsh -File .\tools\GPayDbScaffold\Scaffold-GPayDb.ps1
#>
[CmdletBinding()]
param(
    [string]$RepoRoot,
    [switch]$SkipBrowser,
    [int]$ReviewTimeoutMinutes = 120
)

$ErrorActionPreference = 'Stop'
$VerbosePreference = 'Continue'

$toolRoot = $PSScriptRoot
. (Join-Path $toolRoot 'lib\Logging.ps1')
. (Join-Path $toolRoot 'lib\ConnectionString.ps1')
. (Join-Path $toolRoot 'lib\Scaffold.ps1')
. (Join-Path $toolRoot 'lib\Transform.ps1')
. (Join-Path $toolRoot 'lib\Diff.ps1')
. (Join-Path $toolRoot 'lib\Apply.ps1')
. (Join-Path $toolRoot 'lib\ReviewServer.ps1')

if (-not $RepoRoot) {
    $RepoRoot = (Resolve-Path (Join-Path $toolRoot '..\..')).Path
}

$paths = Get-GPayProjectPaths -RepoRoot $RepoRoot
$workDir = Join-Path $toolRoot '.work'
New-Item -ItemType Directory -Force -Path $workDir | Out-Null
Initialize-GPayLog -WorkDir $workDir

Write-Host ''
Write-Host '========================================' -ForegroundColor Green
Write-Host '  G-PAY  ·  DB Scaffold → ABP Models' -ForegroundColor Green
Write-Host '========================================' -ForegroundColor Green
Write-GPayLog 'INFO' "Repo: $($paths.RepoRoot)"
Write-GPayLog 'INFO' "Models: $($paths.ModelsDir)"
Write-GPayLog 'INFO' "DbContext: $($paths.DbContextPath)"

foreach ($required in @($paths.DomainProject, $paths.EfProject, $paths.HostProject, $paths.ModelsDir, $paths.DbContextPath)) {
    if (-not (Test-Path -LiteralPath $required)) {
        throw "Required path not found: $required"
    }
}

# --- Connection string ---
$conn = Select-GPayConnectionString -Paths $paths
Write-GPayLog 'VERBOSE' ("Connection (masked): " + (Mask-ConnectionString $conn.Value))

# ABP / framework tables are always ignored (no prompt)
$ignoreAbp = $true
Write-GPayLog 'INFO' 'Ignoring ABP system tables (Abp*, OpenIddict*, Saas*, Gdpr*, App*) including AbpUsers'


# --- Scaffold ---
$runId = Get-Date -Format 'yyyyMMdd-HHmmss'
$scaffoldModelsAbs = Join-Path $workDir "scaffold-models-$runId"
$scaffoldCtxAbs    = Join-Path $workDir "scaffold-context-$runId"
$transformedDir    = Join-Path $workDir "transformed-$runId"

Write-GPayLog 'INFO' 'Starting EF Core dbcontext scaffold...'
$scaffoldResult = Invoke-GPayEfScaffold `
    -Paths $paths `
    -ConnectionString $conn.Value `
    -OutputDir $scaffoldModelsAbs `
    -ContextDir $scaffoldCtxAbs `
    -IgnoreAbpTables:$ignoreAbp

$scaffoldModelsAbs = $scaffoldResult.OutputDir
$scaffoldCtxAbs = $scaffoldResult.ContextDir

$ctxFile = $scaffoldResult.ContextFile
if (-not $ctxFile) {
    $ctxFile = Get-ChildItem -LiteralPath $scaffoldCtxAbs -Filter '*DbContext*.cs' -File -ErrorAction SilentlyContinue |
        Select-Object -First 1
}
if (-not $ctxFile) {
    throw "Scaffold context file not found under $scaffoldCtxAbs"
}
Write-GPayLog 'OK' "Scaffold context: $($ctxFile.FullName)"

# --- Transform + diff ---
Write-GPayLog 'INFO' 'Transforming models to ABP format and computing diffs...'

# Discover VIEW names so transforms stay keyless (no Entity<T> / Id override)
$viewNames = [System.Collections.Generic.HashSet[string]]::new([StringComparer]::OrdinalIgnoreCase)
try {
    $schemaObjects = Get-SqlServerTableList -ConnectionString $conn.Value -IgnoreAbpTables:$ignoreAbp
    foreach ($obj in $schemaObjects) {
        if ($obj.Type -eq 'VIEW') {
            [void]$viewNames.Add($obj.Name)
            if ($obj.FullName) { [void]$viewNames.Add($obj.FullName) }
        }
    }
    Write-GPayLog 'INFO' "View detection: $($viewNames.Count) SQL VIEW name(s)"
} catch {
    Write-GPayLog 'WARN' "Could not list views for transform hints: $($_.Exception.Message)"
}

$diffs = Build-GPayModelDiff `
    -Paths $paths `
    -ScaffoldModelsDir $scaffoldModelsAbs `
    -TransformedDir $transformedDir `
    -IgnoreAbpTables:$ignoreAbp `
    -ViewNames $viewNames `
    -ScaffoldContextPath $ctxFile.FullName

$manifestPath = Join-Path $workDir "manifest-$runId.json"
Export-GPayReviewManifest `
    -Diffs $diffs `
    -OutPath $manifestPath `
    -IgnoreAbpTables $ignoreAbp `
    -ConnectionDisplay $conn.Display `
    -ConnectionMasked (Mask-ConnectionString -Value $conn.Value -Full) `
    -UserName $env:USERNAME

# Console summary
Write-Host ''
Write-Host '--- Diff summary ---' -ForegroundColor White
$diffs | Group-Object ChangeType | Sort-Object Name | ForEach-Object {
    Write-Host ("  {0,-14} {1}" -f $_.Name, $_.Count)
}
Write-Host ''

# --- HTML review + chunked apply (one table per HTTP request) ---
$uiDir = Join-Path $toolRoot 'ui'
$decisionPath = Join-Path $workDir "decision-$runId.json"
$applyContext = @{
    Paths               = $paths
    Diffs               = $diffs
    ScaffoldContextPath = $ctxFile.FullName
    WorkDir             = $workDir
}

$server = $null
$result = $null
try {
    $server = Start-GPayReviewServer `
        -UiDir $uiDir `
        -ManifestPath $manifestPath `
        -DecisionPath $decisionPath `
        -ApplyContext $applyContext
    if ($SkipBrowser) {
        Write-GPayLog 'INFO' "SkipBrowser set — open manually: $($server.Url)"
    }
    $result = Wait-GPayReviewDecision -Server $server -TimeoutMinutes $ReviewTimeoutMinutes
} finally {
    Stop-GPayReviewServer -Server $server
}

if (-not $result -or $result.action -eq 'cancel') {
    Write-GPayLog 'WARN' 'User cancelled. No files were modified (or apply aborted).'
    Write-Host ''
    Write-Host 'Cancelled.' -ForegroundColor Yellow
    exit 1
}

Write-Host ''
Write-Host '--- Apply result ---' -ForegroundColor White
$applied = @($result.Applied)
$skipped = @($result.Skipped)
Write-GPayLog 'OK' ("Applied models ($($applied.Count)): " + ($(if ($applied.Count) { $applied -join ', ' } else { '(none)' })))
Write-GPayLog 'INFO' ("Skipped: " + ($(if ($skipped.Count) { $skipped.Count.ToString() + ' table(s)' } else { '(none)' })))
Write-GPayLog 'INFO' "Backup (not committed): $($result.BackupDir)"
Write-GPayLog 'INFO' "Full log: $($script:GPayLogPath)"

Write-Host ''
Write-Host '========================================' -ForegroundColor Green
Write-Host '  OK — scaffold apply complete' -ForegroundColor Green
Write-Host '  Return to Visual Studio to review.' -ForegroundColor Green
Write-Host '========================================' -ForegroundColor Green
Write-Host "Log: $($script:GPayLogPath)"
Write-Host "Backup: $($result.BackupDir)"
exit 0
