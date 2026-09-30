using System.Collections.Concurrent;
using System.Diagnostics;
using GPay.Banking;
using GPay.Banking.Domain;
using GPay.Banking.Services.Absa.Api.Clients;
using GPay.Banking.Services.Absa.Api.Mapping;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GPay.Banking.Services.Absa.Api.Services;

/// <summary>
/// Absa-specific payment callback validation and payload mapping.
/// </summary>
public sealed class AbsaPaymentCallbackService : IPaymentCallbackService
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
            var invalid = PaymentCallbackResult.Fail("Invalid callback payload.");
            await AuditAsync(inbound, null, invalid, started, cancellationToken);
            return invalid;
        }

        var expectedToken = GetAbsaPaymentToken();
        var token = inbound.Token ?? payload.Token;
        if (string.IsNullOrWhiteSpace(expectedToken) ||
            !string.Equals(token, expectedToken, StringComparison.Ordinal))
        {
            _logger.LogWarning("Absa payment callback rejected: invalid token");
            var unauthorized = PaymentCallbackResult.Unauthorized("Invalid token.");
            await AuditAsync(inbound, payload, unauthorized, started, cancellationToken);
            return unauthorized;
        }

        var apiRef = payload.ApiReference;
        var txRef = payload.TransactionReference;
        var status = payload.StatusLabel;
        var key = $"{txRef ?? apiRef}|{payload.PaymentRail}|{payload.StatusCode}";

        if (!Seen.TryAdd(key, 0))
        {
            _logger.LogInformation("Absa payment callback duplicate ignored Key={Key}", key);
            var duplicate = PaymentCallbackResult.Duplicate();
            await AuditAsync(inbound, payload, duplicate, started, cancellationToken);
            return duplicate;
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

        var ok = PaymentCallbackResult.Ok(update);
        await AuditAsync(inbound, payload, ok, started, cancellationToken);
        return ok;
    }

    private Task AuditAsync(
        PaymentCallbackInbound inbound,
        AbsaParsedPaymentCallback? payload,
        PaymentCallbackResult result,
        Stopwatch started,
        CancellationToken cancellationToken)
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
            },
            cancellationToken);
    }

    private string GetAbsaPaymentToken()
    {
        if (_options.Banks.TryGetValue("Absa", out var bank) &&
            !string.IsNullOrWhiteSpace(bank.PaymentToken))
        {
            return bank.PaymentToken;
        }

        return string.Empty;
    }
}
