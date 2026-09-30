# Diff scaffolded ABP models against existing Domain Models

function Build-GPayModelDiff {
    param(
        [Parameter(Mandatory)]$Paths,
        [Parameter(Mandatory)][string]$ScaffoldModelsDir,
        [Parameter(Mandatory)][string]$TransformedDir,
        [switch]$IgnoreAbpTables,
        [object]$ViewNames = $null,
        [string]$ScaffoldContextPath = $null
    )

    New-Item -ItemType Directory -Force -Path $TransformedDir | Out-Null
    $diffs = [System.Collections.Generic.List[object]]::new()

    $scaffoldContextText = $null
    if ($ScaffoldContextPath -and (Test-Path -LiteralPath $ScaffoldContextPath)) {
        $scaffoldContextText = Get-Content -LiteralPath $ScaffoldContextPath -Raw -Encoding UTF8
    }

    $scaffoldFiles = @(Get-ChildItem -LiteralPath $ScaffoldModelsDir -Filter '*.cs' -File |
        Where-Object { $_.Name -notmatch 'DbContext' })

    foreach ($file in $scaffoldFiles) {
        $raw = Get-Content -LiteralPath $file.FullName -Raw -Encoding UTF8
        if ($raw -notmatch 'public\s+partial\s+class\s+(\w+)') {
            Write-GPayLog 'WARN' "Skipping unparseable file: $($file.Name)"
            continue
        }
        $scaffoldClass = $Matches[1]
        $tableName = $scaffoldClass
        if ($raw -match '\[Table\("([^"]+)"\)\]') { $tableName = $Matches[1] }

        if ($IgnoreAbpTables -and (Test-IsAbpSystemTable -TableName $tableName)) {
            Write-GPayLog 'VERBOSE' "Ignoring ABP table model: $tableName"
            continue
        }

        $existingPath = Get-ExistingModelPath -ModelsDir $Paths.ModelsDir -ClassName $scaffoldClass
        $existingContent = if ($existingPath) { Get-Content -LiteralPath $existingPath -Raw -Encoding UTF8 } else { $null }

        $transformed = ConvertTo-AbpModel `
            -ScaffoldContent $raw `
            -ExistingContent $existingContent `
            -ViewNames $ViewNames `
            -ScaffoldContextText $scaffoldContextText
        if ($transformed.Skip) {
            $diffs.Add([pscustomobject]@{
                TableName      = $tableName
                ClassName      = $transformed.ClassName
                ScaffoldClass  = $scaffoldClass
                ChangeType     = 'Skipped'
                Included       = $false
                Reason         = $transformed.Reason
                ExistingPath   = $existingPath
                TransformedPath= $null
                SqlDelta       = "-- SKIPPED: $($transformed.Reason)"
                PropertyChanges= @()
                Summary        = $transformed.Reason
                IsView         = [bool]$transformed.IsView
            }) | Out-Null
            continue
        }

        $outPath = Join-Path $TransformedDir "$($transformed.ClassName).cs"
        Set-Content -LiteralPath $outPath -Value $transformed.Content -Encoding UTF8

        $oldProps = if ($existingContent) { @(Get-ModelPropertyDetails -Content $existingContent) } else { @() }
        $newProps = @(Get-ModelPropertyDetails -Content $transformed.Content)

        $oldMap = @{}
        foreach ($p in $oldProps) {
            $oldMap[$p.Name] = [pscustomobject]@{
                Name=$p.Name; Type=(Normalize-GPayModelType $p.Type); Column=$p.Column
                Attributes=$p.Attributes; IsNav=$p.IsNav
            }
        }
        $newMap = @{}
        foreach ($p in $newProps) {
            $newMap[$p.Name] = [pscustomobject]@{
                Name=$p.Name; Type=(Normalize-GPayModelType $p.Type); Column=$p.Column
                Attributes=$p.Attributes; IsNav=$p.IsNav
            }
        }

        $propChanges = [System.Collections.Generic.List[object]]::new()
        foreach ($name in $newMap.Keys) {
            if (-not $oldMap.ContainsKey($name)) {
                $propChanges.Add([pscustomobject]@{
                    Name=$name; Change='Added'; OldType=$null; NewType=$newMap[$name].Type
                    Column=$newMap[$name].Column; Included=$true; IsNav=[bool]$newMap[$name].IsNav
                }) | Out-Null
            } elseif ($oldMap[$name].Type -ne $newMap[$name].Type) {
                $propChanges.Add([pscustomobject]@{
                    Name=$name; Change='Modified'; OldType=$oldMap[$name].Type; NewType=$newMap[$name].Type
                    Column=$newMap[$name].Column; Included=$true; IsNav=[bool]$newMap[$name].IsNav
                }) | Out-Null
            }
            # Attribute-only differences are ignored for Entity→CustomEntity noise; still caught as Formatting via content normalize
        }
        foreach ($name in $oldMap.Keys) {
            if (-not $newMap.ContainsKey($name)) {
                $propChanges.Add([pscustomobject]@{
                    Name=$name; Change='Removed'; OldType=$oldMap[$name].Type; NewType=$null
                    Column=$oldMap[$name].Column; Included=$true; IsNav=[bool]$oldMap[$name].IsNav
                }) | Out-Null
            }
        }

        $changeType = if (-not $existingPath) { 'Added' }
            elseif ($propChanges.Count -eq 0 -and (Normalize-Code $existingContent) -eq (Normalize-Code $transformed.Content)) { 'Unchanged' }
            elseif ($propChanges.Count -eq 0) { 'Formatting' }
            else { 'Modified' }

        $sqlDelta = Build-SqlDeltaText -TableName $tableName -ChangeType $changeType -PropertyChanges $propChanges -ClassName $transformed.ClassName -IsView:([bool]$transformed.IsView)
        $summary = switch ($changeType) {
            'Added'   {
                if ($transformed.IsView) { "New view → $($transformed.ClassName) (keyless)" }
                else { "New table/entity → $($transformed.ClassName)" }
            }
            'Modified'{ "$($propChanges.Count) property change(s)" }
            'Unchanged'{
                if ($transformed.IsView) { 'No differences (view / keyless POCO)' }
                elseif ($transformed.ClassName -eq 'CustomEntity') { 'No differences (Entity table → CustomEntity)' }
                else { 'No differences' }
            }
            'Formatting'{ 'Cosmetic / attribute-only differences' }
            default { $changeType }
        }

        # Display: Entity table is always CustomEntity in ABP models
        $displayTable = $tableName
        if ($transformed.ClassName -eq 'CustomEntity') {
            $displayTable = 'Entity'
            if ($changeType -eq 'Unchanged') {
                $summary = 'No differences — Entity table maps to CustomEntity (ABP name clash)'
            }
        }
        if ($transformed.ClassName -eq 'AppUser') {
            $displayTable = 'AbpUsers'
            if ($changeType -eq 'Unchanged') {
                $summary = 'No differences — AbpUsers maps to AppUser (IdentityUser)'
            }
        }

        $reason = if ($transformed.IsView) {
            'Database view — keyless POCO (no Entity<T> / Id override)'
        } elseif ($transformed.ClassName -eq 'CustomEntity') {
            'Entity table → CustomEntity (required; not a rename change)'
        } elseif ($transformed.ClassName -eq 'AppUser') {
            'AbpUsers → AppUser (IdentityUser exception from ABP ignore)'
        } else { $null }

        $diffs.Add([pscustomobject]@{
            TableName       = $displayTable
            ClassName       = $transformed.ClassName
            ScaffoldClass   = $scaffoldClass
            ChangeType      = $changeType
            Included        = $false
            Reason          = $reason
            ExistingPath    = $existingPath
            TransformedPath = $outPath
            OldContent      = $existingContent
            NewContent      = $transformed.Content
            SqlDelta        = $sqlDelta
            PropertyChanges = @($propChanges)
            Summary         = $summary
            EntityType      = $transformed.EntityType
            IsView          = [bool]$transformed.IsView
        }) | Out-Null
    }

    # Detect removed tables (exist in Models but not in scaffold) — informational only
    $scaffoldNames = [System.Collections.Generic.HashSet[string]]::new([StringComparer]::OrdinalIgnoreCase)
    $scaffoldTables = [System.Collections.Generic.HashSet[string]]::new([StringComparer]::OrdinalIgnoreCase)
    foreach ($d in $diffs) {
        [void]$scaffoldNames.Add($d.ClassName)
        [void]$scaffoldTables.Add($d.TableName)
        if ($d.ScaffoldClass) { [void]$scaffoldNames.Add($d.ScaffoldClass) }
    }
    # Entity table is modeled as CustomEntity — never treat CustomEntity as missing when Entity was scaffolded
    if ($scaffoldTables.Contains('Entity') -or $scaffoldNames.Contains('Entity')) {
        [void]$scaffoldNames.Add('CustomEntity')
        [void]$scaffoldTables.Add('Entity')
    }
    if ($scaffoldTables.Contains('AbpUsers') -or $scaffoldNames.Contains('AbpUsers')) {
        [void]$scaffoldNames.Add('AppUser')
        [void]$scaffoldTables.Add('AbpUsers')
    }

    $existingModels = Get-ChildItem -LiteralPath $Paths.ModelsDir -Filter '*.cs' -File |
        Where-Object { $_.Name -notin @('User.cs') }
    foreach ($em in $existingModels) {
        $cls = [IO.Path]::GetFileNameWithoutExtension($em.Name)
        if ($scaffoldNames.Contains($cls)) { continue }
        # Alias: CustomEntity maps from Entity table
        if ($cls -eq 'CustomEntity' -and ($scaffoldNames.Contains('Entity') -or $scaffoldTables.Contains('Entity'))) {
            continue
        }
        $txt = Get-Content -LiteralPath $em.FullName -Raw -Encoding UTF8
        if ($txt -notmatch '\[Table\(') { continue }
        if ($txt -match '\[Table\("([^"]+)"\)\]') {
            $tn = $Matches[1]
            if ($IgnoreAbpTables -and (Test-IsAbpSystemTable -TableName $tn)) { continue }
            if ($scaffoldTables.Contains($tn)) { continue }
            # Table("Entity") owned by CustomEntity
            if ($tn -eq 'Entity' -and $scaffoldNames.Contains('CustomEntity')) { continue }
        } else { $tn = $cls }
        $diffs.Add([pscustomobject]@{
            TableName=$tn; ClassName=$cls; ScaffoldClass=$cls; ChangeType='MissingInDb'
            Included=$false; Reason='Present in Domain Models but not returned by scaffold'
            ExistingPath=$em.FullName; TransformedPath=$null; OldContent=$txt; NewContent=$null
            SqlDelta="-- WARNING: [$tn] exists in Models but was not scaffolded from DB"
            PropertyChanges=@(); Summary='In models, not in DB scaffold'; EntityType=$null
        }) | Out-Null
    }

    Write-GPayLog 'INFO' ("Diff summary: " + (
        ($diffs | Group-Object ChangeType | ForEach-Object { "$($_.Name)=$($_.Count)" }) -join ', '
    ))
    return @($diffs)
}

function Normalize-Code {
    param([string]$Text)
    if (-not $Text) { return '' }
    $t = $Text -replace '\r\n','`n' -replace '\r','`n'
    $t = [regex]::Replace($t, '\s+', ' ')
    return $t.Trim()
}

function Build-SqlDeltaText {
    param(
        [string]$TableName,
        [string]$ChangeType,
        [string]$ClassName,
        $PropertyChanges,
        [switch]$IsView
    )
    $sb = [System.Text.StringBuilder]::new()
    $kind = if ($IsView) { 'VIEW' } else { 'TABLE' }
    [void]$sb.AppendLine("-- G-PAY schema delta for [$TableName] ($ClassName) — $ChangeType ($kind)")
    if ($IsView) {
        [void]$sb.AppendLine("-- View: keyless POCO — no Entity<T>, no Id override.")
    }
    if ($ChangeType -eq 'Added') {
        if ($IsView) {
            [void]$sb.AppendLine("-- CREATE VIEW [$TableName] AS … (scaffold maps ToView)")
        } else {
            [void]$sb.AppendLine("CREATE TABLE [$TableName] (")
        }
        foreach ($p in $PropertyChanges) {
            if ($p.IsNav) { continue }
            $col = if ($p.Column) { $p.Column } else { $p.Name }
            [void]$sb.AppendLine("    [$col] $($p.NewType),  -- NEW")
        }
        if (-not $IsView) { [void]$sb.AppendLine(');') }
        return $sb.ToString()
    }
    if ($ChangeType -eq 'Unchanged') {
        [void]$sb.AppendLine("-- No column changes detected.")
        return $sb.ToString()
    }
    foreach ($p in $PropertyChanges) {
        if ($p.IsNav) {
            [void]$sb.AppendLine("-- NAV $($p.Change): $($p.Name)")
            continue
        }
        $col = if ($p.Column) { $p.Column } else { $p.Name }
        switch ($p.Change) {
            'Added' {
                if ($IsView) { [void]$sb.AppendLine("-- VIEW column NEW: [$col] $($p.NewType)") }
                else { [void]$sb.AppendLine("ALTER TABLE [$TableName] ADD [$col] $($p.NewType) NULL;  -- NEW") }
            }
            'Removed' {
                if ($IsView) { [void]$sb.AppendLine("-- VIEW column REMOVED: [$col]") }
                else { [void]$sb.AppendLine("ALTER TABLE [$TableName] DROP COLUMN [$col];  -- REMOVED FROM DB / MODEL") }
            }
            'Modified' {
                if ($IsView) { [void]$sb.AppendLine("-- VIEW column ALTER: [$col] $($p.NewType) (was $($p.OldType))") }
                else { [void]$sb.AppendLine("ALTER TABLE [$TableName] ALTER COLUMN [$col] $($p.NewType);  -- was $($p.OldType)") }
            }
        }
    }
    if ($PropertyChanges.Count -eq 0) {
        [void]$sb.AppendLine('-- Attribute / formatting differences only (no column add/drop/alter).')
    }
    return $sb.ToString()
}
