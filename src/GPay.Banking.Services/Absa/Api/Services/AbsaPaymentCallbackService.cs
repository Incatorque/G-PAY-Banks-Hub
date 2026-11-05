using System.Diagnostics;
using GPay.Banking;
using GPay.Banking.Domain;
using GPay.Banking.Services.Absa.Api.Clients;
using GPay.Banking.Services.Absa.Api.Mapping;

namespace GPay.Banking.Services.Absa.Api.Services;

/// <summary>
/// Absa-specific payment callback validation and payload mapping.
/// </summary>
internal sealed class AbsaPaymentCallbackService(
    IOptions<BankCallbackOptions> options,
    ILogger<AbsaPaymentCallbackService> logger,
    IBankApiCallAuditor auditor) : IPaymentCallbackService
{
    private readonly BankCallbackOptions _options = options.Value;

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
            logger.LogWarning("Absa payment callback JSON deserialize failed");
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
            logger.LogWarning("Absa payment callback rejected: invalid token");
            return await AuditAndReturnAsync(
                inbound,
                payload,
                PaymentCallbackResult.Unauthorized("Invalid token."),
                started);
        }

        var apiRef = payload.ApiReference;
        var txRef = payload.TransactionReference;
        var status = payload.StatusLabel;

        logger.LogInformation(
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
        started.Stop();
        await AbsaCallAudit.WriteAsync(
            auditor,
            logger,
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

        return result;
    }

}
