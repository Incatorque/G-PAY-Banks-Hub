using System.ComponentModel.DataAnnotations;

namespace GPay.Banking.BankHub;

public class InitiatePaymentRequestDto
{
    /// <summary>Target bank (Absa, Fnb, Nedbank). Defaults to Absa when omitted.</summary>
    public string Bank { get; set; } = "Absa";

    [Required] public string FromAccountNumber { get; set; } = string.Empty;
    [Required] public string ToAccountNumber { get; set; } = string.Empty;
    public string? ToBranchCode { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "ZAR";
    [Required] public string Reference { get; set; } = string.Empty;
    public string? BeneficiaryName { get; set; }
    public string PaymentRail { get; set; } = "RPP";
}
