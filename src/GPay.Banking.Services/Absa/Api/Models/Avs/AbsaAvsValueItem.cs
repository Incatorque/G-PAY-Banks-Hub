using System.Text.Json.Serialization;

namespace GPay.Banking.Services.Absa.Api.Models.Avs;

/// <summary>
/// Matching-criteria item in <see cref="AbsaAvsResponse.ValueList"/>.
/// </summary>
public sealed class AbsaAvsValueItem
{
    [JsonPropertyName("Key")]
    public string? Key { get; init; }

    [JsonPropertyName("Value")]
    public string? Value { get; init; }
}
