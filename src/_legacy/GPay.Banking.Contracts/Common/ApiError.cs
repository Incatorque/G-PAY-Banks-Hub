namespace GPay.Banking.Contracts.Common;

/// <summary>
/// Standardized error payload returned inside <see cref="ApiResult{T}"/>.
/// </summary>
public sealed class ApiError
{
    /// <summary>
    /// GPay-normalized error code.
    /// </summary>
    public required string Code { get; init; }

    /// <summary>
    /// Human-readable error message.
    /// </summary>
    public required string Message { get; init; }

    /// <summary>
    /// Optional bank-native error code prior to mapping.
    /// </summary>
    public string? BankCode { get; init; }

    /// <summary>
    /// Optional bank-native error detail prior to mapping.
    /// </summary>
    public string? BankMessage { get; init; }

    /// <summary>
    /// Optional validation or diagnostic details.
    /// </summary>
    public IReadOnlyDictionary<string, string[]>? Details { get; init; }
}
