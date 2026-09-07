namespace GPay.Banking.Infrastructure.Messaging;

/// <summary>
/// RabbitMQ publish properties used for request/reply routing.
/// </summary>
public sealed class MessagePublishOptions
{
    /// <summary>
    /// Reply address (typically <c>amq.rabbitmq.reply-to</c>) for RPC responses.
    /// </summary>
    public string? ReplyTo { get; init; }

    /// <summary>
    /// Correlation identifier matching the originating request.
    /// </summary>
    public string? CorrelationId { get; init; }
}
