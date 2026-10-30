using GPay.Banking.Domain;
using GPay.Banking.Domain.Dtos;
using GPay.Banking.Services.Absa;

namespace GPay.Banking.Services;

/// <summary>
/// Selects the bank-specific payment adapter and forwards normalized payment requests.
/// </summary>
internal sealed class BankRouterService : IBankRouterService
{
    private readonly IBankCapabilityResolver _capabilityResolver;
    private readonly BankPaymentIntakeOptions _intakeOptions;
    private readonly AbsaCapiOptions _absaOptions;

    public BankRouterService(
        IBankCapabilityResolver capabilityResolver,
        IOptions<BankPaymentIntakeOptions> intakeOptions,
        IOptions<AbsaCapiOptions> absaOptions)
    {
        _capabilityResolver = capabilityResolver;
        _intakeOptions = intakeOptions.Value;
        _absaOptions = absaOptions.Value;
    }

    public Task<ApiResult<InstantPaymentResponse>> SubmitPaymentAsync(
        BankCode bank,
        InstantPaymentRequest request,
        string correlationId,
        CancellationToken cancellationToken = default)
    {
        var service = ResolveInstantPaymentService(bank);
        var routedRequest = ApplyInternalOrderDefaults(bank, request) with
        {
            PaymentRail = ResolvePaymentRail(bank, request.PaymentRail)
        };

        return service.PayAsync(routedRequest, correlationId, cancellationToken);
    }

    public Task<ApiResult<PaymentStatusResponse>> GetPaymentStatusAsync(
        BankCode bank,
        PaymentStatusRequest request,
        string correlationId,
        CancellationToken cancellationToken = default)
    {
        var service = ResolveInstantPaymentService(bank);

        return service.GetStatusAsync(request, correlationId, cancellationToken);
    }

    private IInstantPaymentService ResolveInstantPaymentService(BankCode bank) =>
        _capabilityResolver.GetInstantPayment(bank);

    private static string ResolvePaymentRail(BankCode bank, string? requestedRail)
    {
        if (bank == BankCode.Absa && !string.IsNullOrWhiteSpace(requestedRail))
        {
            var normalizedRail = requestedRail.Trim().ToUpperInvariant();
            if (normalizedRail is "RPP" or "IIP" or "PAAF")
            {
                return normalizedRail;
            }

            throw new NotSupportedException($"Absa payment rail '{requestedRail}' is not supported.");
        }

        return bank switch
        {
            BankCode.Absa => "RPP",
            BankCode.Fnb => "3PIM",
            _ => throw new NotSupportedException(
                $"No instant payment rail is configured for bank '{bank}'.")
        };
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
