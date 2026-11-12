using GPay.Banking.BankHub;
using GPay.Banking.Domain;
using GPay.Banking.Helpers;
using System.Diagnostics;
using System.Text.Json;
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
/// <para>Bank callback URL: <c>POST /api/app/callback/process-payment?bank={BankCode}</c>.</para>
/// <para>
/// When <c>bank</c> is omitted, source IP / domain allow-lists can resolve it.
/// The request body uses that bank's callback format. This endpoint is not JWT-authenticated;
/// it uses configured source allow-lists plus bank-specific verification and idempotent updates.
/// </para>
/// </remarks>
public class CallbackAppService(
    IBankCapabilityResolver resolver,
    IRepository<BankHubApiCall, Guid> apiCallRepository,
    IHttpContextAccessor httpContextAccessor,
    IOptions<BankCallbackOptions> callbackOptions,
    IGpayOrderSync gpayOrderSync)
    : ApplicationService, ICallbackAppService
{
    private readonly BankCallbackOptions _callbackOptions = callbackOptions.Value;

    /// <summary>
    /// Processes a bank payment status callback and updates the matching GPay order when found.
    /// </summary>
    /// <param name="bank">
    /// Registered bank code or name, for example <c>Absa</c> or <c>Capitec</c>.
    /// Omit to resolve from configured source IP / domain allow-lists.
    /// </param>
    /// <returns>Acknowledgement for the bank (<c>success</c> / <c>message</c>).</returns>
    /// <remarks>
    /// Anonymous. The bank adapter validates the bank-native payload and configured source policy.
    /// </remarks>
    [AllowAnonymous]
    public async Task<CallbackAckDto> ProcessPaymentAsync(string? bank = null)
    {
        var http = httpContextAccessor.HttpContext;
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

        var requestId = Activity.Current?.TraceId.ToString() ?? http?.TraceIdentifier;
        Logger.LogInformation(
            "Received {Bank} payment callback RequestId={RequestId} BodyLength={BodyLength}.",
            bankKey,
            requestId,
            rawJson.Length);

        var bankCode = BankCodeParser.Parse(bankKey);
        var handler = resolver.GetPaymentCallback(bankCode);
        PaymentCallbackResult result;
        try
        {
            result = await handler.HandlePaymentAsync(new PaymentCallbackInbound
            {
                RawJson = rawJson
            });
        }
        catch (Exception ex)
        {
            Logger.LogError(
                ex,
                "Payment callback handler failed for {Bank} RequestId={RequestId}.",
                bankKey,
                requestId);
            throw;
        }

        if (!result.IsAuthorized)
        {
            throw new AbpAuthorizationException(result.Message);
        }

        var acknowledgement = new CallbackAckDto
        {
            Success = result.Success,
            Message = result.Message
        };

        if (result.Update is not null)
        {
            try
            {
                await ApplyUpdateAsync(result.Update, bankCode);
            }
            catch (Exception ex)
            {
                Logger.LogError(
                    ex,
                    "Payment callback order sync failed for {Bank} TxRef={TransactionReference} ApiRef={ApiReference} Status={Status} RequestId={RequestId}.",
                    bankKey,
                    result.Update.TransactionReference,
                    result.Update.ApiReference,
                    result.Update.Status,
                    requestId);
                throw;
            }
        }

        Logger.LogInformation(
            "Payment callback completed for {Bank} Success={Success} RequestId={RequestId}.",
            bankKey,
            result.Success,
            requestId);

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
            apiCallRepository,
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

        await gpayOrderSync.ApplyPaymentAsync(new GpayPaymentSyncRequest
        {
            GpayOrderId = gpayOrderId,
            Reference = update.TransactionReference ??
                (bankCode == BankCode.Capitec ? ReadCapitecEndToEndReference(paymentCall.RequestJson) : update.ApiReference),
            TransactionReference = update.TransactionReference,
            HubStatus = update.Status,
            Note = update.ErrorMessage,
            Amount = update.Amount ?? 0m
        });
    }

    private static string? ReadCapitecEndToEndReference(string? requestJson)
    {
        if (string.IsNullOrWhiteSpace(requestJson)) return null;
        try
        {
            using var document = JsonDocument.Parse(requestJson);
            return document.RootElement.GetProperty("paymentInstruction")[0]
                .GetProperty("creditTransferTransaction")[0]
                .GetProperty("endToEndIdentification").GetString();
        }
        catch (Exception exception) when (exception is JsonException or InvalidOperationException or KeyNotFoundException or IndexOutOfRangeException)
        {
            return null;
        }
    }
}
