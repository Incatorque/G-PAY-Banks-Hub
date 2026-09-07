using GPay.Banking.Contracts.Messaging;

namespace GPay.Banking.Infrastructure.Messaging;

/// <summary>
/// Abstraction over RabbitMQ publish/consume and queue inspection.
/// </summary>
public interface IMessageBus
{
    /// <summary>
    /// Publishes a message to the specified queue.
    /// </summary>
    Task PublishAsync<T>(
        string queueName,
        T message,
        MessagePublishOptions? options = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Starts consuming messages from a queue.
    /// </summary>
    Task SubscribeAsync<T>(
        string queueName,
        Func<ConsumedMessage<T>, CancellationToken, Task> handler,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Consumes direct RPC replies on <see cref="QueueNames.DirectReplyTo"/>.
    /// </summary>
    Task SubscribeToDirectRepliesAsync(
        Func<ConsumedMessage<BankResponseMessage>, CancellationToken, Task> handler,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns ready message and consumer counts for a queue.
    /// </summary>
    Task<(uint MessageCount, uint ConsumerCount)> GetQueueStatsAsync(string queueName, CancellationToken cancellationToken = default);

    /// <summary>
    /// Ensures a durable queue exists.
    /// </summary>
    Task DeclareQueueAsync(string queueName, CancellationToken cancellationToken = default);

    /// <summary>
    /// Indicates whether the broker connection is open.
    /// </summary>
    bool IsConnected { get; }

    /// <summary>
    /// Indicates whether the direct RPC reply consumer and publish channel are ready.
    /// </summary>
    bool IsDirectReplyReady { get; }
}
