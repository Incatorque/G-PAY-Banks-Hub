using GPay.Banking.BankHub;
using GPay.Banking.Domain;
using GPay.Banking.Domain.Dtos;
using GPay.Banking.Helpers;
using Microsoft.Extensions.Logging;
using Volo.Abp;
using Volo.Abp.BackgroundJobs;

namespace GPay.Banking;

/// <summary>Runs the shared payment submission flow for API requests and internal workers.</summary>
public interface IInstantPaymentInitiator
{
    Task<PaymentResultDto> InitiateAsync(
        InitiatePaymentRequestDto input,
        Guid? gpayOrderId = null,
        string? orderNumber = null);
}

internal sealed class InstantPaymentInitiator : IInstantPaymentInitiator
{
    private readonly IBankRouterService _bankRouter;
    private readonly IGpayOrderSync _gpayOrderSync;
    private readonly IBackgroundJobManager _backgroundJobs;
    private readonly ILogger<InstantPaymentInitiator> _logger;

    public InstantPaymentInitiator(
        IBankRouterService bankRouter,
        IGpayOrderSync gpayOrderSync,
        IBackgroundJobManager backgroundJobs,
        ILogger<InstantPaymentInitiator> logger)
    {
        _bankRouter = bankRouter;
        _gpayOrderSync = gpayOrderSync;
        _backgroundJobs = backgroundJobs;
        _logger = logger;
    }

    public async Task<PaymentResultDto> InitiateAsync(
        InitiatePaymentRequestDto input,
        Guid? gpayOrderId = null,
        string? orderNumber = null)
    {
        if (input.Amount <= 0)
        {
            throw new UserFriendlyException("Amount must be greater than zero.");
        }

        var bank = BankCodeParser.Parse(input.Bank);
        var bankStorageCode = bank.ToString();
        var notificationOrderNumber = orderNumber ?? gpayOrderId?.ToString("D") ?? input.Reference;
        var correlationId = Guid.NewGuid().ToString("N");
        var domainRequest = new InstantPaymentRequest
        {
            GpayOrderId = gpayOrderId,
            FromAccountNumber = input.FromAccountNumber,
            ToAccountNumber = input.ToAccountNumber,
            ToBranchCode = input.ToBranchCode,
            Amount = input.Amount,
            Currency = input.Currency,
            Reference = input.Reference,
            BeneficiaryName = input.BeneficiaryName,
            PaymentRail = input.PaymentRail,
            SubmittingEntityName = input.SubmittingEntityName,
            SubsidiaryEntityName = input.SubsidiaryEntityName,
            AuthorisationIndicator = input.AuthorisationIndicator,
            FromAccountType = input.FromAccountType,
            FromShortName = input.FromShortName,
            FromStatementRef = input.FromStatementRef,
            ToAccountType = input.ToAccountType,
            ToStatementRef = input.ToStatementRef,
            IsTrustAccount = input.IsTrustAccount,
            PaymentDate = input.PaymentDate,
            ProofOfPaymentEmail = input.ProofOfPaymentEmail,
            ProofOfPaymentMobile = input.ProofOfPaymentMobile,
            ProofOfPaymentIndicator = input.ProofOfPaymentIndicator,
            CallbackUri = input.CallbackUri,
            CallbackToken = input.CallbackToken,
            CallbackSupportEmail = input.CallbackSupportEmail
        };

        PaymentResultDto dto;

        try
        {
            var result = await _bankRouter.SubmitPaymentAsync(bank, domainRequest, correlationId);
            dto = BankPaymentResultMapper.From(result, correlationId);
        }
        catch (Exception ex)
        {
            await QueueInitiationNotificationAsync(new GpayPaymentInitiatedNotification
            {
                Bank = bankStorageCode,
                OrderNumber = notificationOrderNumber,
                Amount = input.Amount,
                Currency = input.Currency,
                FromAccountNumber = input.FromAccountNumber,
                ToAccountNumber = input.ToAccountNumber,
                Reference = input.Reference,
                Status = "Error",
                IsError = true,
                ErrorCode = "PAYMENT_INITIATION_EXCEPTION",
                ErrorMessage = ex.Message
            });

            throw;
        }

        await QueueInitiationNotificationAsync(new GpayPaymentInitiatedNotification
        {
            Bank = bankStorageCode,
            OrderNumber = notificationOrderNumber,
            Amount = dto.Amount ?? input.Amount,
            Currency = dto.Currency ?? input.Currency,
            FromAccountNumber = input.FromAccountNumber,
            ToAccountNumber = input.ToAccountNumber,
            Reference = dto.Reference ?? input.Reference,
            Status = dto.Status ?? (dto.Success ? "Received / Processing" : "Error"),
            IsError = !dto.Success,
            ErrorCode = dto.ErrorCode,
            ErrorMessage = dto.ErrorMessage ?? dto.ResultDescription
        });

        await _gpayOrderSync.ApplyPaymentAsync(new GpayPaymentSyncRequest
        {
            GpayOrderId = gpayOrderId,
            Reference = dto.Reference ?? input.Reference,
            TransactionReference = dto.TransactionReference,
            SourceStatementRef = dto.SourceStatementRef,
            TargetStatementRef = dto.TargetStatementRef,
            HubStatus = dto.Status ?? (dto.Success ? "Submitted" : "Error"),
            Note = dto.ResultDescription ?? dto.ErrorMessage,
            Amount = dto.Amount ?? input.Amount
        });

        _logger.LogInformation(
            "Payment initiation at {Bank} completed with status {Status} " +
            "(success={Success}, correlation={CorrelationId}, GPayOrderId={GpayOrderId}).",
            bankStorageCode,
            dto.Status,
            dto.Success,
            correlationId,
            gpayOrderId);

        return dto;
    }

    private async Task QueueInitiationNotificationAsync(GpayPaymentInitiatedNotification notification)
    {
        try
        {
            await _backgroundJobs.EnqueueAsync(notification);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to queue payment initiation email for {Bank} payment reference {Reference}.",
                notification.Bank,
                notification.Reference);
        }
    }
}
