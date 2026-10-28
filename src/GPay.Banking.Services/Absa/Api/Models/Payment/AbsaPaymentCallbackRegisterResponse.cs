using System.Text.Json.Serialization;

namespace GPay.Banking.Services.Absa.Api.Models.Payment;

/// <summary>
/// Absa PaymentCallback Register / UnRegister response (Payment API v1.8 §9.2–9.3).
/// </summary>
public sealed class AbsaPaymentCallbackRegisterResponse : AbsaPaymentResult
{
    [JsonPropertyName("isSuccess")]
    public bool? IsSuccess { get; init; }

    [JsonPropertyName("CorrelationId")]
    public string? CorrelationId { get; init; }

    /// <summary>
    /// True when Absa accepted the operation (no errors and <c>isSuccess</c> not false).
    /// </summary>
    [JsonIgnore]
    public bool Succeeded => !HasErrors && IsSuccess != false;
}
