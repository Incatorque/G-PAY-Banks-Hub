using System.Collections.Concurrent;
using System.Diagnostics;
using GPay.Banking.Domain;
using GPay.Banking.Services.Capitec.Api.Clients;
using GPay.Banking.Services.Capitec.Api.Mapping;

namespace GPay.Banking.Services.Capitec.Api.Services;

/// <summary>Maps authenticated-by-source-IP Capitec CPS status callbacks to Bank Hub updates.</summary>
public sealed class CapitecPaymentCallbackService : IPaymentCallbackService
{
    private static readonly ConcurrentDictionary<string, byte> Seen = new(StringComparer.Ordinal);
    private readonly ILogger<CapitecPaymentCallbackService> _logger;
    private readonly IBankApiCallAuditor _auditor;

    public CapitecPaymentCallbackService(ILogger<CapitecPaymentCallbackService> logger, IBankApiCallAuditor auditor)
    {
        _logger = logger;
        _auditor = auditor;
    }

    public BankCode Bank => BankCode.Capitec;

    public async Task<PaymentCallbackResult> HandlePaymentAsync(PaymentCallbackInbound inbound, CancellationToken cancellationToken = default)
    {
        var timer = Stopwatch.StartNew();
        var payload = CapitecPaymentCallbackParser.Parse(inbound.RawJson);
        if (payload is null)
        {
            var invalid = PaymentCallbackResult.Fail("Callback must contain trantype, unique_identifier, and status.");
            await AuditAsync(inbound, null, invalid, timer, cancellationToken);
            return invalid;
        }

        var key = $"{payload.TransactionType}|{payload.UniqueIdentifier}|{payload.StatusCode}|{payload.StatusLabel}";
        if (!Seen.TryAdd(key, 0))
        {
            var duplicate = new PaymentCallbackResult { IsAuthorized = true, Success = true, Message = "Duplicate callback ignored." };
            await AuditAsync(inbound, payload, duplicate, timer, cancellationToken);
            return duplicate;
        }

        var update = new PaymentCallbackUpdate
        {
            TransactionReference = payload.TransactionReference,
            ApiReference = payload.ApiReference,
            Status = payload.StatusLabel,
            PaymentRail = payload.PaymentRail,
            ErrorMessage = payload.ErrorMessage,
            ResponseJson = inbound.RawJson
        };
        var result = PaymentCallbackResult.Ok(update);
        await AuditAsync(inbound, payload, result, timer, cancellationToken);
        _logger.LogInformation("Capitec callback type={Type} status={Status} identifier={Identifier} method={Method}.",
            payload.TransactionType, payload.StatusLabel, payload.UniqueIdentifier, payload.PaymentRail);
        return result;
    }

    private Task AuditAsync(PaymentCallbackInbound inbound, CapitecParsedPaymentCallback? payload,
        PaymentCallbackResult result, Stopwatch timer, CancellationToken cancellationToken)
    {
        timer.Stop();
        return CapitecCallAudit.WriteAsync(_auditor, _logger, new BankApiCallAuditEntry
        {
            BankCode = BankCode.Capitec.ToString(),
            Direction = "Inbound",
            Operation = "PaymentCallback",
            HttpMethod = "POST",
            Path = "/api/app/callback/process-payment",
            CorrelationId = payload?.UniqueIdentifier,
            RequestJson = CapitecApiPayloadRedactor.Redact(inbound.RawJson),
            ResponseJson = result.Message,
            DurationMs = timer.ElapsedMilliseconds,
            Success = result.Success,
            ErrorCode = payload?.ErrorCode ?? payload?.StatusCode?.ToString()
        }, cancellationToken);
    }
}
