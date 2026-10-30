using GPay.Banking.BankHub;
using GPay.Banking.Domain;
using GPay.Banking.Domain.Dtos;
using GPay.Banking.Helpers;
using Microsoft.Extensions.Logging;
using Volo.Abp;

namespace GPay.Banking;

/// <summary>Runs the shared payment submission flow for API requests and internal workers.</summary>
public interface IInstantPaymentInitiator
{
    Task<PaymentResultDto> InitiateAsync(
        InitiatePaymentRequestDto input,
        Guid? gpayOrderId = null,
        string? orderNumber = null,
        CancellationToken cancellationToken = default);
}

internal sealed class InstantPaymentInitiator : IInstantPaymentInitiator
{
    private readonly IBankRouterService _bankRouter;
    private readonly IGpayOrderSync _gpayOrderSync;
    private readonly IGpayPaymentNotificationQueue _notifications;
    private readonly IAbsaPaymentSchedule _absaPaymentSchedule;
    private readonly ILogger<InstantPaymentInitiator> _logger;

    public InstantPaymentInitiator(
        IBankRouterService bankRouter,
        IGpayOrderSync gpayOrderSync,
        IGpayPaymentNotificationQueue notifications,
        IAbsaPaymentSchedule absaPaymentSchedule,
        ILogger<InstantPaymentInitiator> logger)
    {
        _bankRouter = bankRouter;
        _gpayOrderSync = gpayOrderSync;
        _notifications = notifications;
        _absaPaymentSchedule = absaPaymentSchedule;
        _logger = logger;
    }

    public async Task<PaymentResultDto> InitiateAsync(
        InitiatePaymentRequestDto input,
        Guid? gpayOrderId = null,
        string? orderNumber = null,
        CancellationToken cancellationToken = default)
    {
        var bankStorageCode = input.Bank ?? string.Empty;
        var notificationOrderNumber = orderNumber ?? gpayOrderId?.ToString("D") ?? input.Reference;
        var notificationRail = string.IsNullOrWhiteSpace(input.PaymentRail)
            ? "RPP"
            : input.PaymentRail.Trim().ToUpperInvariant();
        var correlationId = Guid.NewGuid().ToString("N");
        BankCode bank;
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
            if (input.Amount <= 0)
            {
                throw new UserFriendlyException("Amount must be greater than zero.");
            }

            bank = BankCodeParser.Parse(input.Bank);
            bankStorageCode = bank.ToString();

            if (bank != BankCode.Absa && string.IsNullOrWhiteSpace(input.PaymentRail))
            {
                notificationRail = "-";
            }

            if (bank == BankCode.Absa && string.Equals(notificationRail, "PAAF", StringComparison.Ordinal))
            {
                var deferralReason = await _absaPaymentSchedule.GetPaafDeferralReasonAsync(
                    input.Amount,
                    input.BeneficiaryBank,
                    cancellationToken);
                if (deferralReason is not null)
                {
                    throw new PaymentDeferredException($"Payment was not submitted to Absa. {deferralReason}");
                }
            }

            var result = await _bankRouter.SubmitPaymentAsync(bank, domainRequest, correlationId);
            dto = BankPaymentResultMapper.From(result, correlationId);
        }
        catch (PaymentDeferredException)
        {
            throw;
        }
        catch (Exception ex)
        {
            await _notifications.EnqueueAsync(new GpayPaymentInitiatedNotification
            {
                Bank = bankStorageCode,
                Route = "API",
                Rail = notificationRail,
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

        await _notifications.EnqueueAsync(new GpayPaymentInitiatedNotification
        {
            Bank = bankStorageCode,
            Route = "API",
            Rail = notificationRail,
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

}
