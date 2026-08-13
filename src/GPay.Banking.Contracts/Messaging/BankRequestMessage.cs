using GPay.Banking.Contracts.Enums;

namespace GPay.Banking.Contracts.Messaging;

/// <summary>
/// Shared RabbitMQ request payload published to each bank request queue.
/// </summary>
public sealed class BankRequestMessage
{
    /// <summary>
    /// Correlation identifier spanning orchestrator and bank services.
    /// </summary>
    public required string CorrelationId { get; init; }

    /// <summary>
    /// Target bank.
    /// </summary>
    public required BankCode BankCode { get; init; }

    /// <summary>
    /// Capability to execute.
    /// </summary>
    public required BankOperation Operation { get; init; }

    /// <summary>
    /// UTC timestamp when the message was created.
    /// </summary>
    public DateTimeOffset CreatedAtUtc { get; init; } = DateTimeOffset.UtcNow;

    /// <summary>
    /// JSON-serialized GPay request DTO for <see cref="Operation"/>.
    /// </summary>
    public required string Payload { get; init; }
}
