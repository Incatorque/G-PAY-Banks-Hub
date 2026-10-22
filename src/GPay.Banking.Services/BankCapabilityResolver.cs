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

    public IInstantPaymentService GetInstantPayment(string bank) =>
        Resolve<IInstantPaymentService>(bank);

    public IBalanceService GetBalance(BankCode bank) => Resolve<IBalanceService>(bank);

    public IStatementService GetStatement(BankCode bank) => Resolve<IStatementService>(bank);

    public INotificationService GetNotification(BankCode bank) => Resolve<INotificationService>(bank);

    public IPaymentCallbackService GetPaymentCallback(BankCode bank) => Resolve<IPaymentCallbackService>(bank);

    public IPaymentCallbackRegistrationService GetPaymentCallbackRegistration(BankCode bank) =>
        Resolve<IPaymentCallbackRegistrationService>(bank);

    private T Resolve<T>(BankCode bank) where T : notnull
    {
        return Resolve<T>(bank.ToString());
    }

    private T Resolve<T>(string bank) where T : notnull
    {
        if (string.IsNullOrWhiteSpace(bank))
        {
            throw new NotSupportedException("A bank key is required to resolve the bank capability.");
        }

        var key = bank.Trim();
        var service = _serviceProvider.GetKeyedService<T>(key);

        return service ?? throw new NotSupportedException(
            $"Bank capability '{typeof(T).Name}' is not registered for '{bank}'.");
    }
}
