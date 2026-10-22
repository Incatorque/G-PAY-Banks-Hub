using GPay.Banking.Domain;
using GPay.Banking.Domain.Dtos;
using GPay.Banking.Services.Absa;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace GPay.Banking.Services;

/// <summary>
/// Selects the bank-specific payment adapter and forwards normalized payment requests.
/// </summary>
public sealed class BankRouterService : IBankRouterService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly BankPaymentIntakeOptions _intakeOptions;
    private readonly AbsaCapiOptions _absaOptions;

    public BankRouterService(
        IServiceProvider serviceProvider,
        IOptions<BankPaymentIntakeOptions> intakeOptions,
        IOptions<AbsaCapiOptions> absaOptions)
    {
        _serviceProvider = serviceProvider;
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
            PaymentRail = ResolvePaymentRail(bank)
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

    private IInstantPaymentService ResolveInstantPaymentService(BankCode bank)
    {
        var serviceKey = bank switch
        {
            BankCode.Absa => BankCode.Absa.ToString(),
            BankCode.Fnb => BankCode.Fnb.ToString(),
            BankCode.Nedbank => BankCode.Nedbank.ToString(),
            _ => throw new NotSupportedException($"Instant payments are not supported for bank '{bank}'.")
        };

        return _serviceProvider.GetKeyedService<IInstantPaymentService>(serviceKey)
            ?? throw new NotSupportedException(
                $"No instant payment adapter is registered for bank '{bank}'.");
    }

    private static string ResolvePaymentRail(BankCode bank)
    {
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

        var bankOptions = bank switch
        {
            BankCode.Absa => _intakeOptions.Absa,
            BankCode.Fnb => _intakeOptions.Fnb,
            BankCode.Nedbank => _intakeOptions.Nedbank,
            _ => throw new NotSupportedException($"Payment intake is not configured for bank '{bank}'.")
        };

        var configuredSourceAccount = bank == BankCode.Absa
            ? FirstConfigured(bankOptions.SourceAccountNumber, _absaOptions.DefaultSourceAccountNumber)
            : FirstConfigured(bankOptions.SourceAccountNumber);
        var configuredSourceShortName = bank == BankCode.Absa
            ? FirstConfigured(bankOptions.SourceShortName, _absaOptions.DefaultSourceShortName)
            : FirstConfigured(bankOptions.SourceShortName);

        return request with
        {
            FromAccountNumber = FirstConfigured(configuredSourceAccount, request.FromAccountNumber)!,
            FromShortName = FirstConfigured(configuredSourceShortName, request.FromShortName)
        };
    }

    private static string? FirstConfigured(params string?[] values)
    {
        return values.FirstOrDefault(value => !string.IsNullOrWhiteSpace(value))?.Trim();
    }
}
