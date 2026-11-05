using GPay.Banking.Domain.Dtos;

namespace GPay.Banking.Helpers;

internal static class PaymentStatusClassifier
{
    private static readonly string[] SuccessStatuses = [PaymentStatuses.Completed, PaymentStatuses.Accepted];
    private static readonly string[] FailureMarkers =
    ["FAIL", "ERROR", "DUPLICATE", "REJECT", "REVERS", "UNSUCCESSFUL", "CANCEL"];

    public static string Normalize(IPaymentResponse response) => response.RawStatusLabel ?? response.Status;

    public static bool IsSuccess(string? status) =>
        SuccessStatuses.Any(value => string.Equals(status, value, StringComparison.OrdinalIgnoreCase));

    public static bool IsFailure(string? status) =>
        FailureMarkers.Any(marker => status?.Contains(marker, StringComparison.OrdinalIgnoreCase) == true);
}
