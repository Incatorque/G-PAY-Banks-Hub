namespace GPay.Banking.Domain.Dtos;

/// <summary>Fields shared by payment initiation and status responses.</summary>
public abstract class PaymentResponseBase : IPaymentResponse
{
    /// <summary>Bank adapter that produced the response.</summary>
    public string? BankCode { get; init; }

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

    /// <summary>Bank API or message reference.</summary>
    public string? ApiReference { get; init; }

    /// <summary>Bank or scheme transaction reference.</summary>
    public string? TransactionReference { get; init; }

    /// <summary>Source statement reference when supplied by the bank.</summary>
    public string? SourceStatementRef { get; init; }

    /// <summary>Target statement reference when supplied by the bank.</summary>
    public string? TargetStatementRef { get; init; }

    /// <summary>Bank-native request message identifier required for subsequent status enquiries.</summary>
    public string? BankMessageIdentification { get; init; }

    /// <summary>Bank-native instruction UETR required for subsequent status enquiries.</summary>
    public string? BankInstructionUetr { get; init; }

    /// <summary>Raw bank-native status code.</summary>
    public int? BankStatusCode { get; init; }

    /// <summary>Bank-specific payment rail used.</summary>
    public string? PaymentRail { get; init; }

    /// <summary>Human-readable result description.</summary>
    public string? ResultDescription { get; init; }

    /// <summary>Normalized or bank error code when failed.</summary>
    public string? ErrorCode { get; init; }

    /// <summary>Normalized status label: Submitted, Completed, Failed, Duplicate, or Pending.</summary>
    public string? RawStatusLabel { get; init; }
}
