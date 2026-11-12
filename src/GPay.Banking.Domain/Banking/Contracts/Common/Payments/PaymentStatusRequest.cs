namespace GPay.Banking.Domain.Dtos;


/// <summary>
/// Bank-neutral request to query an instant payment status.
/// </summary>
public sealed class PaymentStatusRequest
{
    /// <summary>Internal GPay order identifier used to associate API audit rows.</summary>
    public Guid? GpayOrderId { get; init; }

    /// <summary>Original bank request message identifier required by some bank status enquiries.</summary>
    public string? BankMessageIdentification { get; init; }

    /// <summary>Original instruction UETR required by some bank status enquiries.</summary>
    public string? BankInstructionUetr { get; init; }

    /// <summary>
    /// Bank transaction reference, when available.
    /// </summary>
    public string? TransactionReference { get; init; }

    /// <summary>
    /// Bank API reference, used when the transaction reference is unavailable.
    /// </summary>
    public string? ApiReference { get; init; }
}




