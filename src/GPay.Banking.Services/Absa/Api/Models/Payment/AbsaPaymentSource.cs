using System.Text.Json.Serialization;

namespace GPay.Banking.Services.Absa.Api.Models.Payment;

/// <summary>
/// Absa payment source (debit) account.
/// </summary>
public sealed class AbsaPaymentSource
{
    [JsonPropertyName("StatementRef")]
    public required string StatementRef { get; init; }

    [JsonPropertyName("ShortName")]
    public required string ShortName { get; init; }

    [JsonPropertyName("AccountType")]
    public int AccountType { get; init; }

    [JsonPropertyName("AccountNumber")]
    public required string AccountNumber { get; init; }
}
