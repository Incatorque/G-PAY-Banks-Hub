using GPay.Banking.Domain.Dtos;

namespace GPay.Banking.Domain;

/// <summary>
/// Resolves bank capability adapters by bank code.
/// </summary>
public interface IBankCapabilityResolver
{
    IAccountVerificationService GetAccountVerification(BankCode bank);
    IInstantPaymentService GetInstantPayment(BankCode bank);
    IInstantPaymentService GetInstantPayment(string bank);
    IBalanceService GetBalance(BankCode bank);
    IStatementService GetStatement(BankCode bank);
    INotificationService GetNotification(BankCode bank);
    IPaymentCallbackService GetPaymentCallback(BankCode bank);
    IPaymentCallbackRegistrationService GetPaymentCallbackRegistration(BankCode bank);
}
