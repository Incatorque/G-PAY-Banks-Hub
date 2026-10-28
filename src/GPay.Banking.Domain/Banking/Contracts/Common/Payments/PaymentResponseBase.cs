namespace GPay.Banking.Domain.Dtos;

/// <summary>Fields shared by payment initiation and status responses.</summary>
public abstract class PaymentResponseBase : IPaymentResponse
{
    /// <summary>Bank or scheme transaction identifier (legacy alias of <see cref="TransactionReference"/>).</summary>
    public string? TransactionId { get; init; }

    /// <summary>Payment status (for example, Submitted, Completed, or Failed).</summary>
    public required string Status { get; init; }

    /// <summary>Echo of the client payment reference when known.</summary>
    public string? Reference { get; init; }

    /// <summary>Amount processed when known.</summary>
    public decimal? Amount { get; init; }

    /// <summary>Currency of the processed amount.</summary>
    public string? Currency { get; init; }

    /// <summary>Absa Correlations type 3 (ApiRef).</summary>
    public string? ApiReference { get; init; }

    /// <summary>Absa Correlations type 4 (TransactionRef).</summary>
    public string? TransactionReference { get; init; }

    /// <summary>Absa Correlations type 1 (SourceStatementRef).</summary>
    public string? SourceStatementRef { get; init; }

    /// <summary>Absa Correlations type 2 (TargetStatementRef).</summary>
    public string? TargetStatementRef { get; init; }

    /// <summary>Raw Absa status code.</summary>
    public int? BankStatusCode { get; init; }

    /// <summary>Payment rail used (RPP / IIP / PAAF).</summary>
    public string? PaymentRail { get; init; }

    /// <summary>Human-readable result description.</summary>
    public string? ResultDescription { get; init; }

    /// <summary>Normalized or bank error code when failed.</summary>
    public string? ErrorCode { get; init; }

    /// <summary>Normalized status label: Submitted, Completed, Failed, Duplicate, or Pending.</summary>
    public string? RawStatusLabel { get; init; }
}
