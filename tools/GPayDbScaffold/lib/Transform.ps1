# Transform raw EF scaffold models into G-PAY ABP Entity<T> format

function Get-GPayResolvedClassName {
    param([string]$ScaffoldClassName)
    if ($script:ClassRenameMap.ContainsKey($ScaffoldClassName)) {
        return $script:ClassRenameMap[$ScaffoldClassName]
    }
    return $ScaffoldClassName
}

function Get-ExistingModelPath {
    param(
        [string]$ModelsDir,
        [string]$ClassName
    )
    $resolved = Get-GPayResolvedClassName $ClassName
    $candidates = @(
        (Join-Path $ModelsDir "$resolved.cs"),
        (Join-Path $ModelsDir "$ClassName.cs")
    )
    foreach ($c in $candidates) {
        if (Test-Path -LiteralPath $c) { return $c }
    }
    return $null
}

# Identity / ABP user base columns — stay on IdentityUser, not AppUser
$script:IdentityUserBaseProperties = @(
    'Id', 'UserName', 'NormalizedUserName', 'Name', 'Surname', 'Email', 'NormalizedEmail',
    'EmailConfirmed', 'PasswordHash', 'SecurityStamp', 'ConcurrencyStamp',
    'PhoneNumber', 'PhoneNumberConfirmed', 'TwoFactorEnabled', 'LockoutEnd',
    'LockoutEnabled', 'AccessFailedCount', 'IsActive', 'IsExternal',
    'ExtraProperties', 'EntityVersion', 'LastPasswordChangeTime',
    'ShouldChangePasswordOnNextLogin'
)

function ConvertTo-GPayAppUserModel {
    param(
        [Parameter(Mandatory)][string]$ScaffoldContent,
        [string]$ExistingContent,
        [string[]]$ExcludeColumns = @()
    )

    $content = $ScaffoldContent
    $scaffoldClass = 'AbpUsers'
    if ($content -match 'public\s+partial\s+class\s+(\w+)') { $scaffoldClass = $Matches[1] }

    # Rename to AppUser + IdentityUser base
    $content = $content -replace "public\s+partial\s+class\s+$scaffoldClass(\s*:\s*[^{\r\n]+)?", 'public class AppUser : IdentityUser'
    if ($content -notmatch '\[Table\(') {
        $content = $content -replace '(public class AppUser)', "[Table(`"AbpUsers`")]`r`n    `$1"
    } else {
        $content = $content -replace '\[Table\("[^"]+"\)\]', '[Table("AbpUsers")]'
    }

    # Usings
    if ($content -notmatch 'using Volo\.Abp\.Identity') {
        $content = "using Volo.Abp.Identity;`r`n" + $content
    }
    $content = $content -replace 'using Volo\.Abp\.Domain\.Entities;\r?\n', ''

    # Drop Identity base properties from the scaffolded POCO (they live on IdentityUser)
    foreach ($prop in $script:IdentityUserBaseProperties) {
        $content = Remove-ModelProperty -Content $content -PropertyName $prop
    }
    foreach ($col in @($ExcludeColumns | Where-Object { $_ })) {
        $content = Remove-ModelProperty -Content $content -PropertyName $col
    }

    # Prefer existing constructors / class shape when present
    if ($ExistingContent) {
        $content = Merge-CustomMembers -NewContent $content -ExistingContent $ExistingContent
        # Restore IdentityUser constructors from existing AppUser if scaffold wiped them
        if ($ExistingContent -match '(?ms)(public\s+AppUser\s*\([^)]*\)\s*(?::\s*base\([^;]+;\s*)?)' -and
            $content -notmatch 'public\s+AppUser\s*\(') {
            $ctors = [regex]::Matches($ExistingContent, '(?ms)^\s*public\s+AppUser\s*\([^)]*\)\s*(?::\s*base\([^)]*\))?\s*\{[^}]*\}')
            if ($ctors.Count -gt 0) {
                $ctorBlock = ($ctors | ForEach-Object { $_.Value.TrimEnd() }) -join "`r`n`r`n        "
                $content = $content -replace '(public class AppUser\s*:\s*IdentityUser\s*\{)', "`$1`r`n`r`n        $ctorBlock`r`n"
            }
        }
    } else {
        # Minimal constructors matching current AppUser
        $ctor = @"

        public AppUser()
        {
        }

        public AppUser(Guid id, string userName, string email)
        : base(id, userName, email)
        {
        }
"@
        $content = $content -replace '(public class AppUser\s*:\s*IdentityUser\s*\{)', "`$1$ctor"
    }

    $content = $content -replace 'namespace\s+[^\s{;]+', 'namespace GPay.Banking.BankHub'
    # class should not stay partial when matching hand-maintained AppUser style
    $content = $content -replace 'public\s+partial\s+class\s+AppUser', 'public class AppUser'

    return [pscustomobject]@{
        Skip          = $false
        ClassName     = 'AppUser'
        ScaffoldClass = $scaffoldClass
        Content       = $content
        EntityType    = 'Guid'
        Reason        = 'AbpUsers → AppUser (IdentityUser exception)'
    }
}

function Test-GPayIsDbView {
    param(
        [string]$ScaffoldContent,
        [string]$ExistingContent,
        [string]$TableName,
        [string]$ClassName,
        [object]$ViewNames = $null,
        [string]$ScaffoldContextText = $null
    )

    $inViewSet = $false
    if ($null -ne $ViewNames) {
        foreach ($key in @($TableName, $ClassName)) {
            if ([string]::IsNullOrWhiteSpace($key)) { continue }
            if ($ViewNames -is [System.Collections.IDictionary]) {
                if ($ViewNames.Contains($key)) { $inViewSet = $true; break }
            } elseif ($ViewNames.PSObject.Methods.Name -contains 'Contains') {
                if ($ViewNames.Contains($key)) { $inViewSet = $true; break }
            }
        }
    }

    $toViewInContext = $false
    if ($ScaffoldContextText -and $ClassName) {
        $escaped = [regex]::Escape($ClassName)
        if ($ScaffoldContextText -match "Entity<$escaped>\s*\([\s\S]{0,800}?\.ToView\(") {
            $toViewInContext = $true
        }
    }

    $scaffoldKeyless = ($ScaffoldContent -match '\[Keyless\]')
    $scaffoldHasKey = ($ScaffoldContent -match '\[Key\]')

    # Strong table signal: scaffolded with [Key] and not a known view → table entity
    if ($scaffoldHasKey -and -not $scaffoldKeyless -and -not $inViewSet -and -not $toViewInContext) {
        return $false
    }

    if ($scaffoldKeyless) { return $true }
    if ($inViewSet) { return $true }
    if ($toViewInContext) { return $true }

    # Existing [Keyless] alone is weak (may be from a prior bad transform) —
    # only trust it when scaffold also lacks [Key]
    if (($ExistingContent -match '\[Keyless\]') -and -not $scaffoldHasKey) {
        return $true
    }

    # SQL views in this DB commonly use a leading lowercase 'v' (vAccount).
    # MUST be ordinal/case-sensitive — 'VATAmount' / 'Vatamount' are tables.
    if ($TableName -and $TableName.StartsWith('v', [StringComparison]::Ordinal)) {
        return $true
    }

    return $false
}

function ConvertTo-GPayViewModel {
    param(
        [Parameter(Mandatory)][string]$ScaffoldContent,
        [string]$ExistingContent,
        [string[]]$ExcludeColumns = @(),
        [string]$ScaffoldClass,
        [string]$TargetClass,
        [string]$TableName
    )

    $content = $ScaffoldContent
    if (-not $ScaffoldClass -and $content -match 'public\s+partial\s+class\s+(\w+)') {
        $ScaffoldClass = $Matches[1]
    }
    if (-not $TargetClass) { $TargetClass = Get-GPayResolvedClassName $ScaffoldClass }

    # Views are keyless POCOs — never Entity<T>, never Id override
    $content = $content -replace 'using Volo\.Abp\.Domain\.Entities;\r?\n', ''
    # Drop any accidental base type (should not be on EF scaffold, but guard anyway)
    $content = $content -replace "public\s+partial\s+class\s+$([regex]::Escape($ScaffoldClass))\s*:\s*[^{\r\n]+", "public partial class $TargetClass"
    if ($ScaffoldClass -ne $TargetClass) {
        $content = $content -replace "public\s+partial\s+class\s+$([regex]::Escape($ScaffoldClass))\b", "public partial class $TargetClass"
    } else {
        $content = $content -replace "public\s+partial\s+class\s+$([regex]::Escape($TargetClass))\s*:\s*[^{\r\n]+", "public partial class $TargetClass"
    }

    if ($content -notmatch '\[Keyless\]') {
        $content = $content -replace "(public partial class $([regex]::Escape($TargetClass))\b)", "[Keyless]`r`n`$1"
    }

    # Views must not carry [Key] (EF keyless)
    $content = [regex]::Replace($content, '(?m)^\s*\[Key\]\s*\r?\n', '')

    foreach ($col in @($ExcludeColumns | Where-Object { $_ })) {
        $content = Remove-ModelProperty -Content $content -PropertyName $col
    }

    if ($ExistingContent) {
        $content = Merge-CustomMembers -NewContent $content -ExistingContent $ExistingContent
    }

    $content = Convert-GPayKnownTypeRefs -Content $content

    $display = if ($TableName) { $TableName } else { $ScaffoldClass }
    Write-GPayLog 'VERBOSE' "View model (keyless POCO, no Entity<T>): $TargetClass ← $display"

    return [pscustomobject]@{
        Skip          = $false
        ClassName     = $TargetClass
        ScaffoldClass = $ScaffoldClass
        Content       = $content
        EntityType    = $null
        PkInfo        = $null
        IsView        = $true
        Reason        = 'Database view — keyless POCO (no Entity<T> / Id override)'
    }
}

function ConvertTo-AbpModel {
    param(
        [Parameter(Mandatory)][string]$ScaffoldContent,
        [string]$ExistingContent,
        [string[]]$ExcludeColumns = @(),
        [object]$ViewNames = $null,
        [string]$ScaffoldContextText = $null
    )

    $content = $ScaffoldContent

    # Extract class name
    if ($content -notmatch 'public\s+partial\s+class\s+(\w+)') {
        throw 'Could not find partial class declaration in scaffolded model.'
    }
    $scaffoldClass = $Matches[1]
    $targetClass = Get-GPayResolvedClassName $scaffoldClass
    $tableName = $scaffoldClass
    if ($content -match '\[Table\("([^"]+)"\)\]') { $tableName = $Matches[1] }

    # AbpUsers / AppUser — never scaffold (ABP Identity)
    if ($targetClass -eq 'AppUser' -or $scaffoldClass -eq 'AbpUsers' -or $tableName -eq 'AbpUsers') {
        return [pscustomobject]@{
            Skip          = $true
            ClassName     = 'AppUser'
            ScaffoldClass = $scaffoldClass
            Content       = $null
            Reason        = 'ABP Identity (AbpUsers / AppUser) — excluded from scaffold'
            IsView        = $false
        }
    }

    if ($script:SkipScaffoldClasses -contains $scaffoldClass -or $script:SkipScaffoldClasses -contains $targetClass) {
        return [pscustomobject]@{
            Skip       = $true
            ClassName  = $targetClass
            ScaffoldClass = $scaffoldClass
            Content    = $null
            Reason     = "Hand-maintained / skipped class: $scaffoldClass"
        }
    }

    # Views: keyless POCOs only — never Entity<T> or PK→Id override
    if (Test-GPayIsDbView -ScaffoldContent $ScaffoldContent -ExistingContent $ExistingContent `
            -TableName $tableName -ClassName $scaffoldClass -ViewNames $ViewNames `
            -ScaffoldContextText $ScaffoldContextText) {
        return ConvertTo-GPayViewModel `
            -ScaffoldContent $ScaffoldContent `
            -ExistingContent $ExistingContent `
            -ExcludeColumns $ExcludeColumns `
            -ScaffoldClass $scaffoldClass `
            -TargetClass $targetClass `
            -TableName $tableName
    }

    # Ensure required usings (tables only)
    if ($content -notmatch 'using Volo\.Abp\.Domain\.Entities') {
        $content = $content -replace '(using Microsoft\.EntityFrameworkCore;\r?\n)', "`$1using Volo.Abp.Domain.Entities;`r`n"
        if ($content -notmatch 'using Volo\.Abp\.Domain\.Entities') {
            $content = "using Volo.Abp.Domain.Entities;`r`n" + $content
        }
    }

    # Rename class if needed (Entity → CustomEntity avoids clash with ABP Entity<T>)
    if ($targetClass -ne $scaffoldClass) {
        $content = $content -replace "public\s+partial\s+class\s+$scaffoldClass\b", "public partial class $targetClass"
        # Table attribute must keep the real DB table name (Entity, Menus, …)
        if ($content -notmatch '\[Table\(') {
            $tableName = if ($scaffoldClass -eq 'Entity') { 'Entity' } elseif ($scaffoldClass -match '^Menu') { 'Menus' } else { $scaffoldClass }
            $content = $content -replace "(public partial class $targetClass)", "[Table(`"$tableName`")]`r`n`$1"
        }
    }

    # Always keep [Table("Entity")] when this is CustomEntity
    if ($targetClass -eq 'CustomEntity') {
        if ($content -match '\[Table\("([^"]+)"\)\]') {
            if ($Matches[1] -ne 'Entity') {
                $content = $content -replace '\[Table\("[^"]+"\)\]', '[Table("Entity")]'
            }
        } else {
            $content = $content -replace '(public partial class CustomEntity)', "[Table(`"Entity`")]`r`n`$1"
        }
    }

    # Detect primary key property
    $pkInfo = Find-ScaffoldPrimaryKey -Content $content
    if (-not $pkInfo) {
        Write-GPayLog 'WARN' "No PK detected for $targetClass — leaving as POCO (keyless/view?)."
    }

    $entityType = if ($pkInfo) { $pkInfo.ClrType } else { $null }

    # Strip existing inheritance if scaffold added none (typical)
    if ($entityType) {
        if ($content -match "public\s+partial\s+class\s+$targetClass\s*:\s*[^{\r\n]+") {
            $content = $content -replace "public\s+partial\s+class\s+$targetClass\s*:\s*[^{\r\n]+", "public partial class $targetClass : Entity<$entityType>"
        } else {
            $content = $content -replace "public\s+partial\s+class\s+$targetClass\b", "public partial class $targetClass : Entity<$entityType>"
        }
    }

    # Entity → CustomEntity; AbpUser → AppUser (navs / collections)
    $content = Convert-GPayKnownTypeRefs -Content $content

    # Transform PK property to ABP Id
    if ($pkInfo -and $pkInfo.PropertyName -ne 'Id') {
        $content = Convert-PrimaryKeyToAbpId -Content $content -PkInfo $pkInfo
    } elseif ($pkInfo -and $pkInfo.PropertyName -eq 'Id') {
        $idPattern = "(?ms)((?:\s*\[[^\]]+\]\s*\r?\n)*)\s*public\s+$([regex]::Escape($pkInfo.ClrType))\??\s+Id\s*\{\s*get;\s*set;\s*\}"
        $idMatch = [regex]::Match($content, $idPattern)
        if ($idMatch.Success) {
            $attrs = $idMatch.Groups[1].Value
            if ($attrs -notmatch '\[Key\]') { $attrs = "    [Key]`r`n" + $attrs }
            if ($attrs -notmatch '\[Column\(' -and $pkInfo.ColumnName -and $pkInfo.ColumnName -ne 'Id') {
                $attrs = $attrs.TrimEnd() + "`r`n    [Column(`"$($pkInfo.ColumnName)`")]`r`n"
            }
            $repl = "$attrs    public override $($pkInfo.ClrType) Id { get; protected set; }"
            $content = $content.Remove($idMatch.Index, $idMatch.Length).Insert($idMatch.Index, $repl)
        }
    }

    # After PK→Id, a second scalar "Id" (e.g. Entity.ID int) must be renamed (OtherId)
    if ($entityType) {
        $content = Resolve-DuplicateIdProperty -Content $content -ExistingContent $ExistingContent
    }

    # Remove excluded columns (scalar properties)
    foreach ($col in $ExcludeColumns) {
        $content = Remove-ModelProperty -Content $content -PropertyName $col
    }

    # Merge preserved custom members from existing model (NotMapped, helpers, etc.)
    if ($ExistingContent) {
        $content = Merge-CustomMembers -NewContent $content -ExistingContent $ExistingContent
    }

    # Normalize namespace
    $content = $content -replace 'namespace\s+[^\s{;]+', 'namespace GPay.Banking.BankHub'

    return [pscustomobject]@{
        Skip          = $false
        ClassName     = $targetClass
        ScaffoldClass = $scaffoldClass
        Content       = $content
        EntityType    = $entityType
        PkInfo        = $pkInfo
        IsView        = $false
        Reason        = $null
    }
}

function Convert-EntityTypeRefsToCustomEntity {
    param([string]$Content)
    if ([string]::IsNullOrEmpty($Content)) { return $Content }

    # Keep the real DB table name on [Table("Entity")]
    $protected = $Content -replace '\[Table\("Entity"\)\]', '§§TABLE_ENTITY§§'
    # Protect ABP base Entity<T>
    $protected = [regex]::Replace($protected, '\bEntity\s*<', '§§ABP_ENTITY_BASE§§<')

    # Protect property/identifier named Entity (not the Entity table type):
    # e.g. bool? Entity, e.Entity, nameof(Entity)
    $protected = [regex]::Replace(
        $protected,
        '(?m)(\b(?:bool|byte|sbyte|char|decimal|double|float|int|uint|long|ulong|short|ushort|string|object|Guid|DateTime|DateOnly|TimeOnly|DateTimeOffset)\??\s+)Entity\b',
        '${1}§§PROP_ENTITY§§'
    )
    $protected = [regex]::Replace($protected, '\.Entity\b', '.§§PROP_ENTITY§§')
    $protected = [regex]::Replace($protected, 'nameof\(\s*Entity\s*\)', 'nameof(§§PROP_ENTITY§§)')

    # Remaining bare "Entity" type refs (navs/collections) → CustomEntity
    $protected = [regex]::Replace($protected, '\bEntity\b', 'CustomEntity')

    $protected = $protected.Replace('§§ABP_ENTITY_BASE§§<', 'Entity<')
    $protected = $protected.Replace('§§PROP_ENTITY§§', 'Entity')
    return $protected.Replace('§§TABLE_ENTITY§§', '[Table("Entity")]')
}

function Convert-AbpUserTypeRefsToAppUser {
    param([string]$Content)
    if ([string]::IsNullOrEmpty($Content)) { return $Content }
    # EF scaffolds AbpUsers → type AbpUser; G-PAY uses hand-maintained AppUser
    # Do not touch [Table("AbpUsers")] if it ever appears
    $protected = $Content -replace '\[Table\("AbpUsers"\)\]', '§§TABLE_ABPUSERS§§'
    $protected = [regex]::Replace($protected, '\bAbpUsers\b', 'AppUser')  # rare plural type / DbSet leftovers
    $protected = [regex]::Replace($protected, '\bAbpUser\b', 'AppUser')
    return $protected.Replace('§§TABLE_ABPUSERS§§', '[Table("AbpUsers")]')
}

function Convert-GPayKnownTypeRefs {
    param([string]$Content)
    $c = Convert-EntityTypeRefsToCustomEntity -Content $Content
    return (Convert-AbpUserTypeRefsToAppUser -Content $c)
}

function Normalize-GPayModelType {
    param([string]$TypeName)
    if ([string]::IsNullOrWhiteSpace($TypeName)) { return $TypeName }
    $t = Convert-GPayKnownTypeRefs -Content $TypeName
    return ($t -replace '\s+', '')
}

function Find-ScaffoldPrimaryKey {
    param([string]$Content)

    # Prefer [Key] annotated property
    $keyMatch = [regex]::Match($Content, '(?ms)\[Key\]\s*(?:\[[^\]]+\]\s*)*public\s+(\w+\??)\s+(\w+)\s*\{\s*get;\s*set;\s*\}')
    if ($keyMatch.Success) {
        $clr = $keyMatch.Groups[1].Value.TrimEnd('?')
        $prop = $keyMatch.Groups[2].Value
        $col = $null
        $block = $keyMatch.Value
        if ($block -match '\[Column\("([^"]+)"\)\]') { $col = $Matches[1] }
        elseif ($prop -match '^Pk') { $col = $prop } # best-effort
        return [pscustomobject]@{ ClrType=$clr; PropertyName=$prop; ColumnName=$col }
    }

    # Fallback: Pk* Id-like property
    $pkMatch = [regex]::Match($Content, 'public\s+(Guid|int|long|string)\s+(Pk\w+)\s*\{\s*get;\s*set;\s*\}')
    if ($pkMatch.Success) {
        return [pscustomobject]@{
            ClrType=$pkMatch.Groups[1].Value
            PropertyName=$pkMatch.Groups[2].Value
            ColumnName=$pkMatch.Groups[2].Value
        }
    }

    $idMatch = [regex]::Match($Content, 'public\s+(Guid|int|long|string)\s+Id\s*\{\s*get;\s*set;\s*\}')
    if ($idMatch.Success) {
        return [pscustomobject]@{ ClrType=$idMatch.Groups[1].Value; PropertyName='Id'; ColumnName='Id' }
    }
    return $null
}

function Convert-PrimaryKeyToAbpId {
    param(
        [string]$Content,
        $PkInfo
    )
    $prop = $PkInfo.PropertyName
    $clr = $PkInfo.ClrType
    $col = $PkInfo.ColumnName
    if (-not $col) { $col = $prop }

    $pattern = "(?ms)((?:\s*\[[^\]]+\]\s*\r?\n)*)\s*public\s+$([regex]::Escape($clr))\??\s+$([regex]::Escape($prop))\s*\{\s*get;\s*set;\s*\}"
    $m = [regex]::Match($Content, $pattern)
    if (-not $m.Success) {
        Write-GPayLog 'WARN' "Could not rewrite PK property '$prop' to Id"
        return $Content
    }

    $attrs = $m.Groups[1].Value
    if ($attrs -notmatch '\[Key\]') {
        $attrs = "    [Key]`r`n" + $attrs
    }
    if ($attrs -notmatch '\[Column\(') {
        $attrs = $attrs.TrimEnd() + "`r`n    [Column(`"$col`")]`r`n"
    }
    $attrs = $attrs -replace '(\r?\n){3,}', "`r`n`r`n"
    $replacement = "$attrs    public override $clr Id { get; protected set; }"
    return $Content.Remove($m.Index, $m.Length).Insert($m.Index, $replacement)
}

function Resolve-DuplicateIdProperty {
    param(
        [string]$Content,
        [string]$ExistingContent
    )

    # Prefer existing project's name for Column("ID") / secondary Id (e.g. OtherId on CustomEntity)
    $preferredName = 'OtherId'
    if ($ExistingContent) {
        $ex = [regex]::Match(
            $ExistingContent,
            '(?ms)(?:\s*\[[^\]]*\]\s*\r?\n)*\s*\[Column\("ID"\)\]\s*\r?\n(?:\s*\[[^\]]*\]\s*\r?\n)*\s*public\s+[\w\.\?]+\s+(\w+)\s*\{'
        )
        if ($ex.Success -and $ex.Groups[1].Value -ne 'Id') {
            $preferredName = $ex.Groups[1].Value
        } elseif ($ExistingContent -match '(?m)public\s+(?:override\s+)?[\w\.\?]+\s+OtherId\s*\{') {
            $preferredName = 'OtherId'
        }
    }

    # Non-override scalar Id left after PK was mapped to override Id (e.g. [Column("ID")] public int Id)
    $rx = '(?ms)((?:\s*\[[^\]]+\]\s*\r?\n)*)\s*public\s+(?!override\b)([\w\.\?]+)\s+Id\s*\{\s*get;\s*set;\s*\}'
    $m = [regex]::Match($Content, $rx)
    if (-not $m.Success) { return $Content }

    $attrs = $m.Groups[1].Value
    $clr = $m.Groups[2].Value
    if ($attrs -notmatch '\[Column\(') {
        $attrs = $attrs.TrimEnd() + "`r`n    [Column(`"ID`")]`r`n"
    }
    $replacement = "$attrs    public $clr $preferredName { get; set; }"
    $Content = $Content.Remove($m.Index, $m.Length).Insert($m.Index, $replacement)

    # Update Index("Id", ...) that targeted the secondary column when Column was ID —
    # only rewrite indexes that still would be ambiguous; prefer renaming Index nameof to OtherId
    # when Index lists "Id" alongside an override Id (EF index attribute uses property names).
    # CustomEntity HEAD keeps Index("Id") for the Guid PK alternate key — leave indexes untouched
    # unless the attribute clearly referenced the int column only. Safer: leave indexes as-is.

    Write-GPayLog 'VERBOSE' "Renamed secondary Id property → $preferredName (PK already mapped to override Id)"
    return $Content
}

function Remove-ModelProperty {
    param(
        [string]$Content,
        [string]$PropertyName
    )
    # Remove attribute block + property
    $pattern = "(?ms)\r?\n(?:\s*\[[^\]]+\]\s*\r?\n)*\s*public\s+[^{\n]+?\s+$([regex]::Escape($PropertyName))\s*\{[^}]*\}"
    return [regex]::Replace($Content, $pattern, '')
}

function Get-GPayBalancedBlockEnd {
    param(
        [string]$Text,
        [int]$OpenBraceIndex
    )
    $depth = 0
    for ($i = $OpenBraceIndex; $i -lt $Text.Length; $i++) {
        $ch = $Text[$i]
        if ($ch -eq '{') { $depth++ }
        elseif ($ch -eq '}') {
            $depth--
            if ($depth -eq 0) { return $i }
        }
    }
    return -1
}

function Get-GPayNotMappedMembers {
    param([string]$Content)
    if ([string]::IsNullOrEmpty($Content)) { return @() }

    $results = [System.Collections.Generic.List[string]]::new()
    # Match attribute + "public ..." up to (but not including) { or =>
    $rx = [regex]::new('(?ms)(?:^[ \t]*\[[^\]]+\]\s*\r?\n)*^[ \t]*\[NotMapped\][^\r\n]*\r?\n(?:^[ \t]*\[[^\]]+\]\s*\r?\n)*^[ \t]*public\b[\w\.\<\>\?\,\s]+?\w+(?=\s*(?:\{|=>))')
    foreach ($m in $rx.Matches($Content)) {
        $start = $m.Index
        $afterSig = $m.Index + $m.Length
        $tail = $Content.Substring($afterSig)

        # Expression-bodied: public T Name => expr;
        $expr = [regex]::Match($tail, '(?s)^\s*=>[^;]*;')
        if ($expr.Success) {
            $results.Add(($Content.Substring($start, $m.Length + $expr.Index + $expr.Length)).TrimEnd())
            continue
        }

        # Block-bodied / auto-prop: public T Name { ... }
        $braceRel = $tail.IndexOf('{')
        if ($braceRel -lt 0) { continue }
        $absBrace = $afterSig + $braceRel
        $end = Get-GPayBalancedBlockEnd -Text $Content -OpenBraceIndex $absBrace
        if ($end -lt 0) { continue }
        $results.Add(($Content.Substring($start, $end - $start + 1)).TrimEnd())
    }
    return @($results)
}

function Merge-CustomMembers {
    param(
        [string]$NewContent,
        [string]$ExistingContent
    )
    # Preserve [NotMapped] members (auto-props, expression-bodied, and full getters/setters)
    $preserved = @(Get-GPayNotMappedMembers -Content $ExistingContent)
    if ($preserved.Count -eq 0) { return $NewContent }

    # Skip members already present on the new model (by property name)
    $toInsert = [System.Collections.Generic.List[string]]::new()
    foreach ($block in $preserved) {
        $name = $null
        if ($block -match '(?m)^\s*public\s+(?:virtual\s+)?[\w\.\<\>\?\,\s]+\s+(\w+)\s*(?:\{|=>)') {
            $name = $Matches[1]
        }
        if ($name -and $NewContent -match "(?m)^\s*public\s+(?:override\s+|virtual\s+)?[\w\.\<\>\?\,\s]+\s+$([regex]::Escape($name))\s*(?:\{|=>)") {
            continue
        }
        $toInsert.Add($block)
    }
    if ($toInsert.Count -eq 0) { return $NewContent }

    $insert = ($toInsert | ForEach-Object {
        $b = $_
        if ($b -notmatch '(?m)^ {4}\S') {
            # normalize indentation to 4 spaces for class body
            ($b -split "`r?`n" | ForEach-Object {
                if ($_ -match '^\s*$') { '' } else { ($_ -replace '^\s+', '    ') }
            }) -join "`r`n"
        } else { $b }
    }) -join "`r`n`r`n"

    $idx = $NewContent.LastIndexOf('}')
    if ($idx -lt 0) { return $NewContent }
    $before = $NewContent.Substring(0, $idx).TrimEnd()
    $after = $NewContent.Substring($idx)
    return "$before`r`n`r`n    // --- preserved custom members ---`r`n$insert`r`n$after"
}

function Get-ModelPropertyNames {
    param([string]$Content)
    $names = [System.Collections.Generic.List[string]]::new()
    foreach ($m in [regex]::Matches($Content, 'public\s+(?:virtual\s+)?[\w\.\<\>\?\,\s]+\s+(\w+)\s*\{')) {
        $n = $m.Groups[1].Value
        if ($n -in @('get','set','partial')) { continue }
        if (-not $names.Contains($n)) { $names.Add($n) }
    }
    return @($names)
}

function Get-ModelPropertyDetails {
    param([string]$Content)
    $props = @()
    # Matches scalar + virtual nav properties; type may include generics like ICollection<Foo>
    $rx = '(?ms)((?:\s*\[[^\]]+\]\s*\r?\n)*)\s*public\s+(?<virt>virtual\s+)?(?<type>[\w\.]+(?:\s*<\s*[\w\.\s,\?]+>\s*)?\??)\s+(?<name>\w+)\s*\{(?<body>[^}]*)\}'
    foreach ($m in [regex]::Matches($Content, $rx)) {
        $name = $m.Groups['name'].Value
        if ($name -in @('get', 'set', 'partial')) { continue }
        $attrs = $m.Groups[1].Value
        $col = $null
        if ($attrs -match '\[Column\("([^"]+)"\)\]') { $col = $Matches[1] }
        $props += [pscustomobject]@{
            Name       = $name
            Type       = ($m.Groups['type'].Value -replace '\s+', '')
            Column     = $col
            Attributes = (($attrs -split "`n" | ForEach-Object { $_.Trim() } | Where-Object { $_ }) -join ' ')
            IsNav      = [bool]$m.Groups['virt'].Value
            Raw        = $m.Value.Trim()
        }
    }
    return $props
}
