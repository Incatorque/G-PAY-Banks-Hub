using System.Text.Json.Serialization;

namespace GPay.Banking.Services.Absa.Api.Models.Payment;

/// <summary>
/// Proof-of-payment notification. Absa requires this object on initiate.
/// <c>Indicator</c> is <c>T</c> or <c>F</c>.
/// </summary>
public sealed class AbsaPaymentProofOfPayment
{
    [JsonPropertyName("EmailAddress")]
    public string? EmailAddress { get; init; }

    [JsonPropertyName("MobileNumber")]
    public string? MobileNumber { get; init; }

    /// <summary><c>T</c> send PoP, <c>F</c> do not.</summary>
    [JsonPropertyName("Indicator")]
    public string Indicator { get; init; } = "F";
}
