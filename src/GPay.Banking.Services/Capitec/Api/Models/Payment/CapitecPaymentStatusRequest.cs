using System.Text.Json.Serialization;

namespace GPay.Banking.Services.Capitec.Api.Models.Payment;

public sealed class CapitecPaymentStatusRequest
{
    [JsonIgnore]
    public Guid? GpayOrderId { get; init; }

    [JsonPropertyName("groupHeader")]
    public required CapitecStatusGroupHeader GroupHeader { get; init; }

    [JsonPropertyName("statusEnquiryRequest")]
    public required CapitecStatusEnquiry StatusEnquiryRequest { get; init; }
}

public sealed class CapitecStatusGroupHeader
{
    [JsonPropertyName("messageIdentification")]
    public required string MessageIdentification { get; init; }

    [JsonPropertyName("creationDateTime")]
    public required string CreationDateTime { get; init; }

    [JsonPropertyName("initiatingPartyId")]
    public required string InitiatingPartyId { get; init; }
}

public sealed class CapitecStatusEnquiry
{
    [JsonPropertyName("originalMessageIdentification")]
    public required string OriginalMessageIdentification { get; init; }

    [JsonPropertyName("originalInstructionUETR")]
    public required string OriginalInstructionUetr { get; init; }

    [JsonPropertyName("originalEndToEndIdentification")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? OriginalEndToEndIdentification { get; init; }
}
