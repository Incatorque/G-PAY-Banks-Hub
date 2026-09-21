using System.Text.Json.Serialization;

namespace GPay.Banking.Services.Absa.Api.Models.Payment;

/// <summary>
/// Absa payment economics block. Amount is a string per MIG.
/// </summary>
public sealed class AbsaPaymentEconomics
{
    /// <summary>
    /// RPP = PayShap, IIP = RTC, PAAF = other.
    /// </summary>
    [JsonPropertyName("Indicator")]
    public required string Indicator { get; init; }

    [JsonPropertyName("CurrencyCode")]
    public required string CurrencyCode { get; init; }

    /// <summary>
    /// Amount as string (MIG).
    /// </summary>
    [JsonPropertyName("Amount")]
    public required string Amount { get; init; }

    [JsonPropertyName("PaymentDate")]
    public required string PaymentDate { get; init; }

    [JsonPropertyName("TransactionRef")]
    public required string TransactionRef { get; init; }
}
