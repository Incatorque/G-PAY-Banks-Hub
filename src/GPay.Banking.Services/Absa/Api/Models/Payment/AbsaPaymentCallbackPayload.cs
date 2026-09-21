using System.Text.Json.Serialization;

namespace GPay.Banking.Services.Absa.Api.Models.Payment;

/// <summary>
/// Inbound Absa payment callback payload (MIG – Payment API v1.8).
/// </summary>
public sealed class AbsaPaymentCallbackPayload
{
    /// <summary>
    /// Shared secret token registered with Absa.
    /// </summary>
    [JsonPropertyName("Token")]
    public string? Token { get; init; }

    /// <summary>
    /// Payment type / rail: RPP, IIP, or PAAF.
    /// </summary>
    [JsonPropertyName("Type")]
    public string? Type { get; init; }

    /// <summary>
    /// Payment status from Absa.
    /// </summary>
    [JsonPropertyName("PaymentStatus")]
    public object? PaymentStatus { get; init; }

    [JsonPropertyName("Correlations")]
    public List<AbsaPaymentCorrelation>? Correlations { get; init; }

    [JsonPropertyName("ErrorList")]
    public List<AbsaPaymentErrorItem>? ErrorList { get; init; }

    /// <summary>
    /// Optional amount echo.
    /// </summary>
    [JsonPropertyName("Amount")]
    public string? Amount { get; init; }

    /// <summary>
    /// Optional currency echo.
    /// </summary>
    [JsonPropertyName("CurrencyCode")]
    public string? CurrencyCode { get; init; }
}
