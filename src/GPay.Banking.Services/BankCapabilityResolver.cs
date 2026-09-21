using GPay.Banking;
using GPay.Banking.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace GPay.Banking.Services;

public sealed class BankCapabilityResolver : IBankCapabilityResolver
{
    private readonly IServiceProvider _serviceProvider;

    public BankCapabilityResolver(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IAccountVerificationService GetAccountVerification(BankCode bank) => Resolve<IAccountVerificationService>(bank);
    public IInstantPaymentService GetInstantPayment(BankCode bank) => Resolve<IInstantPaymentService>(bank);
    public IBalanceService GetBalance(BankCode bank) => Resolve<IBalanceService>(bank);
    public IStatementService GetStatement(BankCode bank) => Resolve<IStatementService>(bank);
    public INotificationService GetNotification(BankCode bank) => Resolve<INotificationService>(bank);
    public IPaymentCallbackService GetPaymentCallback(BankCode bank) => ResolvePaymentCallback(bank);

    private IPaymentCallbackService ResolvePaymentCallback(BankCode bank)
    {
        if (bank != BankCode.Absa)
        {
            throw new NotSupportedException($"Payment callbacks for bank '{bank}' are not registered.");
        }

        return _serviceProvider.GetRequiredService<IPaymentCallbackService>();
    }

    private T Resolve<T>(BankCode bank) where T : notnull
    {
        if (bank != BankCode.Absa)
        {
            throw new NotSupportedException($"Bank '{bank}' is not registered. Only Absa API is available currently.");
        }
        return _serviceProvider.GetRequiredService<T>();
    }
}
