namespace GPay.Banking.Infrastructure.Correlation;

/// <summary>
/// In-memory request/response correlator used by the orchestrator base.
/// Replace with a distributed store (e.g. Redis) for multi-instance deployments.
/// </summary>
public interface IResponseCorrelator
{
    /// <summary>
    /// Registers an outstanding correlation and returns a task completed when the response arrives.
    /// </summary>
    TaskCompletionSource<string> Register(string correlationId, TimeSpan timeout);

    /// <summary>
    /// Completes a pending correlation with the JSON payload.
    /// </summary>
    bool TryComplete(string correlationId, string payload);

    /// <summary>
    /// Removes a correlation without completing it.
    /// </summary>
    void Cancel(string correlationId);
}
