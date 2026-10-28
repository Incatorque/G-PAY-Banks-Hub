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
/// </summary>
/// <remarks>
/// <para>
/// Register with banks as:
/// <c>POST /api/app/callback/process-payment?bank=Absa</c>
/// </para>
/// <para>
/// When <c>bank</c> is omitted, source IP / domain allow-lists can resolve it.
/// Request body is bank-native JSON (Absa PaymentCallback). Not JWT-authenticated —
/// secured via allow-list + shared Token (+ signature inside the bank adapter).
/// Absa may retry; this handler is idempotent.
/// </para>
/// </remarks>
public class CallbackAppService : ApplicationService, ICallbackAppService
{
    private readonly IBankCapabilityResolver _resolver;
    private readonly IRepository<BankHubApiCall, Guid> _apiCallRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly BankCallbackOptions _callbackOptions;
    private readonly IGpayOrderSync _gpayOrderSync;

    public CallbackAppService(
        IBankCapabilityResolver resolver,
        IRepository<BankHubApiCall, Guid> apiCallRepository,
        IHttpContextAccessor httpContextAccessor,
        IOptions<BankCallbackOptions> callbackOptions,
        IGpayOrderSync gpayOrderSync)
    {
        _resolver = resolver;
        _apiCallRepository = apiCallRepository;
        _httpContextAccessor = httpContextAccessor;
        _callbackOptions = callbackOptions.Value;
        _gpayOrderSync = gpayOrderSync;
    }

    /// <summary>
    /// Processes a bank payment status callback and updates the matching GPay order when found.
    /// </summary>
    /// <param name="bank">
    /// Bank key: <c>Absa</c>, <c>Fnb</c>, or <c>Nedbank</c>.
    /// Omit to resolve from configured source IP / domain allow-lists.
    /// </param>
    /// <returns>Acknowledgement for the bank (<c>success</c> / <c>message</c>). Idempotent under Absa retries.</returns>
    /// <remarks>
    /// Anonymous. Body = bank-native JSON (Absa PaymentCallback with Token).
    /// Secured by allow-list + shared Token (+ signature in adapter).
    /// </remarks>
    [AllowAnonymous]
    public async Task<CallbackAckDto> ProcessPaymentAsync(string? bank = null)
    {
        var http = _httpContextAccessor.HttpContext;
        var sourceIp = CallbackSourceAllowList.GetSourceIp(http);

        var bankKey = string.IsNullOrWhiteSpace(bank)
            ? await CallbackSourceAllowList.ResolveBankKeyAsync(
                _callbackOptions,
                sourceIp)
            : bank;

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

        var bankCode = BankCodeParser.Parse(bankKey);
        var handler = _resolver.GetPaymentCallback(bankCode);
        var result = await handler.HandlePaymentAsync(new PaymentCallbackInbound
        {
            RawJson = rawJson
        });

        if (!result.IsAuthorized)
        {
            throw new AbpAuthorizationException(result.Message);
        }

        var acknowledgement = new CallbackAckDto
        {
            Success = result.Success,
            Message = result.Message
        };

        if (!result.IsDuplicate && result.Update is not null)
        {
            await ApplyUpdateAsync(result.Update, bankCode);
        }

        return acknowledgement;
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

    private async Task ApplyUpdateAsync(PaymentCallbackUpdate update, BankCode bankCode)
    {
        var bankStorageCode = bankCode.ToString();
        var paymentCall = await BankPaymentCallLookup.FindAsync(
            _apiCallRepository,
            bankStorageCode,
            update.TransactionReference,
            update.ApiReference);

        if (paymentCall?.GpayOrderId is not Guid gpayOrderId)
        {
            Logger.LogWarning(
                "Could not map {Bank} callback references to a GPay order. TxRef={TransactionReference}, ApiRef={ApiReference}.",
                bankStorageCode,
                update.TransactionReference,
                update.ApiReference);
            return;
        }

        await _gpayOrderSync.ApplyPaymentAsync(new GpayPaymentSyncRequest
        {
            GpayOrderId = gpayOrderId,
            Reference = update.TransactionReference ?? update.ApiReference,
            TransactionReference = update.TransactionReference,
            HubStatus = update.Status,
            Note = update.ErrorMessage,
            Amount = update.Amount ?? 0m
        });
    }
}
