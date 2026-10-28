using System.Text.Json.Serialization;

namespace GPay.Banking.Services.Absa.Api.Models.Payment;

/// <summary>
/// Absa Payment Status response.
/// </summary>
public sealed class AbsaPaymentStatusResponse : AbsaPaymentResult
{
    [JsonPropertyName("StatusList")]
    public List<AbsaPaymentStatusListItem>? StatusList { get; init; }

    /// <summary>
    /// True when ErrorList has items and StatusList is empty.
    /// </summary>
    [JsonIgnore]
    public override bool HasErrors =>
        ErrorList is { Count: > 0 } &&
        (StatusList is null || StatusList.Count == 0);
}
