using System.Text.Json.Serialization;

namespace GPay.Banking.Services.Absa.Api.Models.Avs;

/// <summary>
/// Absa error list item.
/// </summary>
public sealed class AbsaAvsErrorItem
{
    [JsonPropertyName("CorrelationId")]
    public string? CorrelationId { get; init; }

    [JsonPropertyName("Code")]
    public string? Code { get; init; }

    [JsonPropertyName("Description")]
    public string? Description { get; init; }

    [JsonPropertyName("Group")]
    public int? Group { get; init; }

    [JsonPropertyName("Severity")]
    public int? Severity { get; init; }

    [JsonPropertyName("Tag")]
    public string? Tag { get; init; }

    [JsonPropertyName("DetailedMessages")]
    public List<AbsaAvsDetailedMessage>? DetailedMessages { get; init; }
}
