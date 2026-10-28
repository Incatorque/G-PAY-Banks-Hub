# GPay Banking Ops deployment

Build the app with `npm run build`. The deployable files are in `dist/gpay-banking-ops/browser`.

1. Point the IIS site at the `browser` output directory.
2. Install IIS URL Rewrite so `public/web.config` can route SPA paths to `index.html`.
3. The production API base URL is empty, so the UI calls `/health` on its own origin. If the Banking API is hosted separately, set `apiBaseUrl` in `src/environments/environment.prod.ts` before building.

For local development, `npm start` serves the UI at `http://localhost:4200` and uses the Banking API at `http://localhost:63340` by default. Update `src/environments/environment.ts` if the API uses a different address.
