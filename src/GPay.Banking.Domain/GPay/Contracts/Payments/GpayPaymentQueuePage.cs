namespace GPay.Banking.Domain;

/// <summary>A page of active GPay orders waiting in the Bank Hub handoff.</summary>
public sealed class GpayPaymentQueuePage
{
    public int TotalCount { get; init; }
    public IReadOnlyList<string> Banks { get; init; } = [];
    public IReadOnlyList<GpayPaymentQueueItem> Items { get; init; } = [];
}

public sealed class GpayPaymentQueueItem
{
    public Guid OrderId { get; init; }
    public string OrderNumber { get; init; } = string.Empty;
    public string Reference { get; init; } = string.Empty;
    public string Bank { get; init; } = string.Empty;
    public string Route { get; init; } = string.Empty;
    public string Rail { get; init; } = string.Empty;
    public string Status { get; init; } = string.Empty;
    public DateTime OrderDate { get; init; }
    public decimal Amount { get; init; }
    public string Currency { get; init; } = string.Empty;
}
