using Volo.Abp.Domain.Entities.Auditing;

namespace GPay.Banking.BankHub;

/// <summary>
/// Stored copy of an outbound bank API call or an inbound payment callback.
/// </summary>
public class BankHubApiCall : CreationAuditedAggregateRoot<Guid>
{
    /// <summary>Links payment API audit rows to the originating GPay order.</summary>
    public Guid? GpayOrderId { get; set; }

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
