using System.Text.Json.Serialization;

namespace GPay.Banking.Services.Absa.Api.Models.Payment;

/// <summary>
/// Optional proof-of-payment notification.
/// </summary>
public sealed class AbsaPaymentProofOfPayment
{
    [JsonPropertyName("EmailAddress")]
    public string? EmailAddress { get; init; }

    [JsonPropertyName("MobileNumber")]
    public string? MobileNumber { get; init; }

    [JsonPropertyName("Indicator")]
    public int? Indicator { get; init; }
}
