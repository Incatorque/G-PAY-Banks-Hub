# GPay Banking API — Swagger / OpenAPI

Interactive docs: run `GPay.Banking.HttpApi.Host` and open **`/swagger`**.

OpenAPI JSON: **`/swagger/v1/swagger.json`**.

This document mirrors what Swagger exposes so callers can read offline.

---

## Authentication

| Surface | Auth |
|---------|------|
| AVS + Instant payments + Transaction history | **JWT Bearer** (`Authorization: Bearer {token}`). Audience `GPay`. |
| Payment callbacks | **Anonymous**. Shared bank `Token` in JSON body + optional IP/domain allow-list. |

### Permissions

| Permission | Used by |
|------------|---------|
| `Banking.Avs.Verify` | `POST .../account-verification/verify` |
| `Banking.Avs.Upload` | `POST .../account-verification/submit-batch` |
| `Banking.Avs.View` | Batch progress / records |
| `Banking.Payments.Initiate` | `POST .../instant-payment/initiate` |
| `Banking.Payments.View` | Status enquiry + get record |
| `Banking.TransactionHistory.View` | `POST .../transaction-history/get` |

Click **Authorize** in Swagger UI and paste a Bearer token for protected operations.

---

## Endpoints

ABP conventional controllers map AppServices to:

`/api/app/{service-kebab}/{action-kebab}`

### Account verification (AVS)

| Method | Path | Permission | Description |
|--------|------|------------|-------------|
| `POST` | `/api/app/account-verification/verify` | Verify | Single AVS enquiry; persists `BankHubAvsRecord`. |
| `POST` | `/api/app/account-verification/submit-batch` | Upload | Async batch (max `AvsBatch:MaxItems`, default 20 000). |
| `GET` | `/api/app/account-verification/batch/{id}` | View | Batch header / progress. |
| `GET` | `/api/app/account-verification/batch-records` | View | All records for `batchId` query. |

**Notes**

- Body includes `bank` (`Absa` / `Fnb` / `Nedbank`). Absa is the wired adapter today.
- Absa AVS has **no** bank webhook; pending non-Absa AVS is handled by adapter polling.
- Match fields: Yes/No/Unverified (or Y/N/U). See Absa AVS MIG v00.5 ValueList (`Initials Match`, `Account Type Matched`).

### Instant payments

| Method | Path | Permission | Description |
|--------|------|------------|-------------|
| `POST` | `/api/app/instant-payment/initiate` | Initiate | Submit PayShap/RTC payment; persists `BankHubPaymentRecord`. |
| `POST` | `/api/app/instant-payment/get-status` | View | Live bank status; updates local record when found. |
| `GET` | `/api/app/instant-payment/{id}` | View | Local record only (no bank call). |

**Rails:** `RPP` (PayShap, default), `IIP` (RTC), `PAAF`.

**Status values:** Submitted, Completed, Failed, Duplicate, Pending, Error.

**References (Absa):** `apiReference` ≈ Correlations type 3; `transactionReference` ≈ type 4.

### Transaction history

| Method | Path | Permission | Description |
|--------|------|------------|-------------|
| `POST` | `/api/app/transaction-history/get` | View | Account statement lines for an inclusive date range. |

**Request:** `bank`, `accountNumber`, `fromDate`, `toDate`, optional `pageSize`.

**Notes:** Uses domain `IStatementService` / `ITransactionHistoryService`. Absa adapter is currently a stub (empty list) until CAPI statement is wired.

### Callbacks

| Method | Path | Auth | Description |
|--------|------|------|-------------|
| `POST` | `/api/app/callback/process-payment?bank=Absa` | Token + allow-list | Absa payment status webhook. |

**Body:** bank-native Absa PaymentCallback JSON (not a GPay DTO schema). Include the registered `Token`.

**Retries:** Absa pushes on status change, then retries “several” times on failure; schedule is not published in the MIG. Handler is idempotent. After exhaustion Absa emails `SupportEmail`. Fallback: get-status.

**Register with Absa:** `POST /api/PaymentCallback/Register` → Uri = this callback URL, Token, SupportEmail. HTTPS port **443** only.

---

## Example payloads

### Verify (AVS)

```json
{
  "bank": "Absa",
  "accountNumber": "4049813068",
  "branchCode": "632005",
  "issuingBankCode": "000016",
  "identityNumber": "8001015009087",
  "identityType": "SAID",
  "initials": "J",
  "lastName": "DOE",
  "reference": "AVS-001"
}
```

### Initiate payment

```json
{
  "bank": "Absa",
  "fromAccountNumber": "4049813068",
  "toAccountNumber": "51000716346",
  "toBranchCode": "678910",
  "amount": 1.00,
  "currency": "ZAR",
  "reference": "PAY20260922-001",
  "beneficiaryName": "ACME (PTY) LTD",
  "paymentRail": "RPP"
}
```

### Get status

```json
{
  "bank": "Absa",
  "transactionReference": "PAY20260922-001",
  "apiReference": "SIM-API-20260922120000"
}
```

---

## How Swagger is built

| Piece | Location |
|-------|----------|
| OpenAPI info, JWT scheme, XML include | `HttpApi.Host/Swagger/BankingSwaggerExtensions.cs` |
| Operation + schema text | XML comments on Application.Contracts + Application |
| XML generation | `GenerateDocumentationFile` on Contracts, Application, Host |
| XML copy into Host output | `CopyBankingXmlDocumentation` target on Host csproj |

Re-run the Host after doc changes so XML is regenerated and copied.

---

## Out of scope of this Swagger

- Absa CAPI wire models (use Absa Postman / MIG under `GPay.Banking` Documentation).
- Legacy Orchestrator (`src/_legacy`) — obsolete; do not register its URLs with banks.
- Balance / statement / notification AppServices (domain ports exist; not exposed on ABP HTTP yet).
