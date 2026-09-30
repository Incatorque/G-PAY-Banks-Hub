# Apply approved model + DbContext changes

function Test-GPayJsonBool {
    param($Value)
    if ($null -eq $Value) { return $false }
    if ($Value -is [bool]) { return [bool]$Value }
    $s = [string]$Value
    return ($s -eq '1' -or $s -eq 'True' -or $s -eq 'true')
}

function Apply-GPaySingleTable {
    param(
        [Parameter(Mandatory)]$Paths,
        [Parameter(Mandatory)]$Diff,
        [Parameter(Mandatory)][string]$ScaffoldContextPath,
        [Parameter(Mandatory)][string]$BackupDir,
        [string[]]$ExcludeColumns = @()
    )

    if ($Diff.ChangeType -in @('Skipped','MissingInDb','Unchanged')) {
        return [pscustomobject]@{
            Ok = $true; Skipped = $true; ClassName = $Diff.ClassName
            TableName = $Diff.TableName; Message = "Skipped ($($Diff.ChangeType))"
        }
    }

    if (-not $Diff.TransformedPath -or -not (Test-Path -LiteralPath $Diff.TransformedPath)) {
        return [pscustomobject]@{
            Ok = $false; Skipped = $true; ClassName = $Diff.ClassName
            TableName = $Diff.TableName; Message = 'Missing transformed model file'
        }
    }

    New-Item -ItemType Directory -Force -Path $BackupDir | Out-Null

    $content = Get-Content -LiteralPath $Diff.TransformedPath -Raw -Encoding UTF8
    foreach ($col in @($ExcludeColumns | Where-Object { $_ })) {
        Write-GPayLog 'VERBOSE' "Excluding column/property $col on $($Diff.ClassName)"
        $content = Remove-ModelProperty -Content $content -PropertyName $col
    }

    $targetPath = Join-Path $Paths.ModelsDir "$($Diff.ClassName).cs"
    if (Test-Path -LiteralPath $targetPath) {
        Copy-Item -LiteralPath $targetPath -Destination (Join-Path $BackupDir "$($Diff.ClassName).cs") -Force
    }
    Set-Content -LiteralPath $targetPath -Value $content -Encoding UTF8
    Write-GPayLog 'OK' "Wrote model: $targetPath"

    # AppUser / AbpUsers is configured via ABP Identity — do not rewrite fluent blocks
    if ($Diff.ClassName -ne 'AppUser' -and $Diff.TableName -ne 'AbpUsers') {
        Update-GPayDbContextEntity `
            -Paths $Paths `
            -ClassName $Diff.ClassName `
            -ScaffoldClass $Diff.ScaffoldClass `
            -ScaffoldContextPath $ScaffoldContextPath `
            -BackupDir $BackupDir `
            -ExcludeColumns $ExcludeColumns `
            -IsNew:($Diff.ChangeType -eq 'Added') `
            -IsView:([bool]$Diff.IsView)
    } else {
        Write-GPayLog 'INFO' 'AppUser (AbpUsers) — model only; DbContext Identity config left unchanged'
    }

    return [pscustomobject]@{
        Ok = $true; Skipped = $false; ClassName = $Diff.ClassName
        TableName = $Diff.TableName; Message = 'Applied'; Path = $targetPath
    }
}

function Find-GPayDiff {
    param($Diffs, [string]$TableName, [string]$ClassName)
    foreach ($d in $Diffs) {
        if ($ClassName -and $d.ClassName -eq $ClassName) { return $d }
        if ($TableName -and $d.TableName -eq $TableName) { return $d }
    }
    return $null
}

function Apply-GPayScaffoldSelection {
    param(
        [Parameter(Mandatory)]$Paths,
        [Parameter(Mandatory)]$Diffs,
        [Parameter(Mandatory)]$Selection,
        [Parameter(Mandatory)][string]$ScaffoldContextPath,
        [Parameter(Mandatory)][string]$WorkDir
    )

    $backupDir = Join-Path $WorkDir ("backup-" + (Get-Date -Format 'yyyyMMdd-HHmmss'))
    New-Item -ItemType Directory -Force -Path $backupDir | Out-Null
    Write-GPayLog 'INFO' "Backup directory: $backupDir (gitignored — will not be committed)"

    $rawTables = @($Selection.tables)
    $applied = [System.Collections.Generic.List[string]]::new()
    $skipped = [System.Collections.Generic.List[string]]::new()

    foreach ($t in $rawTables) {
        if (-not (Test-GPayJsonBool $t.included)) { continue }
        $diff = Find-GPayDiff -Diffs $Diffs -TableName $t.tableName -ClassName $t.className
        if (-not $diff) {
            $skipped.Add("$($t.tableName) (not in diff)") | Out-Null
            continue
        }
        $excludeCols = @()
        if ($t.columns) {
            $excludeCols = @($t.columns | Where-Object { -not (Test-GPayJsonBool $_.included) } | ForEach-Object { $_.name })
        }
        $result = Apply-GPaySingleTable `
            -Paths $Paths `
            -Diff $diff `
            -ScaffoldContextPath $ScaffoldContextPath `
            -BackupDir $backupDir `
            -ExcludeColumns $excludeCols
        if ($result.Ok -and -not $result.Skipped) { $applied.Add($result.ClassName) | Out-Null }
        else { $skipped.Add("$($result.TableName) ($($result.Message))") | Out-Null }
    }

    Write-GPayLog 'INFO' "Apply finished — $($applied.Count) written, $($skipped.Count) skipped"
    return [pscustomobject]@{
        Applied = @($applied)
        Skipped = @($skipped)
        BackupDir = $backupDir
    }
}

function Update-GPayDbContextEntity {
    param(
        $Paths,
        [string]$ClassName,
        [string]$ScaffoldClass,
        [string]$ScaffoldContextPath,
        [string]$BackupDir,
        [string[]]$ExcludeColumns = @(),
        [switch]$IsNew,
        [switch]$IsView
    )

    if (-not (Test-Path -LiteralPath $ScaffoldContextPath)) {
        Write-GPayLog 'WARN' "Scaffold context not found: $ScaffoldContextPath"
        return
    }

    $dbPath = $Paths.DbContextPath
    if (-not (Test-Path -LiteralPath (Join-Path $BackupDir 'BankingDbContext.cs'))) {
        Copy-Item -LiteralPath $dbPath -Destination (Join-Path $BackupDir 'BankingDbContext.cs') -Force
    }

    $dbText = Get-Content -LiteralPath $dbPath -Raw -Encoding UTF8
    $scaffoldText = Get-Content -LiteralPath $ScaffoldContextPath -Raw -Encoding UTF8

    $block = Extract-EntityConfigBlock -ContextText $scaffoldText -ClassName $ScaffoldClass
    if (-not $block) {
        # try resolved name
        $block = Extract-EntityConfigBlock -ContextText $scaffoldText -ClassName $ClassName
    }
    if (-not $block) {
        Write-GPayLog 'WARN' "No fluent config block found in scaffold context for $ScaffoldClass"
        # Still ensure DbSet exists for new entities
        if ($IsNew) {
            $dbText = Ensure-DbSetProperty -DbContextText $dbText -ClassName $ClassName
            Set-Content -LiteralPath $dbPath -Value $dbText -Encoding UTF8
        }
        return
    }

    $block = Transform-FluentBlockForAbp `
        -Block $block `
        -ClassName $ClassName `
        -ScaffoldClass $ScaffoldClass `
        -ExcludeColumns $ExcludeColumns `
        -IsView:($IsView)

    # Replace or insert in GPayDbContext
    if ($dbText -match "builder\.Entity<$([regex]::Escape($ClassName))>\s*\(") {
        $dbText = Replace-EntityConfigBlock -DbContextText $dbText -ClassName $ClassName -NewBlock $block
        Write-GPayLog 'VERBOSE' "Replaced OnModelCreating block for $ClassName"
    } else {
        $dbText = Insert-EntityConfigBlock -DbContextText $dbText -NewBlock $block
        Write-GPayLog 'VERBOSE' "Inserted OnModelCreating block for $ClassName"
    }

    $dbText = Ensure-DbSetProperty -DbContextText $dbText -ClassName $ClassName
    Set-Content -LiteralPath $dbPath -Value $dbText -Encoding UTF8
    Write-GPayLog 'OK' "Updated BankingDbContext for $ClassName"
}

function Extract-EntityConfigBlock {
    param([string]$ContextText, [string]$ClassName)

    $pattern = "builder\.Entity<$([regex]::Escape($ClassName))>\s*\(\s*(entity|b)\s*=>"
    $m = [regex]::Match($ContextText, $pattern)
    if (-not $m.Success) { return $null }

    $start = $m.Index
    # Find matching closing ');' for the Entity<>(...) call
    $i = $m.Index + $m.Length
    $depth = 1 # already inside => { or (
    # Actually pattern ends at => so we need to find the opening { or (
    # EF scaffold uses: builder.Entity<X>(entity => { ... });
    $braceStart = $ContextText.IndexOf('{', $m.Index)
    if ($braceStart -lt 0) { return $null }
    $depth = 0
    for ($i = $braceStart; $i -lt $ContextText.Length; $i++) {
        $ch = $ContextText[$i]
        if ($ch -eq '{') { $depth++ }
        elseif ($ch -eq '}') {
            $depth--
            if ($depth -eq 0) {
                # include trailing );
                $end = $i + 1
                while ($end -lt $ContextText.Length -and $ContextText[$end] -match '\s') { $end++ }
                if ($end -lt $ContextText.Length -and $ContextText[$end] -eq ')') {
                    $end++
                    if ($end -lt $ContextText.Length -and $ContextText[$end] -eq ';') { $end++ }
                }
                return $ContextText.Substring($start, $end - $start)
            }
        }
    }
    return $null
}

function Transform-FluentBlockForAbp {
    param(
        [string]$Block,
        [string]$ClassName,
        [string]$ScaffoldClass,
        [string[]]$ExcludeColumns,
        [switch]$IsView
    )
    $b = $Block
    if ($ScaffoldClass -ne $ClassName) {
        $b = $b -replace "builder\.Entity<$([regex]::Escape($ScaffoldClass))>", "builder.Entity<$ClassName>"
    }

    $isViewBlock = $IsView -or ($b -match '\.ToView\(') -or ($b -match '\.HasNoKey\(')

    # Tables only: PK renames e.PkAccountId -> e.Id
    if (-not $isViewBlock) {
        $b = [regex]::Replace($b, '\.HasKey\(\s*e\s*=>\s*e\.(Pk\w+)\s*\)', '.HasKey(e => e.Id)')
        $b = [regex]::Replace($b, 'entity\.Property\(\s*e\s*=>\s*e\.(Pk\w+)\s*\)', 'entity.Property(e => e.Id)')
        $b = [regex]::Replace($b, '\bProperty\(\s*e\s*=>\s*e\.(Pk\w+)\s*\)', 'Property(e => e.Id)')
    }

    foreach ($col in $ExcludeColumns) {
        # Remove fluent lines referencing the property
        $b = [regex]::Replace($b, "(?m)^\s*entity\.Property\(e\s*=>\s*e\.$([regex]::Escape($col))\).*;\s*\r?\n", '')
        $b = [regex]::Replace($b, "(?m)^\s*entity\.HasOne\(d\s*=>\s*d\.$([regex]::Escape($col))\).*?\r?\n(?:\s*\..*?\r?\n)*", '')
    }

    # CustomEntity table mapping
    if ($ClassName -eq 'CustomEntity' -and $b -notmatch 'ToTable\("Entity"') {
        $b = $b -replace '(builder\.Entity<CustomEntity>\(entity\s*=>\s*\{)', "`$1`r`n            entity.ToTable(`"Entity`", tb => tb.UseSqlOutputClause(false));"
    }

    # AbpUser nav types → AppUser (Identity)
    $b = Convert-AbpUserTypeRefsToAppUser -Content $b

    return $b
}

function Replace-EntityConfigBlock {
    param([string]$DbContextText, [string]$ClassName, [string]$NewBlock)
    $existing = Extract-EntityConfigBlock -ContextText $DbContextText -ClassName $ClassName
    if (-not $existing) { return $DbContextText }
    return $DbContextText.Replace($existing, $NewBlock)
}

function Insert-EntityConfigBlock {
    param([string]$DbContextText, [string]$NewBlock)
    # Insert before the final closing braces of OnModelCreating
    $marker = [regex]::Match($DbContextText, '(?ms)protected override void OnModelCreating\(ModelBuilder builder\).*')
    if (-not $marker.Success) {
        Write-GPayLog 'ERROR' 'Could not locate OnModelCreating to insert entity block.'
        return $DbContextText
    }
    # Find last occurrence of "        });" before OnModelCreating's closing
    $idx = $DbContextText.LastIndexOf("`r`n    }`r`n}")
    if ($idx -lt 0) { $idx = $DbContextText.LastIndexOf("`n    }`n}") }
    if ($idx -lt 0) {
        return $DbContextText + "`r`n`r`n        " + $NewBlock + "`r`n"
    }
    return $DbContextText.Insert($idx, "`r`n        " + $NewBlock.Trim() + "`r`n")
}

function Ensure-DbSetProperty {
    param([string]$DbContextText, [string]$ClassName)

    if ($DbContextText -match "DbSet<$([regex]::Escape($ClassName))>") {
        return $DbContextText
    }

    # Pluralize simply
    $propName = if ($ClassName -eq 'CustomEntity') { 'Entities' }
        elseif ($ClassName.EndsWith('y') -and $ClassName.Length -gt 1 -and $ClassName[-2] -match '[^aeiou]') {
            $ClassName.Substring(0, $ClassName.Length - 1) + 'ies'
        }
        elseif ($ClassName.EndsWith('s') -or $ClassName.EndsWith('x') -or $ClassName.EndsWith('ch')) { $ClassName + 'es' }
        else { $ClassName + 's' }

    $dbSetLine = "    public virtual DbSet<$ClassName> $propName { get; set; }`r`n"
    # Insert after last DbSet line in the models region — find first BankHub or Books area end; append near other virtual DbSets
    $lastDbSet = [regex]::Matches($DbContextText, '(?m)^    public virtual DbSet<[^>]+>\s+\w+\s*\{\s*get;\s*set;\s*\}\s*$')
    if ($lastDbSet.Count -gt 0) {
        $m = $lastDbSet[$lastDbSet.Count - 1]
        $insertAt = $m.Index + $m.Length
        return $DbContextText.Insert($insertAt, "`r`n`r`n" + $dbSetLine.TrimEnd() )
    }
    return $DbContextText
}
