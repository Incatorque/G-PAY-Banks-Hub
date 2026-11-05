namespace GPay.Banking;

public static class BankHubOrderStatuses
{
    public const string ReadyForPayment = "BankHub - PRS";

    public static bool IsReadyForPayment(string? status) =>
        string.Equals(status, ReadyForPayment, StringComparison.OrdinalIgnoreCase);
}
