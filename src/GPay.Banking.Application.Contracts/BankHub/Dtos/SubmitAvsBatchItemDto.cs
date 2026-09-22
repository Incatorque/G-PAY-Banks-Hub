namespace GPay.Banking.BankHub;

/// <summary>
/// One row inside an AVS batch submit payload.
/// </summary>
public class SubmitAvsBatchItemDto
{
    /// <summary>
    /// 1-based (or client-defined) row number used for ordering and dashboard display.
    /// </summary>
    public int RowNumber { get; set; }

    /// <summary>
    /// Full single-account verification request for this row.
    /// </summary>
    public VerifyAccountRequestDto Request { get; set; } = new();
}
