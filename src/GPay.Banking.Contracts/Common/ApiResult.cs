namespace GPay.Banking.Contracts.Common;

/// <summary>
/// Unified success/error envelope for all GPay banking responses.
/// Cast <see cref="Data"/> to the operation-specific DTO when <see cref="Success"/> is true.
/// </summary>
/// <typeparam name="T">Payload type for a successful response.</typeparam>
public sealed class ApiResult<T>
{
    /// <summary>
    /// Indicates whether the operation completed successfully.
    /// </summary>
    public bool Success { get; init; }

    /// <summary>
    /// Operation payload when <see cref="Success"/> is <c>true</c>; otherwise <c>null</c>.
    /// </summary>
    public T? Data { get; init; }

    /// <summary>
    /// Error details when <see cref="Success"/> is <c>false</c>; otherwise <c>null</c>.
    /// </summary>
    public ApiError? Error { get; init; }

    /// <summary>
    /// Correlation identifier used across HTTP and RabbitMQ hops.
    /// </summary>
    public required string CorrelationId { get; init; }

    /// <summary>
    /// Creates a successful result.
    /// </summary>
    public static ApiResult<T> Ok(T data, string correlationId) =>
        new()
        {
            Success = true,
            Data = data,
            CorrelationId = correlationId
        };

    /// <summary>
    /// Creates a failed result.
    /// </summary>
    public static ApiResult<T> Fail(ApiError error, string correlationId) =>
        new()
        {
            Success = false,
            Error = error,
            CorrelationId = correlationId
        };
}
