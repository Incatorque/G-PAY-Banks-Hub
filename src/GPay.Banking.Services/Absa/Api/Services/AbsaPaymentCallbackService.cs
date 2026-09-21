using System.Collections.Concurrent;
using System.Text.Json;
using GPay.Banking;
using GPay.Banking.Domain;
using GPay.Banking.Domain.Dtos;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GPay.Banking.Services.Absa.Api.Services;

/// <summary>
/// Absa-specific payment callback validation and payload mapping.
/// </summary>
public sealed class AbsaPaymentCallbackService : IPaymentCallbackService
{
    private static readonly ConcurrentDictionary<string, byte> Seen = new(StringComparer.Ordinal);
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    private readonly BankCallbackOptions _options;
    private readonly ILogger<AbsaPaymentCallbackService> _logger;

    public AbsaPaymentCallbackService(
        IOptions<BankCallbackOptions> options,
        ILogger<AbsaPaymentCallbackService> logger)
    {
        _options = options.Value;
        _logger = logger;
    }

    public BankCode Bank => BankCode.Absa;

    public Task<PaymentCallbackResult> HandlePaymentAsync(
        PaymentCallbackInbound inbound,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        AbsaPaymentCallbackDto? payload;
        try
        {
            payload = JsonSerializer.Deserialize<AbsaPaymentCallbackDto>(inbound.RawJson, JsonOptions);
        }
        catch (JsonException ex)
        {
            _logger.LogWarning(ex, "Absa payment callback JSON deserialize failed");
            return Task.FromResult(PaymentCallbackResult.Fail("Invalid callback payload."));
        }

        if (payload is null)
        {
            return Task.FromResult(PaymentCallbackResult.Fail("Invalid callback payload."));
        }

        var expectedToken = GetAbsaPaymentToken();
        var token = inbound.Token ?? payload.Token;
        if (string.IsNullOrWhiteSpace(expectedToken) ||
            !string.Equals(token, expectedToken, StringComparison.Ordinal))
        {
            _logger.LogWarning("Absa payment callback rejected: invalid token");
            return Task.FromResult(PaymentCallbackResult.Unauthorized("Invalid token."));
        }

        var apiRef = payload.Correlations?.FirstOrDefault(c => c.Type == 3)?.Value;
        var txRef = payload.Correlations?.FirstOrDefault(c => c.Type == 4)?.Value;
        var status = payload.PaymentStatus?.ToString() ?? string.Empty;
        var key = $"{txRef ?? apiRef}|{payload.Type}|{status}";

        if (!Seen.TryAdd(key, 0))
        {
            _logger.LogInformation("Absa payment callback duplicate ignored Key={Key}", key);
            return Task.FromResult(PaymentCallbackResult.Duplicate());
        }

        _logger.LogInformation(
            "Absa payment callback Type={Type} Status={Status} TxRef={TxRef}",
            payload.Type,
            status,
            txRef);

        decimal? amount = null;
        if (decimal.TryParse(payload.Amount, out var parsedAmount))
        {
            amount = parsedAmount;
        }

        var firstError = payload.ErrorList?.FirstOrDefault();
        var update = new PaymentCallbackUpdate
        {
            TransactionReference = txRef,
            ApiReference = apiRef,
            Status = string.IsNullOrWhiteSpace(status) ? null : status,
            PaymentRail = payload.Type,
            Amount = amount,
            Currency = payload.CurrencyCode,
            ErrorMessage = firstError?.Description ?? firstError?.Message,
            ResponseJson = inbound.RawJson
        };

        return Task.FromResult(PaymentCallbackResult.Ok(update));
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
