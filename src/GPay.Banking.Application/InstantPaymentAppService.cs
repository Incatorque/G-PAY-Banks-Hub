using GPay.Banking.BankHub;
using GPay.Banking.Domain;
using GPay.Banking.Domain.Dtos;
using GPay.Banking.Helpers;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace GPay.Banking;

/// <summary>
/// Instant payment AppService — PayShap / RTC initiate and status enquiry.
/// </summary>
/// <remarks>
/// Routes under <c>/api/app/instant-payment/*</c>.
/// </remarks>
[Authorize]
public class InstantPaymentAppService : ApplicationService, IInstantPaymentAppService
{
    private readonly IInstantPaymentInitiator _initiator;
    private readonly IBankRouterService _bankRouter;
    private readonly IRepository<BankHubApiCall, Guid> _apiCallRepository;
    private readonly IGpayOrderSync _gpayOrderSync;
    private readonly IGpayPaymentNotificationQueue _notifications;

    public InstantPaymentAppService(
        IInstantPaymentInitiator initiator,
        IBankRouterService bankRouter,
        IRepository<BankHubApiCall, Guid> apiCallRepository,
        IGpayOrderSync gpayOrderSync,
        IGpayPaymentNotificationQueue notifications)
    {
        _initiator = initiator;
        _bankRouter = bankRouter;
        _apiCallRepository = apiCallRepository;
        _gpayOrderSync = gpayOrderSync;
        _notifications = notifications;
    }

    /// <summary>
    /// Submits an instant payment to the bank named in the request body.
    /// </summary>
    /// <param name="input">Debit/credit accounts, amount, reference, rail, and target <c>Bank</c>.</param>
    /// <returns>Submission result with status, bank references, and <c>correlationId</c>.</returns>
    /// <remarks>Amount must be &gt; 0.</remarks>
    public Task<PaymentResultDto> InitiateAsync(InitiatePaymentRequestDto input) => _initiator.InitiateAsync(input);

    /// <summary>
    /// Queries live payment status from the bank and updates the linked GPay order when found.
    /// </summary>
    /// <param name="input">Bank plus <c>transactionReference</c> and/or <c>apiReference</c> (at least one required).</param>
    /// <returns>Normalized status payload from the bank.</returns>
    /// <remarks>Use as fallback when bank callbacks are missed.</remarks>
    public async Task<PaymentResultDto> GetStatusAsync(PaymentStatusRequestDto input)
    {
        if (string.IsNullOrWhiteSpace(input.TransactionReference) && string.IsNullOrWhiteSpace(input.ApiReference))
        {
            throw new UserFriendlyException("Provide transactionReference or apiReference.");
        }

        var bank = BankCodeParser.Parse(input.Bank);
        var correlationId = Guid.NewGuid().ToString("N");
        var bankCode = bank.ToString();
        var paymentCall = await BankPaymentCallLookup.FindAsync(
            _apiCallRepository,
            bankCode,
            input.TransactionReference,
            input.ApiReference);
        ApiResult<PaymentStatusResponse> result;
        try
        {
            result = await _bankRouter.GetPaymentStatusAsync(
                bank,
                new PaymentStatusRequest
                {
                    TransactionReference = input.TransactionReference,
                    ApiReference = input.ApiReference,
                    GpayOrderId = paymentCall?.GpayOrderId
                },
                correlationId);
        }
        catch (Exception ex)
        {
            await QueueStatusErrorAsync(input, paymentCall?.GpayOrderId, bankCode, "PAYMENT_STATUS_EXCEPTION", ex.Message);
            throw;
        }

        var dto = BankPaymentResultMapper.From(result, correlationId);
        if (!result.Success || result.Data is null)
        {
            await QueueStatusErrorAsync(
                input,
                paymentCall?.GpayOrderId,
                bankCode,
                result.Error?.Code ?? "PAYMENT_STATUS_ERROR",
                result.Error?.Message ?? "No status data returned.");
        }
        else if (PaymentStatusClassifier.IsFailure(result.Data.RawStatusLabel ?? result.Data.Status))
        {
            await _notifications.EnqueueAsync(new GpayPaymentInitiatedNotification
            {
                Bank = bankCode,
                OrderNumber = paymentCall?.GpayOrderId?.ToString("D") ?? input.TransactionReference ?? input.ApiReference ?? string.Empty,
                Reference = result.Data.Reference ?? input.TransactionReference ?? input.ApiReference ?? string.Empty,
                Amount = result.Data.Amount ?? 0m,
                Currency = result.Data.Currency ?? string.Empty,
                Status = result.Data.RawStatusLabel ?? result.Data.Status,
                IsStatusCheck = true,
                IsError = true,
                ErrorCode = result.Data.ErrorCode ?? result.Data.RawStatusLabel ?? result.Data.Status,
                ErrorMessage = result.Data.ResultDescription,
                StatusDescription = result.Data.ResultDescription
            });
        }
        if (result.Success && result.Data is not null && paymentCall?.GpayOrderId is Guid gpayOrderId)
        {
            await _gpayOrderSync.ApplyPaymentAsync(new GpayPaymentSyncRequest
            {
                GpayOrderId = gpayOrderId,
                Reference = dto.Reference,
                TransactionReference = dto.TransactionReference,
                SourceStatementRef = dto.SourceStatementRef,
                TargetStatementRef = dto.TargetStatementRef,
                HubStatus = dto.Status,
                Note = dto.ResultDescription,
                Amount = dto.Amount ?? 0m
            });
        }

        return dto;
    }

    private Task QueueStatusErrorAsync(
        PaymentStatusRequestDto input,
        Guid? orderId,
        string bank,
        string errorCode,
        string errorMessage) =>
        _notifications.EnqueueAsync(new GpayPaymentInitiatedNotification
        {
            Bank = bank,
            OrderNumber = orderId?.ToString("D") ?? input.TransactionReference ?? input.ApiReference ?? string.Empty,
            Reference = input.TransactionReference ?? input.ApiReference ?? string.Empty,
            Status = "Error",
            IsStatusCheck = true,
            IsError = true,
            ErrorCode = errorCode,
            ErrorMessage = errorMessage,
            StatusDescription = errorMessage
        });

}
