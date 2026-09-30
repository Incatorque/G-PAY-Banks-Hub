namespace GPay.Banking.Domain;

/// <summary>
/// Persists every Absa CAPI call and inbound payment callback.
/// </summary>
public interface IBankApiCallAuditor
{
    /// <summary>
    /// Stores one API call. Failures to persist must not fail the bank call.
    /// </summary>
    Task RecordAsync(BankApiCallAuditEntry entry, CancellationToken cancellationToken = default);
}

/// <summary>
/// One audited bank API call.
/// </summary>
public sealed class BankApiCallAuditEntry
{
    public string BankCode { get; init; } = "Absa";

    /// <summary><c>Outbound</c> or <c>Inbound</c>.</summary>
    public string Direction { get; init; } = "Outbound";

    public string Operation { get; init; } = string.Empty;

    public string? HttpMethod { get; init; }

    public string? Path { get; init; }

    public int? HttpStatus { get; init; }

    public string? CorrelationId { get; init; }

    public string? RequestJson { get; init; }

    public string? ResponseJson { get; init; }

    public long? DurationMs { get; init; }

    public bool Success { get; init; }

    public string? ErrorCode { get; init; }
}
