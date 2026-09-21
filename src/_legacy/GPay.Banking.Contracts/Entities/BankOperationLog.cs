using GPay.Banking.Contracts.Enums;

namespace GPay.Banking.Contracts.Entities;

/// <summary>
/// Audit record for a bank capability operation flowing through the platform.
/// </summary>
public sealed class BankOperationLog : EntityBase
{
    /// <summary>
    /// Correlation identifier spanning orchestrator and bank services.
    /// </summary>
    public required string CorrelationId { get; set; }

    /// <summary>
    /// Target bank.
    /// </summary>
    public BankCode BankCode { get; set; }

    /// <summary>
    /// Capability that was executed.
    /// </summary>
    public BankOperation Operation { get; set; }

    /// <summary>
    /// Indicates whether the operation succeeded.
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// Serialized request payload (JSON).
    /// </summary>
    public string? RequestPayload { get; set; }

    /// <summary>
    /// Serialized response payload (JSON).
    /// </summary>
    public string? ResponsePayload { get; set; }

    /// <summary>
    /// Optional normalized error code.
    /// </summary>
    public string? ErrorCode { get; set; }

    /// <summary>
    /// Optional error message.
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// Service that wrote the log (e.g. Orchestrator, Absa).
    /// </summary>
    public string? SourceService { get; set; }
}
