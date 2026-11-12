using System.Collections.Concurrent;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using GPay.Banking.Domain;
using GPay.Banking.Helpers;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Volo.Abp;

namespace GPay.Banking;

public interface IBankPaymentSchedule
{
    Task<string?> GetDeferralReasonAsync(
        decimal amount,
        string? payerBank,
        string? paymentRail,
        string? beneficiaryBank,
        CancellationToken cancellationToken = default);
}

public sealed class PaymentDeferredException(string message) : UserFriendlyException(message);

/// <summary>Enforces each bank's database-configured rail, amount, on-us, day, and execution-window rules.</summary>
internal sealed class BankPaymentSchedule(
    HttpClient httpClient,
    IGpayOrderSync gpayOrderSync,
    IBankPaymentConfigurationProvider paymentConfigurationProvider,
    IMemoryCache cache,
    IOptions<SouthAfricanHolidayOptions> options,
    ILogger<BankPaymentSchedule> logger) : IBankPaymentSchedule
{
    private static readonly ConcurrentDictionary<string, Lazy<Task<HashSet<DateOnly>>>> HolidayRequests = new();
    private static readonly TimeZoneInfo SouthAfricaTimeZone = ResolveSouthAfricaTimeZone();
    private readonly SouthAfricanHolidayOptions _options = options.Value;

    public async Task<string?> GetDeferralReasonAsync(
        decimal amount,
        string? payerBank,
        string? paymentRail,
        string? beneficiaryBank,
        CancellationToken cancellationToken = default)
    {
        var bank = NormalizeBank(payerBank);
        var railToken = Normalize(paymentRail);

        // Absa's RPP has no channel cut-off; the configured amount check runs in PaymentRailRouting.
        if (bank == "ABSA" && railToken == "RPP") return null;
        if (bank.Length == 0 || railToken.Length == 0)
            return "The bank or payment rail could not be resolved for schedule validation.";

        var configuration = await paymentConfigurationProvider.GetAsync(payerBank!, cancellationToken);
        var rail = configuration?.Rails.FirstOrDefault(item => item.IsActive &&
            (Normalize(item.RailCode) == railToken || Normalize(item.Name) == railToken));
        if (rail is null)
            return $"No active {paymentRail} rail is configured for {payerBank} in GPay.";

        var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, SouthAfricaTimeZone);
        var isHoliday = await IsPublicHolidayAsync(now.Date, cancellationToken);
        var day = isHoliday ? 8 : now.DayOfWeek == DayOfWeek.Sunday ? 7 : (int)now.DayOfWeek;
        var sourceBank = NormalizeBank(payerBank);
        var targetBank = NormalizeBank(beneficiaryBank);
        var isOnUs = sourceBank.Length > 0 && sourceBank == targetBank;
        var eligibleRules = rail.Rules.Where(rule =>
            (rule.DayOfWeek == 0 || rule.DayOfWeek == day) &&
            rule.IsOnUs == isOnUs &&
            (!rule.MinAmount.HasValue || amount >= rule.MinAmount.Value) &&
            (!rule.MaxAmount.HasValue || amount <= rule.MaxAmount.Value)).ToList();

        var matched = eligibleRules.FirstOrDefault(rule => IsWithinWindow(now.TimeOfDay, rule));
        if (matched is not null)
        {
            logger.LogInformation(
                "Payment schedule allowed bank={Bank} rail={Rail} rule={RuleId} amount={Amount} onUs={OnUs} day={Day} localTime={LocalTime} window={StartTime}-{EndTime} min={MinAmount} max={MaxAmount}.",
                payerBank, paymentRail, matched.RuleId, amount, isOnUs, PaymentRuleDiagnostics.DayName(day), now,
                matched.StartTime, matched.EndTime, matched.MinAmount, matched.MaxAmount);
            return null;
        }

        if (eligibleRules.Count == 0)
        {
            logger.LogWarning(
                "Payment schedule has no matching active rule bank={Bank} rail={Rail} amount={Amount} onUs={OnUs} day={Day} localTime={LocalTime}.",
                payerBank, paymentRail, amount, isOnUs, PaymentRuleDiagnostics.DayName(day), now);
            return $"No active {paymentRail} amount and schedule rule is configured for this { (isOnUs ? "on-us" : "off-us") } payment on {now:dddd}.";
        }

        var windows = string.Join(", ", eligibleRules.Select(rule => $"{rule.StartTime:hh\\:mm}-{rule.EndTime:hh\\:mm}").Distinct());
        logger.LogInformation(
            "Payment deferred by configured schedule bank={Bank} rail={Rail} amount={Amount} onUs={OnUs} day={Day} localTime={LocalTime} windows={Windows} ruleIds={RuleIds}.",
            payerBank, paymentRail, amount, isOnUs, PaymentRuleDiagnostics.DayName(day), now, windows,
            string.Join(",", eligibleRules.Select(rule => rule.RuleId)));
        return $"The payment is outside {payerBank}'s configured {paymentRail} processing window(s): {windows} South African time.";
    }

    private static string NormalizeBank(string? value)
    {
        var bank = Normalize(value);
        if (bank.Contains("ABSA", StringComparison.Ordinal)) return "ABSA";
        if (bank is "FNB" or "FIRSTRAND" || bank.Contains("FIRSTNATIONAL", StringComparison.Ordinal)) return "FNB";
        if (bank.Contains("NEDBANK", StringComparison.Ordinal)) return "NEDBANK";
        if (bank.Contains("CAPITEC", StringComparison.Ordinal)) return "CAPITEC";
        return bank.Replace("BANKLIMITED", string.Empty, StringComparison.Ordinal)
            .Replace("BANKLTD", string.Empty, StringComparison.Ordinal)
            .Replace("BANK", string.Empty, StringComparison.Ordinal)
            .Replace("LIMITED", string.Empty, StringComparison.Ordinal)
            .Replace("LTD", string.Empty, StringComparison.Ordinal);
    }

    private static string Normalize(string? value) => string.IsNullOrWhiteSpace(value)
        ? string.Empty
        : string.Concat(value.Where(char.IsLetterOrDigit)).ToUpperInvariant();

    private static bool IsWithinWindow(TimeSpan time, BankPaymentTimeRule rule) =>
        time >= rule.StartTime && time <= rule.EndTime;

    private async Task<bool> IsPublicHolidayAsync(DateTime date, CancellationToken cancellationToken)
    {
        try
        {
            if ((await GetPublicHolidaysAsync(date.Year, cancellationToken)).Contains(DateOnly.FromDateTime(date)))
                return true;
        }
        catch (Exception ex) when (ex is not OperationCanceledException || !cancellationToken.IsCancellationRequested)
        {
            logger.LogWarning(ex, "Unable to load South African public holidays from {HolidayApi}; falling back to GPay's holiday calendar.", _options.ApiBaseUrl);
        }

        return await gpayOrderSync.IsPublicHolidayAsync(date, cancellationToken);
    }

    private Task<HashSet<DateOnly>> GetPublicHolidaysAsync(int year, CancellationToken cancellationToken)
    {
        var key = $"SouthAfricanPublicHolidays:{_options.CountryCode}:{year}";
        if (cache.TryGetValue(key, out HashSet<DateOnly>? cached) && cached is not null)
            return Task.FromResult(cached);

        var request = HolidayRequests.GetOrAdd(key, _ => new Lazy<Task<HashSet<DateOnly>>>(async () =>
        {
            var url = $"{_options.ApiBaseUrl.TrimEnd('/')}/Holidays/{Uri.EscapeDataString(_options.CountryCode)}/{year}";
            var holidays = await httpClient.GetFromJsonAsync<List<PublicHolidayResponse>>(url, cancellationToken)
                ?? throw new InvalidOperationException("The public holiday API returned an empty response.");
            var dates = holidays.Where(holiday => holiday.Date.HasValue &&
                    (holiday.NationalHoliday || holiday.HolidayTypes?.Any(type =>
                        string.Equals(type, "Public", StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(type, "Bank", StringComparison.OrdinalIgnoreCase)) == true))
                .Select(holiday => DateOnly.FromDateTime(holiday.Date!.Value)).ToHashSet();
            cache.Set(key, dates, TimeSpan.FromHours(Math.Max(1, _options.CacheHours)));
            return dates;
        }, LazyThreadSafetyMode.ExecutionAndPublication));

        return AwaitHolidayRequestAsync(key, request, cancellationToken);
    }

    private async Task<HashSet<DateOnly>> AwaitHolidayRequestAsync(string key,
        Lazy<Task<HashSet<DateOnly>>> request, CancellationToken cancellationToken)
    {
        try { return await request.Value.WaitAsync(cancellationToken); }
        finally
        {
            ((ICollection<KeyValuePair<string, Lazy<Task<HashSet<DateOnly>>>>>)HolidayRequests)
                .Remove(new KeyValuePair<string, Lazy<Task<HashSet<DateOnly>>>>(key, request));
        }
    }

    private static TimeZoneInfo ResolveSouthAfricaTimeZone()
    {
        try { return TimeZoneInfo.FindSystemTimeZoneById("South Africa Standard Time"); }
        catch (TimeZoneNotFoundException) { return TimeZoneInfo.FindSystemTimeZoneById("Africa/Johannesburg"); }
    }

    private sealed class PublicHolidayResponse
    {
        [JsonPropertyName("date")]
        public DateTime? Date { get; set; }
        [JsonPropertyName("nationalHoliday")]
        public bool NationalHoliday { get; set; }
        [JsonPropertyName("holidayTypes")]
        public List<string>? HolidayTypes { get; set; }
    }
}
