using System.Text.Json.Serialization;

namespace GPay.Banking.Absa.Models.Payment;

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

/// <summary>
/// Absa PaymentCallback Register request.
/// </summary>
public sealed class AbsaPaymentCallbackRegisterRequest
{
    [JsonPropertyName("Uri")]
    public required string Uri { get; init; }

    [JsonPropertyName("Token")]
    public required string Token { get; init; }

    [JsonPropertyName("SupportEmail")]
    public string? SupportEmail { get; init; }

    [JsonPropertyName("Session")]
    public string? Session { get; set; }
}

/// <summary>
/// Absa PaymentCallback Register response.
/// </summary>
public sealed class AbsaPaymentCallbackRegisterResponse
{
    [JsonPropertyName("Status")]
    public object? Status { get; init; }

    [JsonPropertyName("ErrorList")]
    public List<AbsaPaymentErrorItem>? ErrorList { get; init; }

    [JsonPropertyName("Session")]
    public string? Session { get; init; }

    /// <summary>
    /// True when ErrorList has items.
    /// </summary>
    [JsonIgnore]
    public bool HasErrors => ErrorList is { Count: > 0 };
}
