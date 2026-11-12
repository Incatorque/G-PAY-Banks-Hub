namespace GPay.Banking;

/// <summary>Bank-neutral view of a bank error catalog entry.</summary>
public sealed class BankErrorInfo
{
    public required string Bank { get; init; }
    public required string Code { get; init; }
    public required string Message { get; init; }
    public required string Category { get; init; }
    public required string Severity { get; init; }
    public bool Known { get; init; }
    public string? BankMessage { get; init; }
}
