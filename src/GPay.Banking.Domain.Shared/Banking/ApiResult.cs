namespace GPay.Banking;

public sealed class ApiResult<T>
{
    public bool Success { get; init; }
    public T? Data { get; init; }
    public ApiError? Error { get; init; }
    public required string CorrelationId { get; init; }

    public static ApiResult<T> Ok(T data, string correlationId) =>
        new() { Success = true, Data = data, CorrelationId = correlationId };

    public static ApiResult<T> Fail(ApiError error, string correlationId) =>
        new() { Success = false, Error = error, CorrelationId = correlationId };
}



