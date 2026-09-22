namespace GPay.Banking.BankHub;

/// <summary>
/// Request to query live instant-payment status from the bank.
/// </summary>
/// <remarks>
/// Provide at least one of <see cref="TransactionReference"/> or <see cref="ApiReference"/>.
/// Absa: TransactionReference ≈ Correlations type 4; ApiReference ≈ Correlations type 3.
/// </remarks>
public class PaymentStatusRequestDto
{
    /// <summary>
    /// Target bank: <c>Absa</c>, <c>Fnb</c>, or <c>Nedbank</c>. Defaults to <c>Absa</c>.
    /// </summary>
    /// <example>Absa</example>
    public string Bank { get; set; } = "Absa";

    /// <summary>
    /// Bank / scheme transaction reference (Absa Correlations type 4 — TransactionRef).
    /// </summary>
    public string? TransactionReference { get; set; }

    /// <summary>
    /// Bank API reference (Absa Correlations type 3 — ApiRef), e.g. <c>SIM-API-...</c>.
    /// </summary>
    public string? ApiReference { get; set; }
}
