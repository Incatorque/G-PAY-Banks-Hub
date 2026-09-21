# Legacy (pre-ABP) Banking hosts

These projects are **obsolete** after the in-process ABP migration:

- `GPay.Banking.Orchestrator` — HTTP façade + RabbitMQ routing
- `GPay.Banking.Absa` (worker) — MQ consumers
- `GPay.Banking.Infrastructure` — RabbitMQ bus / correlator
- `GPay.Banking.Contracts` — messaging envelopes (capability contracts moved to Domain)
- `GPay.Banking.Persistence` — GPayDev scaffold DbContext

Use `src\GPay.Banking.HttpApi.Host` + `src\GPay.Banking.Absa` (class library module) instead.

Do not delete until UAT smoke on the ABP host with Absa simulator/live is confirmed.
