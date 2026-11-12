using System.Text.Json.Serialization;

namespace GPay.Banking.Services.Capitec.Api.Models.Payment;

public sealed class CapitecPaymentInitiateRequest
{
    [JsonIgnore]
    public Guid? GpayOrderId { get; init; }

    [JsonPropertyName("groupHeader")]
    public required CapitecGroupHeader GroupHeader { get; init; }

    [JsonPropertyName("paymentInstruction")]
    public required List<CapitecPaymentInstruction> PaymentInstruction { get; init; }
}

public sealed class CapitecGroupHeader
{
    [JsonPropertyName("messageIdentification")]
    public required string MessageIdentification { get; init; }

    [JsonPropertyName("creationDateTime")]
    public required string CreationDateTime { get; init; }

    [JsonPropertyName("numberOfTransactions")]
    public required string NumberOfTransactions { get; init; }

    [JsonPropertyName("controlSum")]
    public required decimal ControlSum { get; init; }

    [JsonPropertyName("initiatingPartyId")]
    public required string InitiatingPartyId { get; init; }
}

public sealed class CapitecPaymentInstruction
{
    [JsonPropertyName("instructionUETR")]
    public required string InstructionUetr { get; init; }

    [JsonPropertyName("consolidatedDebitPosting")]
    public bool ConsolidatedDebitPosting { get; init; }

    [JsonPropertyName("numberOfTransactions")]
    public required int NumberOfTransactions { get; init; }

    [JsonPropertyName("controlSum")]
    public required decimal ControlSum { get; init; }

    [JsonPropertyName("instructionMop")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? InstructionMop { get; init; }

    [JsonPropertyName("instructedDate")]
    public required string InstructedDate { get; init; }

    [JsonPropertyName("debtorName")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? DebtorName { get; init; }

    [JsonPropertyName("debtorAccountIdentification")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? DebtorAccountIdentification { get; init; }

    [JsonPropertyName("creditTransferTransaction")]
    public required List<CapitecCreditTransferTransaction> CreditTransferTransaction { get; init; }
}

public sealed class CapitecCreditTransferTransaction
{
    [JsonPropertyName("endToEndIdentification")]
    public required string EndToEndIdentification { get; init; }

    [JsonPropertyName("transactionMop")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? TransactionMop { get; init; }

    [JsonPropertyName("instructedAmount")]
    public required decimal InstructedAmount { get; init; }

    [JsonPropertyName("creditorAgentBICFI")]
    public required string CreditorAgentBicfi { get; init; }

    [JsonPropertyName("creditorName")]
    public required string CreditorName { get; init; }

    [JsonPropertyName("creditorAccountIdentification")]
    public required string CreditorAccountIdentification { get; init; }

    [JsonPropertyName("creditorReference")]
    public required string CreditorReference { get; init; }
}
