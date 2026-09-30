# Local HTTP review server — serves UI and applies tables one chunk at a time.

function Start-GPayReviewServer {
    param(
        [Parameter(Mandatory)][string]$UiDir,
        [Parameter(Mandatory)][string]$ManifestPath,
        [string]$DecisionPath,
        [hashtable]$ApplyContext = $null,
        [int]$Port = 0
    )

    if ($Port -le 0) {
        $tcp = [System.Net.Sockets.TcpListener]::new([System.Net.IPAddress]::Loopback, 0)
        $tcp.Start()
        $Port = ([System.Net.IPEndPoint]$tcp.LocalEndpoint).Port
        $tcp.Stop()
    }

    $prefix = "http://127.0.0.1:$Port/"
    $listener = [System.Net.HttpListener]::new()
    $listener.Prefixes.Add($prefix)
    try {
        $listener.Start()
    } catch {
        Write-GPayLog 'WARN' "HttpListener failed on $prefix — $($_.Exception.Message)"
        throw
    }

    $contentDir = [IO.Path]::ChangeExtension($ManifestPath, $null) + '-content'

    Write-GPayLog 'INFO' "Review UI listening at $prefix"
    return [pscustomobject]@{
        Listener            = $listener
        Port                = $Port
        Url                 = $prefix
        UiDir               = $UiDir
        ManifestPath        = $ManifestPath
        DecisionPath        = $DecisionPath
        ContentDir          = $contentDir
        ApplyContext        = $ApplyContext
        BackupDir           = $null
        Applied             = [System.Collections.Generic.List[string]]::new()
        Skipped             = [System.Collections.Generic.List[string]]::new()
        ApplyStarted        = $false
    }
}

function Get-GPayDecisionSignalPath {
    param([string]$DecisionPath)
    if ([string]::IsNullOrWhiteSpace($DecisionPath)) { return $null }
    return "$DecisionPath.signal"
}

function Read-HttpJsonBody {
    param($Request)
    $ms = New-Object System.IO.MemoryStream
    $Request.InputStream.CopyTo($ms)
    $bytes = $ms.ToArray()
    $ms.Dispose()
    if ($bytes.Length -eq 0) { return $null }
    $text = [Text.Encoding]::UTF8.GetString($bytes)
    if ((Get-Command ConvertFrom-Json).Parameters.ContainsKey('Depth')) {
        return $text | ConvertFrom-Json -Depth 20
    }
    return $text | ConvertFrom-Json
}

function Wait-GPayReviewDecision {
    param(
        [Parameter(Mandatory)]$Server,
        [int]$TimeoutMinutes = 120
    )

    $listener = $Server.Listener
    $deadline = (Get-Date).AddMinutes($TimeoutMinutes)
    $done = $false
    $resultAction = 'cancel'
    $signalPath = Get-GPayDecisionSignalPath -DecisionPath $Server.DecisionPath

    if ($Server.DecisionPath -and (Test-Path -LiteralPath $Server.DecisionPath)) {
        Remove-Item -LiteralPath $Server.DecisionPath -Force -ErrorAction SilentlyContinue
    }
    if ($signalPath -and (Test-Path -LiteralPath $signalPath)) {
        Remove-Item -LiteralPath $signalPath -Force -ErrorAction SilentlyContinue
    }

    Write-Host ''
    Write-Host "Open the review page in your browser:" -ForegroundColor White
    Write-Host "  $($Server.Url)" -ForegroundColor Green
    Write-Host 'Click Okay to apply tables one-by-one, or Cancel to abort.' -ForegroundColor DarkGray
    Write-Host 'Waiting for UI...' -ForegroundColor Cyan

    try { Start-Process $Server.Url } catch { Write-GPayLog 'WARN' "Could not auto-open browser: $($_.Exception.Message)" }

    # Keep a single outstanding BeginGetContext — never abandon it on timeout
    $async = $listener.BeginGetContext($null, $null)

    while (-not $done) {
        if ((Get-Date) -gt $deadline) {
            throw "Timed out waiting for review UI after $TimeoutMinutes minute(s)."
        }
        if (-not $listener.IsListening) {
            throw 'HTTP listener stopped unexpectedly.'
        }

        if ($signalPath -and (Test-Path -LiteralPath $signalPath)) {
            $signal = (Get-Content -LiteralPath $signalPath -Raw -Encoding UTF8).Trim()
            Write-GPayLog 'OK' "Decision signal received: $signal"
            $resultAction = if ($signal -match 'cancel') { 'cancel' } else { 'finish' }
            $done = $true
            break
        }

        $waited = $async.AsyncWaitHandle.WaitOne(250)
        if (-not $waited) { continue }

        try {
            $ctx = $listener.EndGetContext($async)
        } catch {
            Write-GPayLog 'WARN' "EndGetContext: $($_.Exception.Message)"
            try { $async = $listener.BeginGetContext($null, $null) } catch {}
            continue
        }

        # Re-arm before handling so the next request can queue while we work
        try { $async = $listener.BeginGetContext($null, $null) } catch {
            Write-GPayLog 'WARN' "BeginGetContext: $($_.Exception.Message)"
        }

        $req = $ctx.Request
        $res = $ctx.Response

        try {
            $path = $req.Url.AbsolutePath.TrimEnd('/')
            if ([string]::IsNullOrEmpty($path)) { $path = '/' }
            $method = $req.HttpMethod
            Write-GPayLog 'VERBOSE' "$method $path"

            if ($method -eq 'GET' -and ($path -eq '/' -or $path -eq '/index.html')) {
                Write-HttpFile -Response $res -FilePath (Join-Path $Server.UiDir 'index.html') -ContentType 'text/html; charset=utf-8'
            }
            elseif ($method -eq 'GET' -and $path -eq '/app.js') {
                Write-HttpFile -Response $res -FilePath (Join-Path $Server.UiDir 'app.js') -ContentType 'application/javascript; charset=utf-8'
            }
            elseif ($method -eq 'GET' -and $path -eq '/styles.css') {
                Write-HttpFile -Response $res -FilePath (Join-Path $Server.UiDir 'styles.css') -ContentType 'text/css; charset=utf-8'
            }
            elseif ($method -eq 'GET' -and $path -eq '/api/manifest') {
                Write-HttpFile -Response $res -FilePath $Server.ManifestPath -ContentType 'application/json; charset=utf-8'
            }
            elseif ($method -eq 'GET' -and $path -eq '/api/health') {
                Write-HttpJson -Response $res -Object @{
                    ok = $true
                    waiting = (-not $done)
                    applyStarted = [bool]$Server.ApplyStarted
                    applied = $Server.Applied.Count
                    skipped = $Server.Skipped.Count
                }
            }
            elseif ($method -eq 'GET' -and $path -match '^/api/content/([A-Za-z0-9_\-]+)$') {
                $key = $Matches[1]
                $file = Join-Path $Server.ContentDir "$key.json"
                if (Test-Path -LiteralPath $file) {
                    Write-HttpFile -Response $res -FilePath $file -ContentType 'application/json; charset=utf-8'
                } else {
                    $res.StatusCode = 404
                    $res.Close()
                }
            }
            elseif ($method -eq 'GET' -and $path -match '^/assets/(.+)$') {
                $assetName = $Matches[1]
                if ($assetName -match '[\\/]' -or $assetName -match '\.\.') {
                    $res.StatusCode = 400; $res.Close()
                } else {
                    $assetPath = Join-Path (Join-Path $Server.UiDir 'assets') $assetName
                    if (-not (Test-Path -LiteralPath $assetPath)) { $res.StatusCode = 404; $res.Close() }
                    else {
                        $ct = switch -Regex ([IO.Path]::GetExtension($assetPath).ToLowerInvariant()) {
                            '\.png'  { 'image/png' }
                            '\.jpe?g'{ 'image/jpeg' }
                            '\.svg'  { 'image/svg+xml' }
                            '\.webp' { 'image/webp' }
                            default  { 'application/octet-stream' }
                        }
                        Write-HttpFile -Response $res -FilePath $assetPath -ContentType $ct
                    }
                }
            }
            elseif ($method -eq 'POST' -and $path -eq '/api/apply/start') {
                $body = Read-HttpJsonBody -Request $req
                $total = 0
                if ($body -and $null -ne $body.total) { $total = [int]$body.total }

                if (-not $Server.ApplyContext) {
                    Write-HttpJson -Response $res -Object @{ ok = $false; message = 'Apply context not configured' }
                } else {
                    $backupDir = Join-Path $Server.ApplyContext.WorkDir ("backup-" + (Get-Date -Format 'yyyyMMdd-HHmmss'))
                    New-Item -ItemType Directory -Force -Path $backupDir | Out-Null
                    $Server.BackupDir = $backupDir
                    $Server.ApplyStarted = $true
                    $Server.Applied.Clear()
                    $Server.Skipped.Clear()
                    Write-GPayLog 'INFO' "Chunked apply started — expecting $total table(s). Backup: $backupDir"
                    Write-HttpJson -Response $res -Object @{
                        ok = $true
                        backupDir = $backupDir
                        total = $total
                    }
                }
            }
            elseif ($method -eq 'POST' -and $path -eq '/api/apply/one') {
                $body = Read-HttpJsonBody -Request $req
                if (-not $Server.ApplyContext) {
                    Write-HttpJson -Response $res -Object @{ ok = $false; message = 'Apply context not configured' }
                }
                elseif (-not $Server.ApplyStarted -or -not $Server.BackupDir) {
                    Write-HttpJson -Response $res -Object @{ ok = $false; message = 'Call /api/apply/start first' }
                }
                elseif (-not $body) {
                    Write-HttpJson -Response $res -Object @{ ok = $false; message = 'Empty body' }
                }
                else {
                    $tableName = [string]$body.tableName
                    $className = [string]$body.className
                    $index = if ($null -ne $body.index) { [int]$body.index } else { 0 }
                    $total = if ($null -ne $body.total) { [int]$body.total } else { 0 }
                    $excludeCols = @()
                    if ($body.excludeColumns) {
                        $excludeCols = @($body.excludeColumns | ForEach-Object { [string]$_ } | Where-Object { $_ })
                    }

                    Write-GPayLog 'INFO' ("Applying chunk {0}/{1}: {2} ({3})" -f $index, $total, $tableName, $className)

                    $diff = Find-GPayDiff `
                        -Diffs $Server.ApplyContext.Diffs `
                        -TableName $tableName `
                        -ClassName $className

                    if (-not $diff) {
                        $msg = "Not found in diff: $tableName / $className"
                        Write-GPayLog 'WARN' $msg
                        $Server.Skipped.Add("$tableName ($msg)") | Out-Null
                        Write-HttpJson -Response $res -Object @{
                            ok = $false
                            skipped = $true
                            tableName = $tableName
                            className = $className
                            index = $index
                            total = $total
                            message = $msg
                            applied = $Server.Applied.Count
                            skippedCount = $Server.Skipped.Count
                        }
                    }
                    else {
                        try {
                            $result = Apply-GPaySingleTable `
                                -Paths $Server.ApplyContext.Paths `
                                -Diff $diff `
                                -ScaffoldContextPath $Server.ApplyContext.ScaffoldContextPath `
                                -BackupDir $Server.BackupDir `
                                -ExcludeColumns $excludeCols

                            if ($result.Ok -and -not $result.Skipped) {
                                $Server.Applied.Add($result.ClassName) | Out-Null
                            } else {
                                $Server.Skipped.Add("$($result.TableName) ($($result.Message))") | Out-Null
                            }

                            Write-HttpJson -Response $res -Object @{
                                ok = [bool]$result.Ok
                                skipped = [bool]$result.Skipped
                                tableName = $result.TableName
                                className = $result.ClassName
                                index = $index
                                total = $total
                                message = $result.Message
                                applied = $Server.Applied.Count
                                skippedCount = $Server.Skipped.Count
                            }
                        } catch {
                            $err = $_.Exception.Message
                            Write-GPayLog 'ERROR' "Apply failed for $tableName : $err"
                            $Server.Skipped.Add("$tableName (error: $err)") | Out-Null
                            Write-HttpJson -Response $res -Object @{
                                ok = $false
                                skipped = $true
                                tableName = $tableName
                                className = $className
                                index = $index
                                total = $total
                                message = $err
                                applied = $Server.Applied.Count
                                skippedCount = $Server.Skipped.Count
                            }
                        }
                    }
                }
            }
            elseif ($method -eq 'POST' -and $path -eq '/api/apply/finish') {
                $summary = @{
                    ok = $true
                    action = 'finish'
                    applied = @($Server.Applied)
                    skipped = @($Server.Skipped)
                    backupDir = $Server.BackupDir
                    appliedCount = $Server.Applied.Count
                    skippedCount = $Server.Skipped.Count
                }
                if ($Server.DecisionPath) {
                    ($summary | ConvertTo-Json -Depth 6) | Set-Content -LiteralPath $Server.DecisionPath -Encoding UTF8
                }
                if ($signalPath) {
                    [IO.File]::WriteAllText($signalPath, 'finish', [Text.UTF8Encoding]::new($false))
                }
                Write-GPayLog 'OK' "Chunked apply finished — $($Server.Applied.Count) applied, $($Server.Skipped.Count) skipped"
                Write-HttpJson -Response $res -Object $summary
                $resultAction = 'finish'
                $done = $true
            }
            elseif ($method -eq 'POST' -and ($path -eq '/api/cancel' -or $path -eq '/api/confirm')) {
                # Legacy /api/confirm rejected — use chunked apply
                if ($path -eq '/api/confirm') {
                    Write-HttpJson -Response $res -Object @{
                        ok = $false
                        message = 'Use /api/apply/start + /api/apply/one + /api/apply/finish'
                    }
                } else {
                    if ($Server.DecisionPath) {
                        [IO.File]::WriteAllText($Server.DecisionPath, '{"action":"cancel"}', [Text.UTF8Encoding]::new($false))
                    }
                    if ($signalPath) {
                        [IO.File]::WriteAllText($signalPath, 'cancel', [Text.UTF8Encoding]::new($false))
                    }
                    Write-HttpJson -Response $res -Object @{ ok = $true; message = 'Cancelled' }
                    $resultAction = 'cancel'
                    $done = $true
                }
            }
            else {
                $res.StatusCode = 404
                $bytes = [Text.Encoding]::UTF8.GetBytes('Not found')
                $res.ContentLength64 = $bytes.LongLength
                $res.OutputStream.Write($bytes, 0, $bytes.Length)
                $res.Close()
            }
        } catch {
            Write-GPayLog 'ERROR' "HTTP handler error: $($_.Exception.Message)"
            try {
                $res.StatusCode = 500
                $bytes = [Text.Encoding]::UTF8.GetBytes([string]$_.Exception.Message)
                $res.ContentLength64 = $bytes.LongLength
                $res.OutputStream.Write($bytes, 0, $bytes.Length)
                $res.Close()
            } catch {}
        }
    }

    return [pscustomobject]@{
        action     = $resultAction
        Applied    = @($Server.Applied)
        Skipped    = @($Server.Skipped)
        BackupDir  = $Server.BackupDir
        fromFile   = $true
    }
}

function Stop-GPayReviewServer {
    param($Server)
    try {
        if ($Server -and $Server.Listener -and $Server.Listener.IsListening) {
            $Server.Listener.Stop()
            $Server.Listener.Close()
            Write-GPayLog 'VERBOSE' 'Review HTTP server stopped.'
        }
    } catch {
        Write-GPayLog 'WARN' "Error stopping listener: $($_.Exception.Message)"
    }
}

function Write-HttpFile {
    param($Response, [string]$FilePath, [string]$ContentType)
    $bytes = [IO.File]::ReadAllBytes($FilePath)
    $Response.StatusCode = 200
    $Response.ContentType = $ContentType
    $Response.ContentLength64 = $bytes.LongLength
    $Response.AddHeader('Cache-Control', 'no-store')
    $Response.OutputStream.Write($bytes, 0, $bytes.Length)
    $Response.Close()
}

function Write-HttpJson {
    param($Response, $Object)
    $json = $Object | ConvertTo-Json -Depth 8 -Compress
    $bytes = [Text.Encoding]::UTF8.GetBytes($json)
    $Response.StatusCode = 200
    $Response.ContentType = 'application/json; charset=utf-8'
    $Response.ContentLength64 = $bytes.LongLength
    $Response.AddHeader('Cache-Control', 'no-store')
    $Response.OutputStream.Write($bytes, 0, $bytes.Length)
    $Response.Close()
}

function Export-GPayReviewManifest {
    param(
        [Parameter(Mandatory)]$Diffs,
        [Parameter(Mandatory)][string]$OutPath,
        [bool]$IgnoreAbpTables = $true,
        [string]$ConnectionDisplay = '',
        [string]$ConnectionMasked = '',
        [string]$UserName = ''
    )

    $contentDir = [IO.Path]::ChangeExtension($OutPath, $null) + '-content'
    if (Test-Path -LiteralPath $contentDir) {
        Remove-Item -LiteralPath $contentDir -Recurse -Force -ErrorAction SilentlyContinue
    }
    New-Item -ItemType Directory -Force -Path $contentDir | Out-Null

    $tables = @()
    foreach ($d in $Diffs) {
        $cols = @()
        foreach ($p in @($d.PropertyChanges)) {
            $cols += [ordered]@{
                name      = $p.Name
                change    = $p.Change
                oldType   = $p.OldType
                newType   = $p.NewType
                column    = $p.Column
                included  = [bool]$p.Included
                isNav     = [bool]$p.IsNav
            }
        }

        $safeName = ($d.ClassName -replace '[^\w\-]', '_')
        $contentFile = Join-Path $contentDir "$safeName.json"
        $contentObj = [ordered]@{
            className  = $d.ClassName
            tableName  = $d.TableName
            sqlDelta   = $d.SqlDelta
            oldContent = $d.OldContent
            newContent = $d.NewContent
        }
        [IO.File]::WriteAllText(
            $contentFile,
            ($contentObj | ConvertTo-Json -Depth 6),
            [Text.UTF8Encoding]::new($false)
        )

        $tables += [ordered]@{
            tableName   = $d.TableName
            className   = $d.ClassName
            changeType  = $d.ChangeType
            included    = [bool]$d.Included
            summary     = $d.Summary
            reason      = $d.Reason
            contentKey  = $safeName
            columns     = $cols
        }
    }

    $summary = [ordered]@{
        added        = @($Diffs | Where-Object ChangeType -eq 'Added').Count
        modified     = @($Diffs | Where-Object ChangeType -eq 'Modified').Count
        unchanged    = @($Diffs | Where-Object ChangeType -eq 'Unchanged').Count
        formatting   = @($Diffs | Where-Object ChangeType -eq 'Formatting').Count
        skipped      = @($Diffs | Where-Object ChangeType -eq 'Skipped').Count
        missingInDb  = @($Diffs | Where-Object ChangeType -eq 'MissingInDb').Count
        total        = $Diffs.Count
    }

    if ([string]::IsNullOrWhiteSpace($UserName)) {
        $UserName = $env:USERNAME
        if ([string]::IsNullOrWhiteSpace($UserName)) { $UserName = $env:USER }
        if ([string]::IsNullOrWhiteSpace($UserName)) { $UserName = 'User' }
    }

    $manifest = [ordered]@{
        generatedAt       = (Get-Date).ToString('o')
        product           = 'G-PAY'
        userName          = $UserName
        connection        = $ConnectionDisplay
        connectionSource  = $ConnectionDisplay
        connectionMasked  = $ConnectionMasked
        ignoreAbpTables   = $IgnoreAbpTables
        abpPrefixes       = $script:DefaultAbpIgnorePrefixes
        abpExceptions     = @($script:AbpTableIgnoreExceptions)
        summary           = $summary
        tables            = $tables
    }

    $json = $manifest | ConvertTo-Json -Depth 12
    [IO.File]::WriteAllText($OutPath, $json, [Text.UTF8Encoding]::new($false))
    Write-GPayLog 'OK' "Wrote review manifest: $OutPath (diffs split under $contentDir)"
    return $OutPath
}
