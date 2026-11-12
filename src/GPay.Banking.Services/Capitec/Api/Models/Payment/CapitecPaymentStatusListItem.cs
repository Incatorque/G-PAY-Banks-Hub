using System.Text.Json.Serialization;

namespace GPay.Banking.Services.Capitec.Api.Models.Payment;

public sealed class CapitecPaymentStatusListItem
{
    [JsonPropertyName("transactionType")]
    public string? TransactionType { get; init; }

    [JsonPropertyName("uniqueIdentifier")]
    public string? UniqueIdentifier { get; init; }

    [JsonPropertyName("transactionMop")]
    public string? TransactionMop { get; init; }

    [JsonPropertyName("status")]
    public string? Status { get; init; }

    [JsonPropertyName("reasonCode")]
    public string? ReasonCode { get; init; }

    [JsonPropertyName("reasonDescription")]
    public string? ReasonDescription { get; init; }

    [JsonIgnore]
    public int? StatusCode => int.TryParse(Status, out var code) ? code : null;

    [JsonIgnore]
    public bool HasErrors => string.Equals(Status, "Rejected", StringComparison.OrdinalIgnoreCase);
}
