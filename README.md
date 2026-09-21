# GPay Banking (ABP)

ASP.NET Boilerplate / ABP Framework layered banking service. Standardized against G-PAY-ABP patterns (DDD layers, feature folders, AuditedAggregateRoot, App* tables), hosted as a **standalone** Banking API under `C:\GPay\Bank Hub`.

## Architecture

```
External service / G-PAY-ABP  →  HttpApi.Host (JWT)  →  Application AppServices
                                                          → Domain ports
                                                          → Absa adapters (in-process CAPI)
Angular ops UI (web/)         →  dashboards / progress only (not primary payload source)
```

- **No RabbitMQ / Orchestrator worker.** Bank adapters run in-process.
- **Payloads** for AVS/payments are expected from **external API callers** (`SubmitBatchAsync`, `VerifyAsync`, `InitiateAsync`), not from the Angular dashboard.
- Angular under `web/` remains for health/ops dashboards.

## Solution layout

| Project | Role |
|---------|------|
| `GPay.Banking.Domain.Shared` | Enums, options (`AbsaCapi`, `AvsBatch`, `AbsaCallback`) |
| `GPay.Banking.Domain` | Capability ports + `BankHub*` aggregates |
| `GPay.Banking.Application.Contracts` | AppService interfaces, DTOs, permissions |
| `GPay.Banking.Application` | AppServices + `AvsBatchProcessJob` |
| `GPay.Banking.EntityFrameworkCore` | `BankingDbContext` (`AppBankHub*`) |
| `GPay.Banking.HttpApi` | Callbacks controller |
| `GPay.Banking.HttpApi.Host` | Host (JWT AuthServer audience `GPay`) |
| `GPay.Banking.Services` | Bank adapters (Absa/Fnb/Nedbank × Api/HostToHost) |

Legacy Orchestrator / RabbitMQ / Persistence scaffold live under `src\_legacy\` (do not deploy).

## Run

1. Apply `scripts\sql\CreateBankHubAbpTables.sql` (or add EF migrations later).
2. Set secrets (user-secrets / `GPAY_` env / `appsettings.Local.json`):
   - `AbsaCapi:*` (or keep `UseSimulator=true`)
   - `AbsaCallback:PaymentToken`
3. `dotnet run --project src\GPay.Banking.HttpApi.Host`
4. Swagger: `/swagger`

## Key APIs

- `POST /api/app/account-verification/verify` — single AVS
- `POST /api/app/account-verification/submit-batch` — external batch payload
- `GET /api/app/account-verification/batch/{id}` — batch progress (dashboard)
- `POST /api/app/instant-payment/initiate`
- `POST /api/callbacks/absa/payment` — Absa webhook (token auth)

## Config

See `src\GPay.Banking.HttpApi.Host\appsettings.json`. Default Absa mode is **simulator**.

