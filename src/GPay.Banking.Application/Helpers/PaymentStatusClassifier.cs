namespace GPay.Banking.Helpers;

internal static class PaymentStatusClassifier
{
    private static readonly string[] FailureMarkers =
    ["FAIL", "ERROR", "DUPLICATE", "REJECT", "REVERS", "UNSUCCESSFUL", "CANCEL"];

    public static bool IsFailure(string? status) =>
        FailureMarkers.Any(marker => status?.Contains(marker, StringComparison.OrdinalIgnoreCase) == true);
}
