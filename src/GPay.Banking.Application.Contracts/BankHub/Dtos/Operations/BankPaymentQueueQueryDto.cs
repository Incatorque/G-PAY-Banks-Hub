namespace GPay.Banking.BankHub;

public sealed class BankPaymentQueueQueryDto
{
    public string? Bank { get; set; }
    /// <summary>Optional filter: queued or submitted.</summary>
    public string? Status { get; set; }
    public string? Search { get; set; }
    public int Skip { get; set; }
    public int Take { get; set; } = 50;
}
