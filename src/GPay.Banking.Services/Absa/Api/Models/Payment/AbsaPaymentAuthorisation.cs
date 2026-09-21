using System.Text.Json.Serialization;

namespace GPay.Banking.Services.Absa.Api.Models.Payment;

/// <summary>
/// Absa payment authorisation block.
/// </summary>
public sealed class AbsaPaymentAuthorisation
{
    [JsonPropertyName("SubmittingEntityName")]
    public required string SubmittingEntityName { get; init; }

    [JsonPropertyName("SubsidiaryEntityName")]
    public required string SubsidiaryEntityName { get; init; }

    [JsonPropertyName("Indicator")]
    public int Indicator { get; init; }
}
