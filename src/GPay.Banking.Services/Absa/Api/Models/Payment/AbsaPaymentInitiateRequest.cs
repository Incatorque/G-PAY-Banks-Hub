using System.Text.Json.Serialization;

namespace GPay.Banking.Services.Absa.Api.Models.Payment;

/// <summary>
/// Absa Payment Initiate request (MIG – Payment API v1.8).
/// </summary>
public sealed class AbsaPaymentInitiateRequest
{
    /// <summary>
    /// Authorisation block.
    /// </summary>
    [JsonPropertyName("Authorisation")]
    public required AbsaPaymentAuthorisation Authorisation { get; init; }

    /// <summary>
    /// Debit / source account.
    /// </summary>
    [JsonPropertyName("Source")]
    public required AbsaPaymentSource Source { get; init; }

    /// <summary>
    /// Credit / target account.
    /// </summary>
    [JsonPropertyName("Target")]
    public required AbsaPaymentTarget Target { get; init; }

    /// <summary>
    /// Economics / payment instruction.
    /// </summary>
    [JsonPropertyName("Economics")]
    public required AbsaPaymentEconomics Economics { get; init; }

    /// <summary>
    /// Optional proof of payment notification.
    /// </summary>
    [JsonPropertyName("ProofOfPayment")]
    public AbsaPaymentProofOfPayment? ProofOfPayment { get; init; }

    /// <summary>
    /// Optional callback registration for this payment.
    /// </summary>
    [JsonPropertyName("Callback")]
    public AbsaPaymentCallback? Callback { get; init; }

    /// <summary>
    /// Session from <c>/api/User/Authenticate</c>.
    /// </summary>
    [JsonPropertyName("Session")]
    public string? Session { get; set; }
}
