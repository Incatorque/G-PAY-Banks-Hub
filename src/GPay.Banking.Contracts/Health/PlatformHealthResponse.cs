using GPay.Banking.Contracts.Enums;

namespace GPay.Banking.Contracts.Health;

/// <summary>
/// Aggregated platform health response for GPay and ops UI.
/// </summary>
public sealed class PlatformHealthResponse
{
    /// <summary>
    /// Overall platform status (worst of all services).
    /// </summary>
    public ServiceHealthStatus OverallStatus { get; init; }

    /// <summary>
    /// UTC timestamp of the health check.
    /// </summary>
    public DateTimeOffset CheckedAtUtc { get; init; } = DateTimeOffset.UtcNow;

    /// <summary>
    /// Per-service health entries.
    /// </summary>
    public IReadOnlyList<ServiceHealthInfo> Services { get; init; } = [];
}
