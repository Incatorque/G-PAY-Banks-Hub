using System.Collections.Generic;

namespace GPay.Banking;

public sealed class ApiError
{
    public required string Code { get; init; }
    public required string Message { get; init; }
    public string? BankCode { get; init; }
    public string? BankMessage { get; init; }
    public IReadOnlyDictionary<string, string[]>? Details { get; init; }
}



