# GPay Banking Ops (Angular)

IIS-ready Angular app for platform health and RabbitMQ queue depths.

## Development

```bash
npm install
npm start
```

Calls Orchestrator at `http://localhost:5100` (see `src/environments/environment.ts`).

## Production / IIS

```bash
npm run build
```

Output: `dist/gpay-banking-ops/browser`

1. Create an IIS site (or virtual application) pointing at the browser output folder.
2. Ensure **URL Rewrite** is installed — `web.config` in the output rewrites SPA routes to `index.html`.
3. Production `apiBaseUrl` is empty (same-origin). If the API is on another host, set it in `environment.prod.ts` before building.

### Same-site with Orchestrator

Copy `dist/gpay-banking-ops/browser/*` into Orchestrator `wwwroot` after publish. Orchestrator already serves static files and falls back to `index.html`.
