namespace GPay.Banking.BankHub;

public sealed class BankApiCallPageDto
{
    public int TotalCount { get; set; }
    public int PageCount { get; set; }
    public int FailedCount { get; set; }
    public double SuccessRate { get; set; }
    public double? AverageDurationMs { get; set; }
    public long? MinDurationMs { get; set; }
    public long? MaxDurationMs { get; set; }
    public int OutboundCount { get; set; }
    public int InboundCount { get; set; }
    public int UniqueOrderCount { get; set; }
    public List<BankApiCallListItemDto> Items { get; set; } = [];
    public List<string> Banks { get; set; } = [];
    public List<string> Operations { get; set; } = [];
    public List<BankApiCallTrendDto> DailyActivity { get; set; } = [];
    public List<BankApiCallBreakdownDto> BankBreakdown { get; set; } = [];
    public List<BankApiCallBreakdownDto> StatusBreakdown { get; set; } = [];
    public List<BankApiCallBreakdownDto> OperationBreakdown { get; set; } = [];
    public List<BankApiCallBreakdownDto> DirectionBreakdown { get; set; } = [];
    public List<BankApiCallBreakdownDto> FailureBreakdown { get; set; } = [];
}
