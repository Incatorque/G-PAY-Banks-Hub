using System.Diagnostics;
using System.Text.Json;
using GPay.Banking.Domain;
using GPay.Banking.Services.Absa.Api.Clients;
using Microsoft.Extensions.DependencyInjection;

namespace GPay.Banking.Services;

/// <summary>Request metadata consumed by <see cref="BankApiCallAuditHandler"/>.</summary>
public static class BankApiCallAuditMetadata
{
    public static readonly HttpRequestOptionsKey<Guid> GpayOrderId =
        new("BankHub.GpayOrderId");

    public static readonly HttpRequestOptionsKey<string> CorrelationId =
        new("BankHub.CorrelationId");

    public static readonly HttpRequestOptionsKey<string> BankCode =
        new("BankHub.BankCode");
}

/// <summary>
/// Captures outbound bank HTTP traffic and writes it through the shared audit service.
/// </summary>
public sealed class BankApiCallAuditHandler : DelegatingHandler
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<BankApiCallAuditHandler> _logger;
    private readonly string _defaultBankCode;

    public BankApiCallAuditHandler(
        IServiceScopeFactory scopeFactory,
        ILogger<BankApiCallAuditHandler> logger,
        string defaultBankCode)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
        _defaultBankCode = defaultBankCode;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var stopwatch = Stopwatch.StartNew();
        var requestBody = await ReadContentSafelyAsync(request.Content, "request");
        HttpResponseMessage? response = null;
        string? responseBody = null;
        Exception? sendException = null;

        try
        {
            response = await base.SendAsync(request, cancellationToken);
            responseBody = await ReadContentSafelyAsync(response.Content, "response");
            return response;
        }
        catch (Exception exception)
        {
            sendException = exception;
            throw;
        }
        finally
        {
            stopwatch.Stop();

            int? statusCode = response is null ? null : (int)response.StatusCode;
            var success = response?.IsSuccessStatusCode == true;
            var bankCode = GetOption(request, BankApiCallAuditMetadata.BankCode) ?? _defaultBankCode;
            var orderId = request.Options.TryGetValue(BankApiCallAuditMetadata.GpayOrderId, out var gpayOrderId)
                ? gpayOrderId
                : FindGuid(requestBody, "GpayOrderId");
            var correlationId = GetOption(request, BankApiCallAuditMetadata.CorrelationId)
                ?? GetHeader(request, "X-Correlation-ID")
                ?? FindString(requestBody, "CorrelationId")
                ?? FindString(responseBody, "CorrelationId");
            var errorCode = !success
                ? FindString(responseBody, "ErrorCode")
                    ?? statusCode?.ToString()
                    ?? sendException?.GetType().Name
                : null;

            await RecordSafelyAsync(new BankApiCallAuditEntry
            {
                GpayOrderId = orderId,
                BankCode = bankCode,
                Direction = "Outbound",
                Operation = GetOperation(request.RequestUri),
                HttpMethod = request.Method.Method,
                Path = request.RequestUri?.AbsolutePath,
                HttpStatus = statusCode,
                CorrelationId = correlationId,
                RequestJson = Redact(requestBody),
                ResponseJson = Redact(responseBody),
                DurationMs = stopwatch.ElapsedMilliseconds,
                Success = success,
                ErrorCode = errorCode
            });
        }
    }

    private async Task RecordSafelyAsync(BankApiCallAuditEntry entry)
    {
        try
        {
            await using var scope = _scopeFactory.CreateAsyncScope();
            var auditor = scope.ServiceProvider.GetRequiredService<IBankApiCallAuditor>();
            await auditor.RecordAsync(entry, CancellationToken.None);
        }
        catch (Exception exception)
        {
            _logger.LogWarning(
                exception,
                "Could not write automatic bank API audit for {BankCode} {Operation}.",
                entry.BankCode,
                entry.Operation);
        }
    }

    private async Task<string?> ReadContentSafelyAsync(HttpContent? content, string contentType)
    {
        if (content is null)
        {
            return null;
        }

        try
        {
            return await content.ReadAsStringAsync(CancellationToken.None);
        }
        catch (Exception exception)
        {
            _logger.LogDebug(exception, "Could not read bank HTTP {ContentType} for audit.", contentType);
            return null;
        }
    }

    private static string GetOperation(Uri? uri)
    {
        if (uri is null)
        {
            return "HttpRequest";
        }

        var segment = uri.AbsolutePath.TrimEnd('/').Split('/').LastOrDefault();
        return string.IsNullOrWhiteSpace(segment) ? "HttpRequest" : segment;
    }

    private static string? GetHeader(HttpRequestMessage request, string name)
    {
        return request.Headers.TryGetValues(name, out var values)
            ? values.FirstOrDefault()
            : null;
    }

    private static string? GetOption(HttpRequestMessage request, HttpRequestOptionsKey<string> key)
    {
        return request.Options.TryGetValue(key, out var value) ? value : null;
    }

    private static Guid? FindGuid(string? json, string propertyName)
    {
        var value = FindString(json, propertyName);
        return Guid.TryParse(value, out var result) ? result : null;
    }

    private static string? FindString(string? json, string propertyName)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            return null;
        }

        try
        {
            using var document = JsonDocument.Parse(json);
            return FindString(document.RootElement, propertyName);
        }
        catch (JsonException)
        {
            return null;
        }
    }

    private static string? FindString(JsonElement element, string propertyName)
    {
        if (element.ValueKind == JsonValueKind.Object)
        {
            foreach (var property in element.EnumerateObject())
            {
                if (property.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase) &&
                    property.Value.ValueKind == JsonValueKind.String)
                {
                    return property.Value.GetString();
                }

                var nestedValue = FindString(property.Value, propertyName);
                if (nestedValue is not null)
                {
                    return nestedValue;
                }
            }
        }
        else if (element.ValueKind == JsonValueKind.Array)
        {
            foreach (var item in element.EnumerateArray())
            {
                var nestedValue = FindString(item, propertyName);
                if (nestedValue is not null)
                {
                    return nestedValue;
                }
            }
        }

        return null;
    }

    private static string? Redact(string? json)
    {
        return AbsaApiPayloadRedactor.Redact(json);
    }
}
