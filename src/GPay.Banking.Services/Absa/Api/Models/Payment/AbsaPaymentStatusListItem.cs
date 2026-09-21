using System.Text.Json.Serialization;

namespace GPay.Banking.Services.Absa.Api.Models.Payment;

/// <summary>
/// Single payment status entry in StatusList.
/// </summary>
public sealed class AbsaPaymentStatusListItem
{
    [JsonPropertyName("Correlations")]
    public List<AbsaPaymentCorrelation>? Correlations { get; init; }

    [JsonPropertyName("Status")]
    public object? Status { get; init; }

    [JsonPropertyName("ErrorList")]
    public List<AbsaPaymentErrorItem>? ErrorList { get; init; }

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
