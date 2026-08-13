# Re-scaffold GPayDev into GPay.Banking.Persistence
# Set connection string first (do not commit passwords):
#   $env:GPAY_BANKING_CS = 'Data Source=10.10.1.100;Initial Catalog=GPayDev;User ID=...;Password=...;Encrypt=True;TrustServerCertificate=True'
# Then from Source Code:
#   .\scripts\scaffold-gpaydev.ps1

$ErrorActionPreference = "Stop"
$root = Split-Path $PSScriptRoot -Parent
Set-Location $root

$cs = $env:GPAY_BANKING_CS
if ([string]::IsNullOrWhiteSpace($cs)) {
    throw "Set environment variable GPAY_BANKING_CS to the GPayDev connection string before scaffolding."
}

dotnet ef dbcontext scaffold $cs Microsoft.EntityFrameworkCore.SqlServer `
  --project src/GPay.Banking.Persistence/GPay.Banking.Persistence.csproj `
  --startup-project src/GPay.Banking.Persistence/GPay.Banking.Persistence.csproj `
  --output-dir Entities `
  --context-dir Data `
  --context BankingDbContext `
  --namespace GPay.Banking.Persistence.Entities `
  --context-namespace GPay.Banking.Persistence.Data `
  --no-onconfiguring `
  --force

Write-Host "Scaffold complete. Repositories/UoW under Persistence/Repositories are preserved."
