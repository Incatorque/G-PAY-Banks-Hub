using System.Text.Json.Serialization;
using System;

namespace GPay.Banking.Services.Absa.Api.Models.Payment;

/// <summary>
/// Absa Payment Status request (MIG – Payment API v1.8).
/// </summary>
public sealed class AbsaPaymentStatusRequest
{
    /// <summary>Internal GPay order identifier; excluded from the Absa request payload.</summary>
    [JsonIgnore]
    public Guid? GpayOrderId { get; init; }

    /// <summary>
    /// Array of TransactionRef (or ApiRef) values to query.
    /// </summary>
    [JsonPropertyName("Correlations")]
    public required List<string> Correlations { get; init; }

    /// <summary>
    /// Session from <c>/api/User/Authenticate</c>.
    /// </summary>
    [JsonPropertyName("Session")]
    public string? Session { get; set; }
}
