# GPay Banking Orchestrator

.NET 10 multi-bank banking platform. GPay talks to the **Orchestrator**; each bank runs as its own host with a dedicated RabbitMQ queue and shared GPay DTOs/`ApiResult<T>` envelopes.

## Solution layout

| Project | Role |
|---------|------|
| `GPay.Banking.Contracts` | Shared DTOs, capability interfaces, MQ envelopes |
| `GPay.Banking.Infrastructure` | RabbitMQ, Serilog/ES, HTTP logging, health |
| `GPay.Banking.Persistence` | EF Core DbContext + entities scaffolded from **GPayDev**, Unit of Work |
| `GPay.Banking.Orchestrator` | HTTP + MQ ingress, routing, `/health`, `/api/queues` |
| `GPay.Banking.Absa` | Absa CAPI adapter (stubs) + queue consumer |
| `web/gpay-banking-ops` | Angular IIS ops UI (health + queues) |

## Persistence (separate project)

`GPay.Banking.Persistence` owns all database access:

- Scaffolded entities + `BankingDbContext` from SQL Server **GPayDev**
- `IUnitOfWork` / `IRepository<T>` implementations
- Re-scaffold: `.\scripts\scaffold-gpaydev.ps1`

Connection string lives in **gitignored** `appsettings.Local.json` (Orchestrator / Absa). Copy from teammate or create:

```json
{
  "ConnectionStrings": {
    "Banking": "Data Source=10.10.1.100;Initial Catalog=GPayDev;..."
  },
  "Database": {
    "UseInMemory": false,
    "EnsureCreated": false
  }
}
```

Usage:

```csharp
public class SomeService(IUnitOfWork uow)
{
    public Task<int> CountSettingsAsync(CancellationToken ct) =>
        uow.Repository<AbpSetting>().CountAsync(cancellationToken: ct);
}
```

## Run locally

```bash
# Terminal 1 — Orchestrator (http://localhost:5100)
dotnet run --project src/GPay.Banking.Orchestrator

# Terminal 2 — Absa worker (http://localhost:5101/health)
dotnet run --project src/GPay.Banking.Absa

# Terminal 3 — Angular ops UI
cd web/gpay-banking-ops
npm start
```

Requires RabbitMQ on `localhost:5672`. Elasticsearch logging is off by default (`Elasticsearch:Enabled`).

### Account verification (AVS)

GPay sends one AVS contract for every bank. The orchestrator routes by bank:

- `POST /api/avs` with `{ "bank": "Absa", "accountNumber": "...", "branchCode": "...", ... }`
- or `POST /api/Absa/account-verification` (bank in route)

Flow: **GPay → Orchestrator → Absa queue → Absa AVS (CAPI)** → correlated response.

Absa settings (`AbsaCapi`): set `BaseUrl`, `TokenUrl`, `ClientId`, `ClientSecret`, optional mTLS cert.  
`UseSimulator: true` (default) returns a realistic simulated Absa response without calling the bank.

Swagger: `http://localhost:5100/swagger`

## IIS

1. Publish Orchestrator / Absa (`dotnet publish -c Release`).
2. Create IIS sites pointing at publish folders (`web.config` included, AspNetCoreModuleV2).
3. Build Angular: `npm run build` in `web/gpay-banking-ops`.
4. Either:
   - Host `dist/gpay-banking-ops/browser` as a separate IIS site (SPA rewrite via `public/web.config`), or
   - Copy build output into Orchestrator `wwwroot` and use the same site.

URL Rewrite module is required for Angular deep links when hosted as a static site.

## Tests

```bash
dotnet test GPay.Banking.sln
```

## Adding a bank

1. Create `GPay.Banking.{Bank}` host referencing Contracts + Infrastructure.
2. Implement the capability interfaces (`IAccountVerificationService`, etc.).
3. Consume `gpay.banking.{bank}.requests` and publish to `gpay.banking.orchestrator.responses`.
4. Register the bank under `Banking:Banks` in Orchestrator config.
