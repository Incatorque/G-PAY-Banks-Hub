using System.Text.Json.Serialization;

namespace GPay.Banking.Services.Absa.Api.Models.Payment;

/// <summary>
/// Absa payment error list item.
/// </summary>
public sealed class AbsaPaymentErrorItem
{
    [JsonPropertyName("Code")]
    public string? Code { get; init; }

    [JsonPropertyName("Description")]
    public string? Description { get; init; }

    [JsonPropertyName("Message")]
    public string? Message { get; init; }

    [JsonPropertyName("Severity")]
    public int? Severity { get; init; }
}
