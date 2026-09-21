using System.ComponentModel.DataAnnotations;

namespace GPay.Banking.BankHub;

public class VerifyAccountRequestDto
{
    public string Bank { get; set; } = "Absa";
    [Required] public string AccountNumber { get; set; } = string.Empty;
    [Required] public string BranchCode { get; set; } = string.Empty;
    public string? IssuingBankCode { get; set; }
    public string? IdentityNumber { get; set; }
    public string? IdentityType { get; set; }
    public string? AccountHolderName { get; set; }
    public string? Initials { get; set; }
    public string? LastName { get; set; }
    public string? AccountType { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Reference { get; set; }
    public Guid? ApiClientId { get; set; }
}
