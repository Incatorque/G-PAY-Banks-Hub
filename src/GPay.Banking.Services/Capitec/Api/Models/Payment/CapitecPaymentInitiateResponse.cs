namespace GPay.Banking.Services.Capitec.Api.Models.Payment;

/// <summary>Accepted Capitec payout request and its identifiers.</summary>
public sealed class CapitecPaymentInitiateResponse
{
    public required string MessageIdentification { get; init; }
    public required string InstructionUetr { get; init; }
    public required string EndToEndIdentification { get; init; }
    public int StatusCode { get; init; } = 1;
    public bool HasErrors => false;
}
