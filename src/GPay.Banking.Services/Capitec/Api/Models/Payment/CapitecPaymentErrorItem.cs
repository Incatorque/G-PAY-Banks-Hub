using System.Text.Json.Serialization;

namespace GPay.Banking.Services.Capitec.Api.Models.Payment;

/// <summary>
/// Capitec  payment error list item.
/// </summary>
public sealed class CapitecPaymentErrorItem
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
