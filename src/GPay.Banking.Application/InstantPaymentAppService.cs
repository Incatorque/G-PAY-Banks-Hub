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

    public InstantPaymentAppService(
        IInstantPaymentInitiator initiator,
        IBankRouterService bankRouter,
        IRepository<BankHubApiCall, Guid> apiCallRepository,
        IGpayOrderSync gpayOrderSync)
    {
        _initiator = initiator;
        _bankRouter = bankRouter;
        _apiCallRepository = apiCallRepository;
        _gpayOrderSync = gpayOrderSync;
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
        var result = await _bankRouter.GetPaymentStatusAsync(
            bank,
            new PaymentStatusRequest
            {
                TransactionReference = input.TransactionReference,
                ApiReference = input.ApiReference,
                GpayOrderId = paymentCall?.GpayOrderId
            },
            correlationId);

        var dto = BankPaymentResultMapper.From(result, correlationId);
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

}
