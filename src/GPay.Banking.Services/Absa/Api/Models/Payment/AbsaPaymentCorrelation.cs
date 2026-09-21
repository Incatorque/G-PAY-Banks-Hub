using System.Text.Json.Serialization;

namespace GPay.Banking.Services.Absa.Api.Models.Payment;

/// <summary>
/// Absa payment correlation item.
/// </summary>
public sealed class AbsaPaymentCorrelation
{
    /// <summary>
    /// 1=source stmt, 2=target stmt, 3=ApiRef, 4=TransactionRef.
    /// </summary>
    [JsonPropertyName("Type")]
    public int Type { get; init; }

    [JsonPropertyName("Value")]
    public string? Value { get; init; }
}
