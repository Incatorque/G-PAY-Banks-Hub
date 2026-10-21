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
    Task<PaymentResultDto> InitiateAsync(InitiatePaymentRequestDto input, Guid? gpayOrderId = null);
}

public sealed class InstantPaymentInitiator : IInstantPaymentInitiator
{
    private readonly IBankCapabilityResolver _resolver;
    private readonly IGpayOrderSync _gpayOrderSync;
    private readonly ILogger<InstantPaymentInitiator> _logger;

    public InstantPaymentInitiator(
        IBankCapabilityResolver resolver,
        IGpayOrderSync gpayOrderSync,
        ILogger<InstantPaymentInitiator> logger)
    {
        _resolver = resolver;
        _gpayOrderSync = gpayOrderSync;
        _logger = logger;
    }

    public async Task<PaymentResultDto> InitiateAsync(
        InitiatePaymentRequestDto input,
        Guid? gpayOrderId = null)
    {
        if (input.Amount <= 0)
        {
            throw new UserFriendlyException("Amount must be greater than zero.");
        }

        var bank = BankCodeParser.Parse(input.Bank);
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
            PaymentRail = string.IsNullOrWhiteSpace(input.PaymentRail) ? "RPP" : input.PaymentRail,
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

        var paymentService = _resolver.GetInstantPayment(bank);
        var result = await paymentService.PayAsync(domainRequest, correlationId);
        var dto = BankPaymentResultMapper.FromInitiation(result, correlationId);

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
            BankCodeParser.ToStorageCode(bank),
            dto.Status,
            dto.Success,
            correlationId,
            gpayOrderId);

        return dto;
    }
}
