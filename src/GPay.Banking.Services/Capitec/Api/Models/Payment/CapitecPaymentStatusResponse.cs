using System.Text.Json.Serialization;

namespace GPay.Banking.Services.Capitec.Api.Models.Payment;

public sealed class CapitecPaymentStatusResponse
{
    [JsonPropertyName("StatusList")]
    public List<CapitecPaymentStatusListItem>? StatusList { get; init; }

    [JsonPropertyName("ErrorList")]
    public List<CapitecPaymentErrorItem>? ErrorList { get; init; }

    [JsonIgnore]
    public bool HasErrors => ErrorList is { Count: > 0 } && (StatusList is null || StatusList.Count == 0);
}
