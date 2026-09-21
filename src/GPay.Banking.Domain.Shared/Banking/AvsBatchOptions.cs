namespace GPay.Banking;

public sealed class AvsBatchOptions
{
    public const string SectionName = "AvsBatch";
    public int MaxItems { get; set; } = 20_000;
    public int SegmentSize { get; set; } = 5_000;
    public int DegreeOfParallelism { get; set; } = 5;
}



