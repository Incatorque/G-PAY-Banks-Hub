using System.Text.Json.Serialization;

namespace GPay.Banking.Services.Absa.Api.Models.Payment;

/// <summary>
/// Absa Payment Status response.
/// </summary>
public sealed class AbsaPaymentStatusResponse
{
    [JsonPropertyName("StatusList")]
    public List<AbsaPaymentStatusListItem>? StatusList { get; init; }

    [JsonPropertyName("ErrorList")]
    public List<AbsaPaymentErrorItem>? ErrorList { get; init; }

    [JsonPropertyName("Session")]
    public string? Session { get; init; }

    /// <summary>
    /// True when ErrorList has items and StatusList is empty.
    /// </summary>
    [JsonIgnore]
    public bool HasErrors =>
        ErrorList is { Count: > 0 } &&
        (StatusList is null || StatusList.Count == 0);
}
