using System.Text.Json.Serialization;

namespace GPay.Banking.Services.Absa.Api.Models.Payment;

/// <summary>
/// Absa PaymentCallback UnRegister request (Payment API v1.8 §9.3).
/// </summary>
public sealed class AbsaPaymentCallbackUnregisterRequest
{
    [JsonPropertyName("Uri")]
    public required string Uri { get; init; }

    [JsonPropertyName("Session")]
    public string? Session { get; set; }
}
