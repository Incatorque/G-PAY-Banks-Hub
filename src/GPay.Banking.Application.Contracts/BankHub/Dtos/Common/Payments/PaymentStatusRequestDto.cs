namespace GPay.Banking.BankHub;

/// <summary>
/// Request to query live instant-payment status from the bank.
/// </summary>
/// <remarks>
/// Provide a bank reference and, where required by that bank, its original message identifiers.
/// </remarks>
public class PaymentStatusRequestDto
{
    /// <summary>
    /// Target bank. Defaults to Absa for backwards compatibility.
    /// </summary>
    /// <example>Absa</example>
    public string Bank { get; set; } = "Absa";

    /// <summary>
    /// Bank or scheme transaction reference.
    /// </summary>
    public string? TransactionReference { get; set; }

    /// <summary>
    /// Bank API or message reference.
    /// </summary>
    public string? ApiReference { get; set; }

    /// <summary>Original bank message identifier when required by the bank status endpoint (Capitec).</summary>
    public string? BankMessageIdentification { get; set; }

    /// <summary>Original instruction UETR when required by the bank status endpoint (Capitec).</summary>
    public string? BankInstructionUetr { get; set; }
}
