using System.Text.Json;
using GPay.Banking.BankHub;
using GPay.Banking.Domain;
using GPay.Banking.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Authorization;
using Volo.Abp.Domain.Repositories;

namespace GPay.Banking;

/// <summary>
/// Bank payment webhook AppService (ABP conventional endpoint).
/// Register with banks as: POST /api/app/callback/process-payment?bank=Absa
/// When bank is omitted, source IP/domain allow-lists can resolve it.
/// </summary>
public class CallbackAppService : ApplicationService, ICallbackAppService
{
    private readonly IBankCapabilityResolver _resolver;
    private readonly IRepository<BankHubPaymentRecord, Guid> _paymentRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly BankCallbackOptions _callbackOptions;

    public CallbackAppService(
        IBankCapabilityResolver resolver,
        IRepository<BankHubPaymentRecord, Guid> paymentRepository,
        IHttpContextAccessor httpContextAccessor,
        IOptions<BankCallbackOptions> callbackOptions)
    {
        _resolver = resolver;
        _paymentRepository = paymentRepository;
        _httpContextAccessor = httpContextAccessor;
        _callbackOptions = callbackOptions.Value;
    }

    /// <summary>
    /// Anonymous bank webhook. Validates source allow-list, then dispatches to the bank adapter.
    /// </summary>
    [AllowAnonymous]
    public async Task<CallbackAckDto> ProcessPaymentAsync(string? bank = null)
    {
        var http = _httpContextAccessor.HttpContext;
        var sourceIp = CallbackSourceAllowList.GetSourceIp(http);

        var bankKey = bank;
        if (string.IsNullOrWhiteSpace(bankKey))
        {
            bankKey = await CallbackSourceAllowList.ResolveBankKeyAsync(
                _callbackOptions,
                sourceIp);
        }

        if (string.IsNullOrWhiteSpace(bankKey))
        {
            throw new UserFriendlyException(
                "Bank was not specified and could not be resolved from source IP/domain.");
        }

        var sourceCheck = await CallbackSourceAllowList.CheckAsync(
            _callbackOptions,
            bankKey,
            sourceIp,
            Logger);

        if (!sourceCheck.IsAllowed)
        {
            Logger.LogWarning(
                "Payment callback rejected for bank {Bank}: {Reason} (source {SourceIp})",
                bankKey,
                sourceCheck.Message,
                sourceIp);
            throw new AbpAuthorizationException(sourceCheck.Message);
        }

        var rawJson = await ReadBodyAsync(http);
        if (string.IsNullOrWhiteSpace(rawJson))
        {
            throw new UserFriendlyException("Callback body is required.");
        }

        string? token = null;
        try
        {
            using var doc = JsonDocument.Parse(rawJson);
            if (doc.RootElement.ValueKind == JsonValueKind.Object &&
                doc.RootElement.TryGetProperty("Token", out var tokenEl) &&
                tokenEl.ValueKind == JsonValueKind.String)
            {
                token = tokenEl.GetString();
            }
        }
        catch (JsonException)
        {
            // Bank adapter will reject invalid JSON.
        }

        var bankCode = BankCodeParser.Parse(bankKey);
        var handler = _resolver.GetPaymentCallback(bankCode);
        var result = await handler.HandlePaymentAsync(new PaymentCallbackInbound
        {
            Token = token,
            RawJson = rawJson
        });

        if (!result.IsAuthorized)
        {
            throw new AbpAuthorizationException(result.Message);
        }

        if (result.IsDuplicate || result.Update is null)
        {
            return new CallbackAckDto
            {
                Success = result.Success,
                Message = result.Message
            };
        }

        await ApplyUpdateAsync(result.Update);

        return new CallbackAckDto
        {
            Success = result.Success,
            Message = result.Message
        };
    }

    private static async Task<string> ReadBodyAsync(HttpContext? http)
    {
        if (http?.Request.Body is null)
        {
            return string.Empty;
        }

        http.Request.EnableBuffering();
        http.Request.Body.Position = 0;
        using var reader = new StreamReader(http.Request.Body, leaveOpen: true);
        var body = await reader.ReadToEndAsync();
        http.Request.Body.Position = 0;
        return body;
    }

    private async Task ApplyUpdateAsync(PaymentCallbackUpdate update)
    {
        BankHubPaymentRecord? record = null;

        if (!string.IsNullOrWhiteSpace(update.TransactionReference))
        {
            record = await _paymentRepository.FirstOrDefaultAsync(
                x => x.TransactionReference == update.TransactionReference);
        }

        if (record is null && !string.IsNullOrWhiteSpace(update.ApiReference))
        {
            record = await _paymentRepository.FirstOrDefaultAsync(
                x => x.ApiReference == update.ApiReference);
        }

        if (record is null)
        {
            return;
        }

        if (!string.IsNullOrWhiteSpace(update.Status))
        {
            record.Status = update.Status!;
        }

        if (!string.IsNullOrWhiteSpace(update.TransactionReference))
        {
            record.TransactionReference = update.TransactionReference;
        }

        if (!string.IsNullOrWhiteSpace(update.ApiReference))
        {
            record.ApiReference = update.ApiReference;
        }

        if (!string.IsNullOrWhiteSpace(update.PaymentRail))
        {
            record.PaymentRail = update.PaymentRail!;
        }

        if (update.Amount.HasValue)
        {
            record.Amount = update.Amount.Value;
        }

        if (!string.IsNullOrWhiteSpace(update.Currency))
        {
            record.Currency = update.Currency!;
        }

        if (!string.IsNullOrWhiteSpace(update.ResponseJson))
        {
            record.ResponseJson = update.ResponseJson;
        }

        record.ErrorMessage = update.ErrorMessage;
        await _paymentRepository.UpdateAsync(record, autoSave: true);
    }
}
