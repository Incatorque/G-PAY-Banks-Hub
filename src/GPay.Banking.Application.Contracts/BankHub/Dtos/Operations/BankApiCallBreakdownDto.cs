namespace GPay.Banking.BankHub;

public sealed class BankApiCallBreakdownDto
{
    public string Label { get; set; } = string.Empty;
    public int Total { get; set; }
    public int Failed { get; set; }
    public double SuccessRate { get; set; }
    public double? AverageDurationMs { get; set; }
}
