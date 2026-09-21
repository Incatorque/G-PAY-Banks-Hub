namespace GPay.Banking.BankHub;

public class SubmitAvsBatchItemDto
{
    public int RowNumber { get; set; }
    public VerifyAccountRequestDto Request { get; set; } = new();
}
