using System.Collections.Concurrent;
using System.Diagnostics;
using GPay.Banking;
using GPay.Banking.Domain;
using GPay.Banking.Services.Absa.Api.Clients;
using GPay.Banking.Services.Absa.Api.Mapping;

namespace GPay.Banking.Services.Absa.Api.Services;

/// <summary>
/// Absa-specific payment callback validation and payload mapping.
/// </summary>
internal sealed class AbsaPaymentCallbackService : IPaymentCallbackService
{
    private static readonly ConcurrentDictionary<string, byte> Seen = new(StringComparer.Ordinal);

    private readonly BankCallbackOptions _options;
    private readonly ILogger<AbsaPaymentCallbackService> _logger;
    private readonly IBankApiCallAuditor _auditor;

    public AbsaPaymentCallbackService(
        IOptions<BankCallbackOptions> options,
        ILogger<AbsaPaymentCallbackService> logger,
        IBankApiCallAuditor auditor)
    {
        _options = options.Value;
        _logger = logger;
        _auditor = auditor;
    }

    public BankCode Bank => BankCode.Absa;

    public async Task<PaymentCallbackResult> HandlePaymentAsync(
        PaymentCallbackInbound inbound,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var started = Stopwatch.StartNew();

        var payload = AbsaPaymentCallbackParser.Parse(inbound.RawJson);
        if (payload is null)
        {
            _logger.LogWarning("Absa payment callback JSON deserialize failed");
            return await AuditAndReturnAsync(
                inbound,
                null,
                PaymentCallbackResult.Fail("Invalid callback payload."),
                started);
        }

        var expectedToken = _options.Banks.TryGetValue("Absa", out var bank)
            ? bank.PaymentToken
            : null;
        var token = inbound.Token ?? payload.Token;
        if (string.IsNullOrWhiteSpace(expectedToken) ||
            !string.Equals(token, expectedToken, StringComparison.Ordinal))
        {
            _logger.LogWarning("Absa payment callback rejected: invalid token");
            return await AuditAndReturnAsync(
                inbound,
                payload,
                PaymentCallbackResult.Unauthorized("Invalid token."),
                started);
        }

        var apiRef = payload.ApiReference;
        var txRef = payload.TransactionReference;
        var status = payload.StatusLabel;
        var key = $"{txRef ?? apiRef}|{payload.PaymentRail}|{payload.StatusCode}";

        if (!Seen.TryAdd(key, 0))
        {
            _logger.LogInformation("Absa payment callback duplicate ignored Key={Key}", key);
            return await AuditAndReturnAsync(
                inbound,
                payload,
                PaymentCallbackResult.Duplicate(),
                started);
        }

        _logger.LogInformation(
            "Absa payment callback Type={Type} Status={Status} BankStatus={BankStatus} TxRef={TxRef}",
            payload.PaymentRail,
            status,
            payload.StatusCode,
            txRef);

        var update = new PaymentCallbackUpdate
        {
            TransactionReference = txRef,
            ApiReference = apiRef,
            Status = status,
            PaymentRail = payload.PaymentRail,
            ErrorMessage = payload.ErrorMessage,
            ResponseJson = inbound.RawJson
        };

        return await AuditAndReturnAsync(inbound, payload, PaymentCallbackResult.Ok(update), started);
    }

    private async Task<PaymentCallbackResult> AuditAndReturnAsync(
        PaymentCallbackInbound inbound,
        AbsaParsedPaymentCallback? payload,
        PaymentCallbackResult result,
        Stopwatch started)
    {
        await AuditAsync(inbound, payload, result, started);
        return result;
    }

    private Task AuditAsync(
        PaymentCallbackInbound inbound,
        AbsaParsedPaymentCallback? payload,
        PaymentCallbackResult result,
        Stopwatch started)
    {
        started.Stop();
        return AbsaCallAudit.WriteAsync(
            _auditor,
            _logger,
            new BankApiCallAuditEntry
            {
                Direction = "Inbound",
                Operation = "PaymentCallback",
                HttpMethod = "POST",
                Path = "/api/app/callback/process-payment",
                CorrelationId = payload?.ApiReference,
                RequestJson = AbsaApiPayloadRedactor.Redact(inbound.RawJson),
                ResponseJson = result.Message,
                DurationMs = started.ElapsedMilliseconds,
                Success = result.Success,
                ErrorCode = payload?.ErrorCode ?? payload?.StatusCode?.ToString()
            });
    }

}
