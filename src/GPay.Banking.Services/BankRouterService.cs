using GPay.Banking.Domain;
using GPay.Banking.Domain.Dtos;
using GPay.Banking.Services.Absa;

namespace GPay.Banking.Services;

/// <summary>
/// Selects the bank-specific payment adapter and forwards normalized payment requests.
/// </summary>
internal sealed class BankRouterService(
    IBankCapabilityResolver capabilityResolver,
    IOptions<BankPaymentIntakeOptions> intakeOptions,
    IOptions<AbsaCapiOptions> absaOptions) : IBankRouterService
{
    private readonly BankPaymentIntakeOptions _intakeOptions = intakeOptions.Value;
    private readonly AbsaCapiOptions _absaOptions = absaOptions.Value;

    public Task<ApiResult<InstantPaymentResponse>> SubmitPaymentAsync(
        BankCode bank,
        InstantPaymentRequest request,
        string correlationId,
        CancellationToken cancellationToken = default)
    {
        var service = capabilityResolver.GetInstantPayment(bank);
        var routedRequest = ApplyInternalOrderDefaults(bank, request) with
        {
            PaymentRail = PaymentRailRouting.ForBankRequest(bank, request.PaymentRail)
        };

        return service.PayAsync(routedRequest, correlationId, cancellationToken);
    }

    public Task<ApiResult<PaymentStatusResponse>> GetPaymentStatusAsync(
        BankCode bank,
        PaymentStatusRequest request,
        string correlationId,
        CancellationToken cancellationToken = default)
    {
        var service = capabilityResolver.GetInstantPayment(bank);

        return service.GetStatusAsync(request, correlationId, cancellationToken);
    }

    private InstantPaymentRequest ApplyInternalOrderDefaults(
        BankCode bank,
        InstantPaymentRequest request)
    {
        if (request.GpayOrderId is null)
        {
            return request;
        }

        var bankOptions = _intakeOptions.ForBank(bank);
        var configuredSourceAccount = StringValueResolver.FirstNonEmpty(
            bankOptions.SourceAccountNumber,
            bank == BankCode.Absa ? _absaOptions.DefaultSourceAccountNumber : null);
        var configuredSourceShortName = StringValueResolver.FirstNonEmpty(
            bankOptions.SourceShortName,
            bank == BankCode.Absa ? _absaOptions.DefaultSourceShortName : null);

        return request with
        {
            FromAccountNumber = StringValueResolver.FirstNonEmpty(configuredSourceAccount, request.FromAccountNumber)!,
            FromShortName = StringValueResolver.FirstNonEmpty(configuredSourceShortName, request.FromShortName)
        };
    }
}
