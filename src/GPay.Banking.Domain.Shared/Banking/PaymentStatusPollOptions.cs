namespace GPay.Banking;

/// <summary>Controls polling of open payments across configured bank adapters.</summary>
public sealed class PaymentStatusPollOptions
{
    public const string SectionName = "BankStatusPoll";

    public bool Enabled { get; set; } = true;

    public int PeriodSeconds { get; set; } = 60;

    public int BatchSize { get; set; } = 25;
}
