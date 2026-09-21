using GPay.Banking.Domain.Dtos;

using GPay.Banking;

namespace GPay.Banking.Domain;

/// <summary>
/// Resolves bank capability adapters by bank code.
/// </summary>
public interface IBankCapabilityResolver
{
    IAccountVerificationService GetAccountVerification(BankCode bank);
    IInstantPaymentService GetInstantPayment(BankCode bank);
    IBalanceService GetBalance(BankCode bank);
    IStatementService GetStatement(BankCode bank);
    INotificationService GetNotification(BankCode bank);
    IPaymentCallbackService GetPaymentCallback(BankCode bank);
}



