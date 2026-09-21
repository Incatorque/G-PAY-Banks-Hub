using System.Text.Json.Serialization;

namespace GPay.Banking.Services.Absa.Api.Models.Payment;

/// <summary>
/// Optional payment callback registration on initiate.
/// </summary>
public sealed class AbsaPaymentCallback
{
    [JsonPropertyName("Uri")]
    public required string Uri { get; init; }

    [JsonPropertyName("Token")]
    public required string Token { get; init; }

    [JsonPropertyName("SupportEmail")]
    public string? SupportEmail { get; init; }
}
