namespace GPay.Banking.BankHub;

public class UploadAvsBatchResultDto
{
    public Guid BatchId { get; set; }
    public int TotalRows { get; set; }
    public int SegmentCount { get; set; }
    public string Status { get; set; } = "Processing";
}
