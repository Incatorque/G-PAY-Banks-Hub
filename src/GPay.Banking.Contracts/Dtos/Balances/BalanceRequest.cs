namespace GPay.Banking.Contracts.Dtos.Balances;

/// <summary>
/// GPay account balance enquiry request.
/// </summary>
public sealed class BalanceRequest
{
    /// <summary>
    /// Account number to enquire.
    /// </summary>
    public required string AccountNumber { get; init; }

    /// <summary>
    /// Optional branch / sort code.
    /// </summary>
    public string? BranchCode { get; init; }
}
