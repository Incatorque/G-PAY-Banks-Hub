namespace GPay.Banking.Services.Absa;

/// <summary>
/// How often the host re-queries Absa for payments that are not in a terminal status.
/// </summary>
public sealed class AbsaStatusPollOptions
{
    /// <summary>Configuration section name.</summary>
    public const string SectionName = "AbsaStatusPoll";

    /// <summary>When false, the background worker stays idle.</summary>
    public bool Enabled { get; set; } = true;

    /// <summary>Seconds between polls of the same payment.</summary>
    public int PeriodSeconds { get; set; } = 60;

    /// <summary>Maximum open payments queried on each run.</summary>
    public int BatchSize { get; set; } = 25;
}
