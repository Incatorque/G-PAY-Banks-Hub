using System.Text.Json;
using GPay.Banking.BankHub;
using GPay.Banking.Domain;
using GPay.Banking.Domain.Dtos;
using GPay.Banking.Helpers;
using GPay.Banking.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace GPay.Banking;

/// <summary>
/// Instant payment AppService — PayShap / RTC initiate, status enquiry, and record lookup.
/// </summary>
/// <remarks>
/// Routes under <c>/api/app/instant-payment/*</c>. Requires JWT and <c>Banking.Payments.*</c> permissions.
/// </remarks>
[Authorize(PaymentsPermissions.Default)]
public class InstantPaymentAppService : ApplicationService, IInstantPaymentAppService
{
    private readonly IBankCapabilityResolver _resolver;
    private readonly IRepository<BankHubPaymentRecord, Guid> _paymentRepository;
    private readonly IGpayOrderSync _gpayOrderSync;

    public InstantPaymentAppService(
        IBankCapabilityResolver resolver,
        IRepository<BankHubPaymentRecord, Guid> paymentRepository,
        IGpayOrderSync gpayOrderSync)
    {
        _resolver = resolver;
        _paymentRepository = paymentRepository;
        _gpayOrderSync = gpayOrderSync;
    }

    /// <summary>
    /// Submits an instant payment to the bank named in the request body.
    /// </summary>
    /// <param name="input">Debit/credit accounts, amount, reference, rail, and target <c>Bank</c>.</param>
    /// <returns>Submission result with status, bank references, <c>correlationId</c>, and <c>recordId</c>.</returns>
    /// <remarks>Permission: <c>Banking.Payments.Initiate</c>. Amount must be &gt; 0. Default rail <c>RPP</c> (PayShap).</remarks>
    [Authorize(PaymentsPermissions.Initiate)]
    public async Task<PaymentResultDto> InitiateAsync(InitiatePaymentRequestDto input)
    {
        if (input.Amount <= 0)
        {
            throw new UserFriendlyException("Amount must be greater than zero.");
        }

        var bank = BankCodeParser.Parse(input.Bank);
        var correlationId = Guid.NewGuid().ToString("N");
        var domainRequest = new InstantPaymentRequest
        {
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

        var service = _resolver.GetInstantPayment(bank);
        var result = await service.PayAsync(domainRequest, correlationId);
        var dto = Map(result, correlationId);

        var record = new BankHubPaymentRecord
        {
            BankCode = BankCodeParser.ToStorageCode(bank),
            CorrelationId = correlationId,
            Reference = dto.Reference ?? input.Reference,
            TransactionReference = dto.TransactionReference,
            ApiReference = dto.ApiReference,
            Amount = dto.Amount ?? input.Amount,
            Currency = dto.Currency ?? input.Currency,
            PaymentRail = dto.PaymentRail ?? input.PaymentRail,
            Status = dto.Status ?? (dto.Success ? "Submitted" : "Error"),
            BankStatusCode = dto.BankStatusCode,
            FromAccountNumber = input.FromAccountNumber,
            ToAccountNumber = input.ToAccountNumber,
            ToBranchCode = input.ToBranchCode,
            BeneficiaryName = input.BeneficiaryName,
            ResultDescription = dto.ResultDescription,
            RequestJson = JsonSerializer.Serialize(domainRequest),
            ResponseJson = JsonSerializer.Serialize(result),
            ErrorMessage = dto.ErrorMessage
        };
        await _paymentRepository.InsertAsync(record, autoSave: true);
        dto.RecordId = record.Id;
        await SyncGpayAsync(record, dto.SourceStatementRef, dto.TargetStatementRef, dto.ResultDescription);
        return dto;
    }

    /// <summary>
    /// Queries live payment status from the bank and updates the persisted record when found.
    /// </summary>
    /// <param name="input">Bank plus <c>transactionReference</c> and/or <c>apiReference</c> (at least one required).</param>
    /// <returns>Normalized status payload from the bank.</returns>
    /// <remarks>Permission: <c>Banking.Payments.View</c>. Use as fallback when Absa callbacks are missed.</remarks>
    [Authorize(PaymentsPermissions.View)]
    public async Task<PaymentResultDto> GetStatusAsync(PaymentStatusRequestDto input)
    {
        if (string.IsNullOrWhiteSpace(input.TransactionReference) && string.IsNullOrWhiteSpace(input.ApiReference))
        {
            throw new UserFriendlyException("Provide transactionReference or apiReference.");
        }

        var bank = BankCodeParser.Parse(input.Bank);
        var correlationId = Guid.NewGuid().ToString("N");
        var service = _resolver.GetInstantPayment(bank);
        var result = await service.GetStatusAsync(new PaymentStatusRequest
        {
            TransactionReference = input.TransactionReference,
            ApiReference = input.ApiReference
        }, correlationId);

        var dto = MapStatus(result, correlationId);

        BankHubPaymentRecord? record = null;
        if (!string.IsNullOrWhiteSpace(input.TransactionReference))
        {
            record = await _paymentRepository.FirstOrDefaultAsync(x => x.TransactionReference == input.TransactionReference);
        }
        if (record is null && !string.IsNullOrWhiteSpace(input.ApiReference))
        {
            record = await _paymentRepository.FirstOrDefaultAsync(x => x.ApiReference == input.ApiReference);
        }

        if (record is not null)
        {
            // Absa status has no amount — fill from the hub record saved at initiate.
            dto.RecordId = record.Id;
            dto.Amount ??= record.Amount;
            dto.Currency ??= record.Currency;
            dto.Reference ??= record.Reference;
            dto.PaymentRail ??= record.PaymentRail;
            dto.TransactionReference ??= record.TransactionReference;
            dto.ApiReference ??= record.ApiReference;

            if (result.Success && result.Data is not null)
            {
                record.Status = dto.Status ?? record.Status;
                record.TransactionReference = dto.TransactionReference ?? record.TransactionReference;
                record.ApiReference = dto.ApiReference ?? record.ApiReference;
                record.BankStatusCode = dto.BankStatusCode;
                record.ResultDescription = dto.ResultDescription;
                record.ResponseJson = JsonSerializer.Serialize(result);
                await _paymentRepository.UpdateAsync(record, autoSave: true);
                await SyncGpayAsync(record, dto.SourceStatementRef, dto.TargetStatementRef, dto.ResultDescription);
            }
        }

        return dto;
    }

    /// <summary>
    /// Loads a previously persisted payment record by id (no live bank call).
    /// </summary>
    /// <param name="id">Payment record id returned as <c>recordId</c> from initiate/status.</param>
    /// <remarks>Permission: <c>Banking.Payments.View</c>.</remarks>
    [Authorize(PaymentsPermissions.View)]
    public async Task<PaymentRecordDto> GetAsync(Guid id)
    {
        var r = await _paymentRepository.GetAsync(id);
        return new PaymentRecordDto
        {
            Id = r.Id,
            BankCode = r.BankCode,
            Status = r.Status,
            Reference = r.Reference,
            Amount = r.Amount,
            Currency = r.Currency,
            TransactionReference = r.TransactionReference,
            ApiReference = r.ApiReference,
            CreationTime = r.CreationTime
        };
    }

    private static PaymentResultDto Map(ApiResult<InstantPaymentResponse> result, string correlationId)
    {
        if (!result.Success || result.Data is null)
        {
            return new PaymentResultDto
            {
                Success = false,
                CorrelationId = correlationId,
                Status = "Error",
                ErrorCode = result.Error?.Code,
                ErrorMessage = result.Error?.Message,
                AbsaError = AbsaErrorResponses.From(result.Error)
            };
        }

        var d = result.Data;
        return new PaymentResultDto
        {
            Success = true,
            CorrelationId = correlationId,
            Status = d.RawStatusLabel ?? d.Status,
            Reference = d.Reference,
            Amount = d.Amount,
            Currency = d.Currency,
            ApiReference = d.ApiReference,
            TransactionReference = d.TransactionReference ?? d.TransactionId,
            SourceStatementRef = d.SourceStatementRef,
            TargetStatementRef = d.TargetStatementRef,
            BankStatusCode = d.BankStatusCode?.ToString(),
            PaymentRail = d.PaymentRail,
            ResultDescription = d.ResultDescription,
            ErrorCode = d.ErrorCode,
            AbsaError = AbsaErrorResponses.FromCode(d.ErrorCode, d.ResultDescription)
        };
    }

    private static PaymentResultDto MapStatus(ApiResult<PaymentStatusResponse> result, string correlationId)
    {
        if (!result.Success || result.Data is null)
        {
            return new PaymentResultDto
            {
                Success = false,
                CorrelationId = correlationId,
                Status = "Error",
                ErrorCode = result.Error?.Code,
                ErrorMessage = result.Error?.Message,
                AbsaError = AbsaErrorResponses.From(result.Error)
            };
        }

        var d = result.Data;
        return new PaymentResultDto
        {
            Success = true,
            CorrelationId = correlationId,
            Status = d.RawStatusLabel ?? d.Status,
            Reference = d.Reference,
            Amount = d.Amount,
            Currency = d.Currency,
            ApiReference = d.ApiReference,
            TransactionReference = d.TransactionReference ?? d.TransactionId,
            SourceStatementRef = d.SourceStatementRef,
            TargetStatementRef = d.TargetStatementRef,
            BankStatusCode = d.BankStatusCode?.ToString(),
            PaymentRail = d.PaymentRail,
            ResultDescription = d.ResultDescription,
            ErrorCode = d.ErrorCode,
            AbsaError = AbsaErrorResponses.FromCode(d.ErrorCode, d.ResultDescription)
        };
    }

    private Task SyncGpayAsync(
        BankHubPaymentRecord record,
        string? sourceStatementRef,
        string? targetStatementRef,
        string? note)
    {
        return _gpayOrderSync.ApplyPaymentAsync(new GpayPaymentSyncRequest
        {
            Reference = record.Reference,
            TransactionReference = record.TransactionReference,
            SourceStatementRef = sourceStatementRef,
            TargetStatementRef = targetStatementRef,
            HubStatus = record.Status,
            Note = note ?? record.ErrorMessage ?? record.ResultDescription,
            Amount = record.Amount
        });
    }
}
