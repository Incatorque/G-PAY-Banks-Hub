namespace GPay.Banking;

/// <summary>Bank payment rail definitions and time/amount rules loaded from the GPay database.</summary>
public sealed record BankPaymentConfiguration(
    Guid BankId,
    string BankName,
    IReadOnlyList<BankPaymentRailConfiguration> Rails);

public sealed record BankPaymentRailConfiguration(
    Guid RailId,
    string Name,
    string RailCode,
    bool IsActive,
    IReadOnlyList<BankPaymentTimeRule> Rules);

/// <summary>Maps to an active GPay BankTime row; DayOfWeek 8 represents a public holiday.</summary>
public sealed record BankPaymentTimeRule(
    Guid RuleId,
    int DayOfWeek,
    TimeSpan StartTime,
    TimeSpan EndTime,
    bool IsInternal,
    bool IsOnUs,
    decimal? MinAmount,
    decimal? MaxAmount);
