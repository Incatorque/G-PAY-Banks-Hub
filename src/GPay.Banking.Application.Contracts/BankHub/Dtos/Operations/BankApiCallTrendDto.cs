namespace GPay.Banking.BankHub;

public sealed class BankApiCallTrendDto
{
    public DateTime Day { get; set; }
    public int Total { get; set; }
    public int Failed { get; set; }
    public double? AverageDurationMs { get; set; }
}
