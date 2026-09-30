# G-PAY DB Scaffold — verbose logging helpers

$script:GPayLogPath = $null
$script:GPayLogLevel = 'Verbose'

function Initialize-GPayLog {
    param(
        [Parameter(Mandatory)][string]$WorkDir
    )
    $logDir = Join-Path $WorkDir 'logs'
    New-Item -ItemType Directory -Force -Path $logDir | Out-Null
    $stamp = Get-Date -Format 'yyyyMMdd-HHmmss'
    $script:GPayLogPath = Join-Path $logDir "scaffold-$stamp.log"
    Write-GPayLog 'INFO' "Log file: $($script:GPayLogPath)"
}

function Write-GPayLog {
    param(
        [Parameter(Position = 0)]
        [ValidateSet('VERBOSE','INFO','WARN','ERROR','OK')]
        [string]$Level = 'INFO',

        [Parameter(Mandatory, Position = 1)]
        [string]$Message
    )
    $line = '{0:yyyy-MM-dd HH:mm:ss.fff} [{1}] {2}' -f (Get-Date), $Level, $Message
    switch ($Level) {
        'VERBOSE' { Write-Verbose $Message; Write-Host $line -ForegroundColor DarkGray }
        'INFO'    { Write-Host $line -ForegroundColor Cyan }
        'WARN'    { Write-Warning $Message; Write-Host $line -ForegroundColor Yellow }
        'ERROR'   { Write-Host $line -ForegroundColor Red }
        'OK'      { Write-Host $line -ForegroundColor Green }
    }
    if ($script:GPayLogPath) {
        Add-Content -Path $script:GPayLogPath -Value $line -Encoding UTF8
    }
}
