using System.Text.Json.Serialization;

namespace GPay.Banking.Services.Absa.Api.Models.Avs;

/// <summary>
/// Absa detailed error message.
/// </summary>
public sealed class AbsaAvsDetailedMessage
{
    [JsonPropertyName("Code")]
    public string? Code { get; init; }

    [JsonPropertyName("Message")]
    public string? Message { get; init; }

    [JsonPropertyName("Severity")]
    public int? Severity { get; init; }

    [JsonPropertyName("ErrorCorellationId")]
    public string? ErrorCorrelationId { get; init; }
}
