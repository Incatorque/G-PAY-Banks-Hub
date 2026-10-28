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
- The Angular ops UI in `web/` reports Bank Hub database readiness.

## Solution layout

| Project | Role |
|---------|------|
| `GPay.Banking.Domain.Shared` | Enums and options (`AbsaCapi`, `BankCallbacks`, `AvsBatch`) |
| `GPay.Banking.Domain` | Capability ports + `BankHub*` aggregates |
| `GPay.Banking.Application.Contracts` | AppService interfaces and DTOs |
| `GPay.Banking.Application` | AppServices + `AvsBatchProcessJob` |
| `GPay.Banking.EntityFrameworkCore` | Bank Hub-only `BankingDbContext` for `AppBankHub*` records |
| `GPay.Banking.HttpApi` | ABP HTTP API module for application services |
| `GPay.Banking.HttpApi.Host` | Host (JWT AuthServer audience `GPay`) |
| `GPay.Banking.Services` | Bank capability routing and in-process adapters (Absa CAPI) |

The earlier RabbitMQ Orchestrator and worker implementation has been removed. The active Domain project contains only the Bank Hub persistence entities it uses. GPay PRS orders are read through minimal EF Core query models in `GPay.Banking.EntityFrameworkCore` and synchronized against the shared GPay database.

## Run

1. Point `ConnectionStrings:Default` at a database with the `AppBankHub*` tables and the shared G-PAY tables used by the PRS poller. Schema migrations are not included in this repository.
2. Ensure the shared G-PAY database contains active PRS orders whose payer account is held at Absa. The poller does not require a separate PayShap marker or amount limit.
3. Set secrets (user-secrets / `GPAY_` env / `appsettings.Local.json`):
   - `AbsaCapi:*` credentials and certificate settings
   - `BankCallbacks:Banks:Absa:PaymentToken`
   - `GpayNotifications:Username` and `GpayNotifications:Password` when the SMTP relay requires authentication
4. `dotnet run --project src\GPay.Banking.HttpApi.Host`
5. Swagger: `/swagger` (OpenAPI JSON: `/swagger/v1/swagger.json`)
6. Full offline API reference: [`docs/Swagger-API-Reference.md`](docs/Swagger-API-Reference.md)

## Key APIs

| Method | Path | Auth |
|--------|------|------|
| `POST` | `/api/app/account-verification/verify` | JWT Bearer when enabled |
| `POST` | `/api/app/account-verification/submit-batch` | JWT Bearer when enabled |
| `GET` | `/api/app/account-verification/batch/{id}` | JWT Bearer when enabled |
| `GET` | `/api/app/account-verification/batch-records?batchId={id}` | JWT Bearer when enabled |
| `POST` | `/api/app/instant-payment/initiate` | JWT Bearer when enabled |
| `POST` | `/api/app/instant-payment/get-status` | JWT Bearer when enabled |
| `POST` | `/api/app/transaction-history/get` | JWT Bearer when enabled |
| `POST` | `/api/app/callback/process-payment?bank=Absa` | Bank Token + IP allow-list (no JWT) |

Set `AuthServer:RequireGpayAuth` to `false` to disable JWT. Callbacks still use bank token checks.

In Swagger UI use **Authorize** with a Bearer token for protected operations when auth is on. Callback body is Absa-native JSON.

## Config

Global settings live in `src\GPay.Banking.HttpApi.Host\appsettings.json`. Bank settings live in `appsettings.<BankCode>.json` beside it (for example, `appsettings.Absa.json`). The host loads an optional bank file for every `BankCode` at startup. Keep shared settings such as database, authentication, SMTP, and polling intervals in the global file; place bank-specific API, callback, and bank enablement settings in that bank's file. `appsettings.Local.json` and `GPAY_` environment variables can override these values.

Absa calls always use the configured CAPI endpoints; configure valid credentials and certificate settings before running payments or AVS checks. Tracked settings contain no bank or SMTP credentials; configure them through user-secrets, `appsettings.Local.json`, or `GPAY_` environment variables.

