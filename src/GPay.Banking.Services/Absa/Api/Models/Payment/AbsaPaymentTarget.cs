using System.Text.Json.Serialization;

namespace GPay.Banking.Services.Absa.Api.Models.Payment;

/// <summary>
/// Absa payment target (credit) account.
/// </summary>
public sealed class AbsaPaymentTarget
{
    [JsonPropertyName("StatementRef")]
    public required string StatementRef { get; init; }

    [JsonPropertyName("AccountType")]
    public int AccountType { get; init; }

    [JsonPropertyName("AccountNumber")]
    public required string AccountNumber { get; init; }

    [JsonPropertyName("BankBranchCode")]
    public required string BankBranchCode { get; init; }

    [JsonPropertyName("Name")]
    public required string Name { get; init; }

    [JsonPropertyName("IsTrustAccount")]
    public required string IsTrustAccount { get; init; }
}
