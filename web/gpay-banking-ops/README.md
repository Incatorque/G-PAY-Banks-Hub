# GPay Banking Ops

Angular operations dashboard for Bank Hub health and audited bank API activity. It includes bank and outcome filters, paging, request metadata, redacted request/response payloads, status details, and response timing.

## Development

```bash
npm install
npm start
```

The development API URL defaults to `http://localhost:63340` and can be changed in `src/environments/environment.ts`. Enter a Bank Hub JWT in the dashboard access field to query protected activity. The token is held in session storage for the current browser tab.

The activity endpoint reads `BankHubApiCall` audit rows. New outgoing bank HTTP requests are captured by the Bank Hub audit handler, and sensitive payload fields are redacted before persistence.

## Build and deploy

```bash
npm run build
```

The production build is written to `dist/gpay-banking-ops/browser`. See [IIS deployment](IIS.md) for hosting requirements.
