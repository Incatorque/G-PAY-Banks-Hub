using GPay.Banking.Contracts.Enums;

namespace GPay.Banking.Contracts.Health;

/// <summary>
/// Health snapshot for a single queue.
/// </summary>
public sealed class QueueHealthInfo
{
    /// <summary>
    /// Queue name.
    /// </summary>
    public required string QueueName { get; init; }

    /// <summary>
    /// Ready message count.
    /// </summary>
    public long MessageCount { get; init; }

    /// <summary>
    /// Consumer count attached to the queue.
    /// </summary>
    public int ConsumerCount { get; init; }

    /// <summary>
    /// True when message count exceeds the configured clog threshold.
    /// </summary>
    public bool IsClogged { get; init; }
}
