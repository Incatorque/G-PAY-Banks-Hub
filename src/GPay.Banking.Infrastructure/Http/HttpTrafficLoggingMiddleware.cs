using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace GPay.Banking.Infrastructure.Http;

/// <summary>
/// Logs HTTP requests and responses separately including headers and bodies.
/// Sensitive authorization headers are redacted.
/// </summary>
public sealed class HttpTrafficLoggingMiddleware
{
    private static readonly HashSet<string> RedactedHeaders = new(StringComparer.OrdinalIgnoreCase)
    {
        "Authorization",
        "Cookie",
        "Set-Cookie",
        "X-Api-Key"
    };

    private readonly RequestDelegate _next;
    private readonly ILogger<HttpTrafficLoggingMiddleware> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpTrafficLoggingMiddleware"/> class.
    /// </summary>
    public HttpTrafficLoggingMiddleware(RequestDelegate next, ILogger<HttpTrafficLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// Invokes the middleware.
    /// </summary>
    public async Task InvokeAsync(HttpContext context)
    {
        context.Request.EnableBuffering();
        var requestBody = await ReadBodyAsync(context.Request.Body);
        context.Request.Body.Position = 0;

        _logger.LogInformation(
            "HTTP Request {Method} {Path} Headers={Headers} Body={Body}",
            context.Request.Method,
            context.Request.Path.Value,
            FormatHeaders(context.Request.Headers),
            requestBody);

        var originalBody = context.Response.Body;
        await using var responseBuffer = new MemoryStream();
        context.Response.Body = responseBuffer;

        try
        {
            await _next(context);

            responseBuffer.Position = 0;
            var responseBody = await new StreamReader(responseBuffer, Encoding.UTF8).ReadToEndAsync();
            responseBuffer.Position = 0;
            await responseBuffer.CopyToAsync(originalBody);

            _logger.LogInformation(
                "HTTP Response {StatusCode} {Path} Headers={Headers} Body={Body}",
                context.Response.StatusCode,
                context.Request.Path.Value,
                FormatHeaders(context.Response.Headers),
                responseBody);
        }
        finally
        {
            context.Response.Body = originalBody;
        }
    }

    private static async Task<string> ReadBodyAsync(Stream body)
    {
        using var reader = new StreamReader(body, Encoding.UTF8, detectEncodingFromByteOrderMarks: false, leaveOpen: true);
        return await reader.ReadToEndAsync();
    }

    private static string FormatHeaders(IHeaderDictionary headers)
    {
        var parts = headers.Select(h =>
        {
            var value = RedactedHeaders.Contains(h.Key) ? "***REDACTED***" : h.Value.ToString();
            return $"{h.Key}={value}";
        });

        return string.Join("; ", parts);
    }
}
