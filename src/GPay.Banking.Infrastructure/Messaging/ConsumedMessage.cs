namespace GPay.Banking.Infrastructure.Messaging;

/// <summary>
/// A consumed RabbitMQ message with routing metadata from AMQP properties.
/// </summary>
public sealed class ConsumedMessage<T>
{
    /// <summary>
    /// Deserialized message body.
    /// </summary>
    public required T Payload { get; init; }

    /// <summary>
    /// Reply address from the incoming request, when present.
    /// </summary>
    public string? ReplyTo { get; init; }

    /// <summary>
    /// Correlation identifier from AMQP properties, when present.
    /// </summary>
    public string? CorrelationId { get; init; }
}
