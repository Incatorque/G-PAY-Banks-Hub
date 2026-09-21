namespace GPay.Banking.BankHub;

public class SubmitAvsBatchRequestDto
{
    public string? FileName { get; set; }
    public string? Reference { get; set; }
    public Guid? ApiClientId { get; set; }
    public List<SubmitAvsBatchItemDto> Items { get; set; } = new();
}
