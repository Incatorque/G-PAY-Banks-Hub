using GPay.Banking.Contracts.Enums;

namespace GPay.Banking.Contracts.Health;

/// <summary>
/// Health snapshot for a bank or platform service.
/// </summary>
public sealed class ServiceHealthInfo
{
    /// <summary>
    /// Service name (e.g. Orchestrator, Absa).
    /// </summary>
    public required string ServiceName { get; init; }

    /// <summary>
    /// Associated bank code when applicable.
    /// </summary>
    public BankCode? BankCode { get; init; }

    /// <summary>
    /// Aggregated status.
    /// </summary>
    public ServiceHealthStatus Status { get; init; }

    /// <summary>
    /// True when the service health endpoint responded.
    /// </summary>
    public bool IsResponding { get; init; }

    /// <summary>
    /// Human-readable status detail.
    /// </summary>
    public string? Detail { get; init; }

    /// <summary>
    /// Related queue health entries.
    /// </summary>
    public IReadOnlyList<QueueHealthInfo> Queues { get; init; } = [];
}
