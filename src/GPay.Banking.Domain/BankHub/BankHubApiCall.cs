using Volo.Abp.Domain.Entities.Auditing;

namespace GPay.Banking.BankHub;

/// <summary>
/// Stored copy of an outbound Absa CAPI call or an inbound payment callback.
/// </summary>
public class BankHubApiCall : CreationAuditedAggregateRoot<Guid>
{
    public string BankCode { get; set; } = "Absa";

    public string Direction { get; set; } = "Outbound";

    public string Operation { get; set; } = string.Empty;

    public string? HttpMethod { get; set; }

    public string? Path { get; set; }

    public int? HttpStatus { get; set; }

    public string? CorrelationId { get; set; }

    public string? RequestJson { get; set; }

    public string? ResponseJson { get; set; }

    public long? DurationMs { get; set; }

    public bool Success { get; set; }

    public string? ErrorCode { get; set; }
}
