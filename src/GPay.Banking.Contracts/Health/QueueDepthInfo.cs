namespace GPay.Banking.Contracts.Health;

/// <summary>
/// Queue depth entry returned by the queues monitoring endpoint.
/// </summary>
public sealed class QueueDepthInfo
{
    /// <summary>
    /// Queue name.
    /// </summary>
    public required string QueueName { get; init; }

    /// <summary>
    /// Number of ready messages.
    /// </summary>
    public long MessageCount { get; init; }

    /// <summary>
    /// Number of active consumers.
    /// </summary>
    public int ConsumerCount { get; init; }

    /// <summary>
    /// Optional bank association label.
    /// </summary>
    public string? Bank { get; init; }
}
