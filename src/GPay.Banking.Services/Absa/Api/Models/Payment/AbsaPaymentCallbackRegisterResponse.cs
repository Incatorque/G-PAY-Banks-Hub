using System.Text.Json.Serialization;

namespace GPay.Banking.Services.Absa.Api.Models.Payment;

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
