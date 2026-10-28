using System.Text.Json;

namespace GPay.Banking.Services.Absa.Api.Services;

internal static class AbsaServiceLogging
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    public static void LogOutcome(
        ILogger logger,
        string operation,
        object request,
        object? response,
        bool success,
        string? code,
        string? message,
        string correlationId)
    {
        logger.LogInformation(
            "Absa {Operation} completed Success={Success} Code={Code} Message={Message} CorrelationId={CorrelationId} Request={Request} Response={Response}",
            operation,
            success,
            code,
            message,
            correlationId,
            JsonSerializer.Serialize(request, JsonOptions),
            response is null ? null : JsonSerializer.Serialize(response, JsonOptions));
    }
}
