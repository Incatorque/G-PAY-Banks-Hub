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
| `GPay.Banking.EntityFrameworkCore` | Bank Hub-only `BankingDbContext` for `AppBankHub*` records |
| `GPay.Banking.HttpApi` | Callbacks controller |
| `GPay.Banking.HttpApi.Host` | Host (JWT AuthServer audience `GPay`) |
| `GPay.Banking.Services` | Bank adapters (Absa/Fnb/Nedbank × Api/HostToHost) |

Legacy Orchestrator / RabbitMQ code lives under `src\_legacy\` (do not deploy). GPay order models and the legacy persistence project have been removed for re-scaffolding; PRS intake and GPay order status synchronization are paused until those models are restored.

## Run

1. Point `ConnectionStrings:Default` at the database containing the Bank Hub tables.
2. Apply `scripts\sql\CreateBankHubAbpTables.sql` to that database. The Absa PRS poller reads all PRS orders whose payer account is held at Absa; it does not require a separate PayShap marker or amount limit.
3. Set secrets (user-secrets / `GPAY_` env / `appsettings.Local.json`):
   - `AbsaCapi:*` credentials and certificate settings
   - `AbsaCallback:PaymentToken`
4. `dotnet run --project src\GPay.Banking.HttpApi.Host`
5. Swagger: `/swagger` (OpenAPI JSON: `/swagger/v1/swagger.json`)
6. Full offline API reference: [`docs/Swagger-API-Reference.md`](docs/Swagger-API-Reference.md)

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

Global settings live in `src\GPay.Banking.HttpApi.Host\appsettings.json`. Bank settings live in `appsettings.<BankCode>.json` beside it (for example, `appsettings.Absa.json`). The host loads an optional bank file for every `BankCode` at startup. Keep shared settings such as database, authentication, SMTP, and polling intervals in the global file; place bank-specific API, callback, and bank enablement settings in that bank's file. `appsettings.Local.json` and `GPAY_` environment variables can override these values.

Absa calls always use the configured CAPI endpoints; configure valid credentials and certificate settings before running payments or AVS checks.

