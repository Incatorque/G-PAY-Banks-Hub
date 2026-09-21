using GPay.Banking.Contracts.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace GPay.Banking.Infrastructure.Http;

/// <summary>
/// Maps unhandled exceptions to the unified <see cref="ApiResult{T}"/> envelope.
/// </summary>
public static class GlobalExceptionHandler
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    /// <summary>
    /// Registers the global exception handler middleware.
    /// </summary>
    public static IApplicationBuilder UseGpayGlobalExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                var feature = context.Features.Get<IExceptionHandlerFeature>();
                var exception = feature?.Error;
                var logger = context.RequestServices.GetService(typeof(ILoggerFactory)) as ILoggerFactory;
                var log = logger?.CreateLogger("GPay.Banking.GlobalExceptionHandler");

                var correlationId = context.TraceIdentifier;
                if (context.Request.Headers.TryGetValue("X-Correlation-Id", out var header) &&
                    !string.IsNullOrWhiteSpace(header))
                {
                    correlationId = header.ToString();
                }

                log?.LogError(exception, "Unhandled exception. CorrelationId={CorrelationId}", correlationId);

                var result = ApiResult<object>.Fail(
                    new ApiError
                    {
                        Code = "GPAY_UNHANDLED",
                        Message = exception?.Message ?? "An unexpected error occurred."
                    },
                    correlationId);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync(JsonSerializer.Serialize(result, JsonOptions));
            });
        });

        return app;
    }
}
