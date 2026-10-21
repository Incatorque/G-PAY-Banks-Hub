namespace GPay.Banking.Domain.Dtos;


using GPay.Banking;

/// <summary>
/// Request to query instant payment status by Absa transaction or API reference.
/// </summary>
public sealed class PaymentStatusRequest
{
    /// <summary>Internal GPay order identifier used to associate API audit rows.</summary>
    public Guid? GpayOrderId { get; init; }

    /// <summary>
    /// Absa Correlations type 4 (TransactionRef). Preferred when available.
    /// </summary>
    public string? TransactionReference { get; init; }

    /// <summary>
    /// Absa Correlations type 3 (ApiRef). Used when TransactionReference is omitted.
    /// </summary>
    public string? ApiReference { get; init; }
}




