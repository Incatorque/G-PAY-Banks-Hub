namespace GPay.Banking.BankHub;

public sealed class BankApiCallQueryDto
{
    public string? Bank { get; set; }
    public string? Direction { get; set; }
    public string? Operation { get; set; }
    public bool? Success { get; set; }
    public string? Search { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public int Skip { get; set; }
    public int Take { get; set; } = 25;
}
