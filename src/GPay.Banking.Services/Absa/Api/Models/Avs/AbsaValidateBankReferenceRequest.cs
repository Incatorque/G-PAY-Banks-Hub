using System.Text.Json.Serialization;

namespace GPay.Banking.Services.Absa.Api.Models.Avs;

/// <summary>
/// Absa Validate Bank Reference request (MIG §6.4.2) for non-Absa accounts.
/// </summary>
public sealed class AbsaValidateBankReferenceRequest
{
    [JsonPropertyName("ReferenceNumber")]
    public required string ReferenceNumber { get; init; }

    [JsonPropertyName("CapiCode")]
    public required string CapiCode { get; init; }

    [JsonPropertyName("Session")]
    public required string Session { get; init; }
}
