using GPay.Banking.Contracts.Enums;

namespace GPay.Banking.Contracts.Messaging;

/// <summary>
/// Shared RabbitMQ response payload published back to the orchestrator.
/// </summary>
public sealed class BankResponseMessage
{
    /// <summary>
    /// Correlation identifier matching the originating request.
    /// </summary>
    public required string CorrelationId { get; init; }

    /// <summary>
    /// Source bank.
    /// </summary>
    public required BankCode BankCode { get; init; }

    /// <summary>
    /// Capability that was executed.
    /// </summary>
    public required BankOperation Operation { get; init; }

    /// <summary>
    /// Indicates whether the bank operation succeeded.
    /// </summary>
    public bool Success { get; init; }

    /// <summary>
    /// JSON-serialized <c>ApiResult&lt;T&gt;</c> for the operation.
    /// </summary>
    public required string Payload { get; init; }

    /// <summary>
    /// UTC timestamp when the response was created.
    /// </summary>
    public DateTimeOffset CreatedAtUtc { get; init; } = DateTimeOffset.UtcNow;
}
