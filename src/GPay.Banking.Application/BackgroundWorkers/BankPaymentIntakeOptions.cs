using GPay.Banking;

namespace GPay.Banking.BackgroundWorkers;

/// <summary>Shared PRS payment intake configuration, with bank-specific enablement and source details.</summary>
public sealed class BankPaymentIntakeOptions
{
    public const string SectionName = "BankHub";

    public int PollPeriodSeconds { get; set; } = 120;

    public int BatchSize { get; set; } = 25;

    public BankPaymentIntakeBankOptions Absa { get; set; } = new();

    public BankPaymentIntakeBankOptions Fnb { get; set; } = new();

    public BankPaymentIntakeBankOptions Nedbank { get; set; } = new();

    public BankPaymentIntakeBankOptions ForBank(BankCode bank) => bank switch
    {
        BankCode.Absa => Absa,
        BankCode.Fnb => Fnb,
        BankCode.Nedbank => Nedbank,
        _ => throw new NotSupportedException($"PRS payment intake is not configured for '{bank}'.")
    };

    public IReadOnlyCollection<BankCode> EnabledBanks =>
        Enum.GetValues<BankCode>().Where(bank => ForBank(bank).PayshapEnabled).ToArray();
}

public sealed class BankPaymentIntakeBankOptions
{
    public bool PayshapEnabled { get; set; }

    public string? SourceAccountNumber { get; set; }

    public string? SourceShortName { get; set; }
}
