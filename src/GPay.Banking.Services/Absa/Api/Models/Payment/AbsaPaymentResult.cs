using System.Text.Json.Serialization;
using GPay.Banking.Services;

namespace GPay.Banking.Services.Absa.Api.Models.Payment;

/// <summary>Common status and correlation data returned by Absa payment operations.</summary>
public abstract class AbsaPaymentResult
{
    [JsonPropertyName("Correlations")]
    public List<AbsaPaymentCorrelation>? Correlations { get; init; }

    [JsonPropertyName("Status")]
    public object? Status { get; init; }

    [JsonPropertyName("ErrorList")]
    public List<AbsaPaymentErrorItem>? ErrorList { get; init; }

    [JsonPropertyName("Session")]
    public string? Session { get; init; }

    [JsonIgnore]
    public int? StatusCode => int.TryParse(StatusString, out var value) ? value : null;

    [JsonIgnore]
    public string StatusString => JsonElementReader.ReadScalarString(Status);

    [JsonIgnore]
    public virtual bool HasErrors => ErrorList is { Count: > 0 };

    /// <summary>Returns the value for a correlation type (1–4).</summary>
    public string? GetCorrelation(int type) => Correlations?.FirstOrDefault(correlation => correlation.Type == type)?.Value;
}
