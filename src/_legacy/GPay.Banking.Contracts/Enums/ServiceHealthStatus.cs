namespace GPay.Banking.Contracts.Enums;

/// <summary>
/// Aggregated health severity for a bank or platform service.
/// </summary>
public enum ServiceHealthStatus
{
    /// <summary>Service is reachable and queues are within thresholds.</summary>
    Healthy = 0,

    /// <summary>Service is reachable but queues are clogged or degraded.</summary>
    Unhealthy = 1,

    /// <summary>Service is not responding or unreachable.</summary>
    Critical = 2
}
