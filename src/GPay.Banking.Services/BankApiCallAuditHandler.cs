using System.Diagnostics;
using System.Text.Json;
using GPay.Banking.Domain;
using GPay.Banking.Services.Absa.Api.Clients;
using Microsoft.Extensions.DependencyInjection;

namespace GPay.Banking.Services;

/// <summary>Request metadata consumed by <see cref="BankApiCallAuditHandler"/>.</summary>
internal static class BankApiCallAuditMetadata
{
    public static readonly HttpRequestOptionsKey<Guid> GpayOrderId =
        new("BankHub.GpayOrderId");
}

/// <summary>
/// Captures outbound bank HTTP traffic and writes it through the shared audit service.
/// </summary>
internal sealed class BankApiCallAuditHandler(
    IServiceScopeFactory scopeFactory,
    ILogger<BankApiCallAuditHandler> logger,
    string defaultBankCode) : DelegatingHandler
{
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
            using var requestDocument = JsonElementReader.ParseOrNull(requestBody);
            using var responseDocument = JsonElementReader.ParseOrNull(responseBody);
            var requestJson = requestDocument?.RootElement ?? default;
            var responseJson = responseDocument?.RootElement ?? default;
            var orderId = request.Options.TryGetValue(BankApiCallAuditMetadata.GpayOrderId, out var gpayOrderId)
                ? gpayOrderId
                : FindGuid(requestJson, "GpayOrderId");
            var correlationId = GetHeader(request, "X-Correlation-ID")
                ?? JsonElementReader.FindString(requestJson, "CorrelationId")
                ?? JsonElementReader.FindString(responseJson, "CorrelationId");
            var errorCode = !success
                ? JsonElementReader.FindString(responseJson, "ErrorCode")
                    ?? statusCode?.ToString()
                    ?? sendException?.GetType().Name
                : null;

            await RecordSafelyAsync(new BankApiCallAuditEntry
            {
                GpayOrderId = orderId,
                BankCode = defaultBankCode,
                Operation = GetOperation(request.RequestUri),
                HttpMethod = request.Method.Method,
                Path = request.RequestUri?.AbsolutePath,
                HttpStatus = statusCode,
                CorrelationId = correlationId,
                RequestJson = AbsaApiPayloadRedactor.Redact(requestBody),
                ResponseJson = AbsaApiPayloadRedactor.Redact(responseBody),
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
            await using var scope = scopeFactory.CreateAsyncScope();
            var auditor = scope.ServiceProvider.GetRequiredService<IBankApiCallAuditor>();
            await auditor.RecordAsync(entry, CancellationToken.None);
        }
        catch (Exception exception)
        {
            logger.LogWarning(
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
            logger.LogDebug(exception, "Could not read bank HTTP {ContentType} for audit.", contentType);
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

    private static Guid? FindGuid(JsonElement json, string propertyName)
    {
        var value = JsonElementReader.FindString(json, propertyName);
        return Guid.TryParse(value, out var result) ? result : null;
    }

}
