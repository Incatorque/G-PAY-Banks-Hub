namespace GPay.Banking.BankHub;

public class PaymentStatusRequestDto
{
    /// <summary>Target bank (Absa, Fnb, Nedbank). Defaults to Absa when omitted.</summary>
    public string Bank { get; set; } = "Absa";

    public string? TransactionReference { get; set; }

    public string? ApiReference { get; set; }
}
