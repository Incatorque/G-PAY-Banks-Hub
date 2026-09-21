using System.Text.Json.Serialization;

namespace GPay.Banking.Services.Absa.Api.Models.Payment;

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
