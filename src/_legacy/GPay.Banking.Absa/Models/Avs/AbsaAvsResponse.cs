using System.Text.Json.Serialization;

namespace GPay.Banking.Absa.Models.Avs;

/// <summary>
/// Absa Validate Bank Details / Validate Bank Reference response (MIG – AVS API v1).
/// </summary>
public sealed class AbsaAvsResponse
{
    /// <summary>
    /// Status of the enquiry. Success: 000,2,3,4,5 — Pending: 001 — Tech err: 033 — Duplicate: 098 — Timeout: 099.
    /// Absa samples also return numeric Status (e.g. 5, 1, 2).
    /// </summary>
    [JsonPropertyName("Status")]
    public object? Status { get; init; }

    [JsonPropertyName("ReferenceNumber")]
    public string? ReferenceNumber { get; init; }

    [JsonPropertyName("ValueList")]
    public List<AbsaAvsValueItem>? ValueList { get; init; }

    [JsonPropertyName("CorrelationId")]
    public string? CorrelationId { get; init; }

    [JsonPropertyName("Session")]
    public string? Session { get; init; }

    [JsonPropertyName("ErrorList")]
    public List<AbsaAvsErrorItem>? ErrorList { get; init; }

    [JsonPropertyName("ResponseUtcTime")]
    public string? ResponseUtcTime { get; init; }

    [JsonPropertyName("UserFriendlyDisplayList")]
    public List<AbsaAvsErrorItem>? UserFriendlyDisplayList { get; init; }

    /// <summary>
    /// Normalized status string (e.g. "5", "001").
    /// </summary>
    [JsonIgnore]
    public string StatusCode =>
        Status switch
        {
            null => string.Empty,
            string s => s.Trim(),
            System.Text.Json.JsonElement el when el.ValueKind == System.Text.Json.JsonValueKind.Number => el.GetRawText(),
            System.Text.Json.JsonElement el when el.ValueKind == System.Text.Json.JsonValueKind.String => el.GetString()?.Trim() ?? string.Empty,
            _ => Status.ToString()?.Trim() ?? string.Empty
        };

    /// <summary>
    /// True when Status indicates a completed success result with match criteria.
    /// </summary>
    [JsonIgnore]
    public bool IsSuccessStatus
    {
        get
        {
            var s = StatusCode;
            return s is "000" or "0" or "2" or "3" or "4" or "5" or "00";
        }
    }

    /// <summary>
    /// True when Status indicates pending (non-Absa first hop).
    /// </summary>
    [JsonIgnore]
    public bool IsPendingStatus
    {
        get
        {
            var s = StatusCode;
            return s is "001" or "1";
        }
    }

    /// <summary>
    /// True when Absa returned ErrorList items and no usable ValueList.
    /// </summary>
    [JsonIgnore]
    public bool HasErrors =>
        ErrorList is { Count: > 0 } &&
        (ValueList is null || ValueList.Count == 0);

    /// <summary>
    /// Lookup a ValueList entry by key (case-insensitive).
    /// </summary>
    public string? GetValue(string key) =>
        ValueList?
            .FirstOrDefault(v => string.Equals(v.Key, key, StringComparison.OrdinalIgnoreCase))
            ?.Value;
}

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

/// <summary>
/// Absa error list item.
/// </summary>
public sealed class AbsaAvsErrorItem
{
    [JsonPropertyName("CorrelationId")]
    public string? CorrelationId { get; init; }

    [JsonPropertyName("Code")]
    public string? Code { get; init; }

    [JsonPropertyName("Description")]
    public string? Description { get; init; }

    [JsonPropertyName("Group")]
    public int? Group { get; init; }

    [JsonPropertyName("Severity")]
    public int? Severity { get; init; }

    [JsonPropertyName("Tag")]
    public string? Tag { get; init; }

    [JsonPropertyName("DetailedMessages")]
    public List<AbsaAvsDetailedMessage>? DetailedMessages { get; init; }
}

/// <summary>
/// Absa detailed error message.
/// </summary>
public sealed class AbsaAvsDetailedMessage
{
    [JsonPropertyName("Code")]
    public string? Code { get; init; }

    [JsonPropertyName("Message")]
    public string? Message { get; init; }

    [JsonPropertyName("Severity")]
    public int? Severity { get; init; }

    [JsonPropertyName("ErrorCorellationId")]
    public string? ErrorCorrelationId { get; init; }
}
