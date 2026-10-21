# Legacy (pre-ABP) Banking hosts

These projects are **obsolete** after the in-process ABP migration:

- `GPay.Banking.Orchestrator` — HTTP façade + RabbitMQ routing
- `GPay.Banking.Absa` (worker) — MQ consumers
- `GPay.Banking.Infrastructure` — RabbitMQ bus / correlator
- `GPay.Banking.Contracts` — messaging envelopes (capability contracts moved to Domain)
- `GPay.Banking.Persistence` — removed GPayDev scaffold DbContext and models; re-scaffold when needed. The archived Orchestrator and Absa host projects still reference this project and will not build until it is restored.

Use `src\GPay.Banking.HttpApi.Host` + `src\GPay.Banking.Absa` (class library module) instead.

These projects are archived and are not part of the active solution. Re-scaffold persistence before building the archived hosts.
