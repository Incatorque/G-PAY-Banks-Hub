namespace GPay.Banking.Domain;

/// <summary>Data included in the internal email sent after a payment initiation outcome.</summary>
[Serializable]
public sealed class GpayPaymentInitiatedNotification
{
    public string Bank { get; init; } = string.Empty;

    public string Route { get; init; } = "API";

    public string Rail { get; init; } = string.Empty;

    public string OrderNumber { get; init; } = string.Empty;

    public decimal Amount { get; init; }

    public string Currency { get; init; } = string.Empty;

    public string FromAccountNumber { get; init; } = string.Empty;

    public string ToAccountNumber { get; init; } = string.Empty;

    public string Reference { get; init; } = string.Empty;

    public string Status { get; init; } = string.Empty;

    public bool IsError { get; init; }

    public string? ErrorCode { get; init; }

    public string? ErrorMessage { get; init; }

    public bool IsStatusCheck { get; init; }

    public string? StatusDescription { get; init; }

    public string? TransactionReference { get; init; }
}
