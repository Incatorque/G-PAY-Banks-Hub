using System.Text.Json.Serialization;

namespace GPay.Banking.Services.Absa.Api.Models.Payment;

/// <summary>
/// Absa Payment Initiate response (MIG – Payment API v1.8).
/// </summary>
public sealed class AbsaPaymentInitiateResponse
{
    /// <summary>
    /// Correlations: 1=source stmt, 2=target stmt, 3=ApiRef, 4=TransactionRef.
    /// </summary>
    [JsonPropertyName("Correlations")]
    public List<AbsaPaymentCorrelation>? Correlations { get; init; }

    /// <summary>
    /// Status: typically 2 = accepted/submitted. May be number or string.
    /// </summary>
    [JsonPropertyName("Status")]
    public object? Status { get; init; }

    [JsonPropertyName("ErrorList")]
    public List<AbsaPaymentErrorItem>? ErrorList { get; init; }

    [JsonPropertyName("Session")]
    public string? Session { get; init; }

    /// <summary>
    /// Normalized status integer when parseable.
    /// </summary>
    [JsonIgnore]
    public int? StatusCode
    {
        get
        {
            var s = StatusString;
            return int.TryParse(s, out var n) ? n : null;
        }
    }

    /// <summary>
    /// Normalized status string.
    /// </summary>
    [JsonIgnore]
    public string StatusString =>
        Status switch
        {
            null => string.Empty,
            string s => s.Trim(),
            System.Text.Json.JsonElement el when el.ValueKind == System.Text.Json.JsonValueKind.Number => el.GetRawText(),
            System.Text.Json.JsonElement el when el.ValueKind == System.Text.Json.JsonValueKind.String => el.GetString()?.Trim() ?? string.Empty,
            _ => Status.ToString()?.Trim() ?? string.Empty
        };

    /// <summary>
    /// True when ErrorList has items.
    /// </summary>
    [JsonIgnore]
    public bool HasErrors => ErrorList is { Count: > 0 };

    /// <summary>
    /// Returns the Value for a correlation Type (1–4).
    /// </summary>
    public string? GetCorrelation(int type) =>
        Correlations?
            .FirstOrDefault(c => c.Type == type)
            ?.Value;
}
