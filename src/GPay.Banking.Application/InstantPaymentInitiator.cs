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

internal sealed class InstantPaymentInitiator(
    IBankRouterService bankRouter,
    IGpayOrderSync gpayOrderSync,
    IGpayPaymentNotificationQueue notifications,
    IBankPaymentConfigurationProvider paymentConfigurationProvider,
    IBankPaymentSchedule bankPaymentSchedule,
    ILogger<InstantPaymentInitiator> logger) : IInstantPaymentInitiator
{
    public async Task<PaymentResultDto> InitiateAsync(
        InitiatePaymentRequestDto input,
        Guid? gpayOrderId = null,
        string? orderNumber = null,
        CancellationToken cancellationToken = default)
    {
        var bankStorageCode = input.Bank ?? string.Empty;
        var notificationOrderNumber = orderNumber ?? gpayOrderId?.ToString("D") ?? input.Reference;
        var notificationRail = input.PaymentRail?.Trim().ToUpperInvariant() ?? string.Empty;
        var correlationId = Guid.NewGuid().ToString("N");
        BankCode bank;
        var domainRequest = new InstantPaymentRequest
        {
            GpayOrderId = gpayOrderId,
            FromAccountNumber = input.FromAccountNumber,
            ToAccountNumber = input.ToAccountNumber,
            ToBranchCode = input.ToBranchCode,
            BeneficiaryBank = input.BeneficiaryBank,
            ToBankBicfi = input.ToBankBicfi,
            Amount = input.Amount,
            Currency = input.Currency,
            Reference = input.Reference,
            BeneficiaryName = input.BeneficiaryName,
            PaymentRail = input.PaymentRail ?? string.Empty,
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
            var bankConfiguration = await paymentConfigurationProvider.GetAsync(bankStorageCode, cancellationToken);
            try
            {
                notificationRail = PaymentRailRouting.ForBankRequest(
                    bank,
                    input.PaymentRail,
                    input.Amount,
                    bankConfiguration);
            }
            catch (Exception ex)
            {
                PaymentRuleDiagnostics.LogRailResolutionFailure(
                    logger,
                    bankStorageCode,
                    notificationOrderNumber,
                    input.Amount,
                    input.PaymentRail,
                    bankConfiguration,
                    ex);
                throw;
            }

            domainRequest = domainRequest with { PaymentRail = notificationRail };

            PaymentRuleDiagnostics.LogRailSelection(
                logger,
                bankStorageCode,
                notificationOrderNumber,
                input.Amount,
                input.PaymentRail,
                notificationRail,
                bankConfiguration);

            var deferralReason = await bankPaymentSchedule.GetDeferralReasonAsync(
                input.Amount,
                bankStorageCode,
                notificationRail,
                input.BeneficiaryBank,
                cancellationToken);
            if (deferralReason is not null)
            {
                throw new PaymentDeferredException($"Payment was not submitted to {bankStorageCode}. {deferralReason}");
            }

            var result = await bankRouter.SubmitPaymentAsync(bank, domainRequest, correlationId);
            dto = BankPaymentResultMapper.From(result, correlationId);
        }
        catch (PaymentDeferredException)
        {
            throw;
        }
        catch (Exception ex) when (ex is not OperationCanceledException || !cancellationToken.IsCancellationRequested)
        {
            await notifications.EnqueueAsync(new GpayPaymentInitiatedNotification
            {
                Bank = bankStorageCode,
                Rail = notificationRail,
                OrderNumber = notificationOrderNumber,
                Amount = input.Amount,
                Currency = input.Currency,
                FromAccountNumber = input.FromAccountNumber,
                ToAccountNumber = input.ToAccountNumber,
                Reference = input.Reference,
                Status = PaymentStatuses.Error,
                IsError = true,
                ErrorCode = "PAYMENT_INITIATION_EXCEPTION",
                ErrorMessage = ex.Message
            });

            throw;
        }

        var statusIsFailure = PaymentStatusClassifier.IsFailure(dto.Status);
        await notifications.EnqueueAsync(new GpayPaymentInitiatedNotification
        {
            Bank = bankStorageCode,
            Rail = notificationRail,
            OrderNumber = notificationOrderNumber,
            Amount = dto.Amount ?? input.Amount,
            Currency = dto.Currency ?? input.Currency,
            FromAccountNumber = input.FromAccountNumber,
            ToAccountNumber = input.ToAccountNumber,
            Reference = dto.Reference ?? input.Reference,
            Status = dto.Status ?? (dto.Success ? "Received / Processing" : PaymentStatuses.Error),
            IsError = !dto.Success || statusIsFailure,
            ErrorCode = dto.ErrorCode ?? (statusIsFailure ? dto.Status : null),
            ErrorMessage = dto.ErrorMessage ?? dto.ResultDescription
        });

        await gpayOrderSync.ApplyPaymentAsync(new GpayPaymentSyncRequest
        {
            GpayOrderId = gpayOrderId,
            Reference = dto.Reference ?? input.Reference,
            TransactionReference = dto.TransactionReference,
            SourceStatementRef = dto.SourceStatementRef,
            TargetStatementRef = dto.TargetStatementRef,
            HubStatus = dto.Status ?? (dto.Success ? "Submitted" : PaymentStatuses.Error),
            Note = dto.ResultDescription ?? dto.ErrorMessage,
            Amount = dto.Amount ?? input.Amount
        });

        logger.LogInformation(
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
