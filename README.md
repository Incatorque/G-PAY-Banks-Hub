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
4. Swagger: `/swagger` (OpenAPI JSON: `/swagger/v1/swagger.json`)
5. Full offline API reference: [`docs/Swagger-API-Reference.md`](docs/Swagger-API-Reference.md)

## Key APIs

| Method | Path | Auth |
|--------|------|------|
| `POST` | `/api/app/account-verification/verify` | JWT + `Banking.Avs.Verify` |
| `POST` | `/api/app/account-verification/submit-batch` | JWT + `Banking.Avs.Upload` |
| `GET` | `/api/app/account-verification/batch/{id}` | JWT + `Banking.Avs.View` |
| `GET` | `/api/app/account-verification/batch-records?batchId={id}` | JWT + `Banking.Avs.View` |
| `POST` | `/api/app/instant-payment/initiate` | JWT + `Banking.Payments.Initiate` |
| `POST` | `/api/app/instant-payment/get-status` | JWT + `Banking.Payments.View` |
| `GET` | `/api/app/instant-payment/{id}` | JWT + `Banking.Payments.View` |
| `POST` | `/api/app/transaction-history/get` | JWT + `Banking.TransactionHistory.View` |
| `POST` | `/api/app/callback/process-payment?bank=Absa` | Bank Token + IP allow-list (no JWT) |

Set `AuthServer:RequireGpayAuth` to `false` to disable JWT (Swagger **Authorize** is hidden; AppService `[Authorize]` is bypassed). Callbacks still use bank token checks.

In Swagger UI use **Authorize** with a Bearer token for protected operations when auth is on. Callback body is Absa-native JSON.

## Config

See `src\GPay.Banking.HttpApi.Host\appsettings.json`. Default Absa mode is **simulator**.

