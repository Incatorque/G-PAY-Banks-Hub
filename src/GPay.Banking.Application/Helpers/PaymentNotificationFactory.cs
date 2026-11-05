using GPay.Banking.Domain;

namespace GPay.Banking.Helpers;

internal static class PaymentNotificationFactory
{
    public static GpayPaymentInitiatedNotification StatusCheckError(
        GpayPaymentStatusCandidate order,
        string errorCode,
        string errorMessage) => StatusCheckError(
            order.Bank,
            order.OrderNumber,
            order.Reference,
            order.Amount,
            order.Currency,
            order.FromAccountNumber,
            order.ToAccountNumber,
            errorCode,
            errorMessage);

    public static GpayPaymentInitiatedNotification StatusCheckError(
        string bank,
        string orderNumber,
        string reference,
        decimal amount,
        string currency,
        string? fromAccountNumber,
        string? toAccountNumber,
        string errorCode,
        string errorMessage) => StatusCheckResult(
            bank,
            orderNumber,
            amount,
            currency,
            fromAccountNumber,
            toAccountNumber,
            reference,
            PaymentStatuses.Error,
            isError: true,
            errorCode,
            errorMessage,
            errorMessage);

    public static GpayPaymentInitiatedNotification StatusCheckResult(
        string bank,
        string orderNumber,
        decimal amount,
        string currency,
        string? fromAccountNumber,
        string? toAccountNumber,
        string reference,
        string status,
        bool isError,
        string? errorCode,
        string? errorMessage,
        string? statusDescription,
        string? transactionReference = null) => new()
        {
            Bank = bank,
            OrderNumber = orderNumber,
            Amount = amount,
            Currency = currency,
            FromAccountNumber = fromAccountNumber ?? string.Empty,
            ToAccountNumber = toAccountNumber ?? string.Empty,
            Reference = reference,
            Status = status,
            IsStatusCheck = true,
            IsError = isError,
            ErrorCode = errorCode,
            ErrorMessage = errorMessage,
            StatusDescription = statusDescription,
            TransactionReference = transactionReference
        };
}
