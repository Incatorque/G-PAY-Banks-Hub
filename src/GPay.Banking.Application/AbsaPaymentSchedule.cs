using GPay.Banking.Domain;
using GPay.Banking.Helpers;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Volo.Abp;

namespace GPay.Banking;

public interface IAbsaPaymentSchedule
{
    Task<string?> GetPaafDeferralReasonAsync(
        decimal amount,
        string? payerBank,
        string? beneficiaryBank,
        CancellationToken cancellationToken = default);
}

public sealed class PaymentDeferredException(string message) : UserFriendlyException(message);

internal sealed class AbsaPaymentSchedule(
    HttpClient httpClient,
    IGpayOrderSync gpayOrderSync,
    IBankPaymentConfigurationProvider paymentConfigurationProvider,
    IMemoryCache cache,
    IOptions<SouthAfricanHolidayOptions> options,
    ILogger<AbsaPaymentSchedule> logger) : IAbsaPaymentSchedule
{
    private static readonly ConcurrentDictionary<string, Lazy<Task<HashSet<DateOnly>>>> HolidayRequests = new();
    private static readonly TimeZoneInfo SouthAfricaTimeZone = ResolveSouthAfricaTimeZone();
    private readonly SouthAfricanHolidayOptions _options = options.Value;

    public async Task<string?> GetPaafDeferralReasonAsync(
        decimal amount,
        string? payerBank,
        string? beneficiaryBank,
        CancellationToken cancellationToken = default)
    {
        var southAfricaTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, SouthAfricaTimeZone);
        var isPublicHoliday = await IsPublicHolidayAsync(southAfricaTime.Date, cancellationToken);
        var dayOfWeek = isPublicHoliday
            ? 8
            : southAfricaTime.DayOfWeek == DayOfWeek.Sunday
                ? 7
                : (int)southAfricaTime.DayOfWeek;
        var sourceBankKey = NormalizeBankToken(payerBank);
        var targetBankKey = NormalizeBankToken(beneficiaryBank);
        var isOnUs = sourceBankKey.Length > 0 &&
            string.Equals(sourceBankKey, targetBankKey, StringComparison.OrdinalIgnoreCase);
        logger.LogInformation(
            "Evaluated Absa PAAF transaction type: sourceBank={SourceBank} ({SourceBankKey}), targetBank={TargetBank} ({TargetBankKey}), onUs={OnUs}. On-us rules apply only when both bank identifiers are present and match.",
            payerBank,
            sourceBankKey,
            beneficiaryBank,
            targetBankKey,
            isOnUs);
        var configuration = await paymentConfigurationProvider.GetAsync("ABSA", cancellationToken);
        var paafRail = configuration?.Rails.FirstOrDefault(rail =>
            rail.IsActive && string.Equals(
                string.Concat(rail.RailCode.Where(char.IsLetterOrDigit)),
                PaymentRailRouting.AbsaPaaf,
                StringComparison.OrdinalIgnoreCase));

        if (paafRail is null)
        {
            return "No active PAAF rail is configured for Absa in GPay.";
        }

        var amountRules = paafRail.Rules.Where(rule =>
                rule.DayOfWeek == dayOfWeek &&
                rule.IsOnUs == isOnUs &&
                (!rule.MinAmount.HasValue || amount >= rule.MinAmount.Value) &&
                (!rule.MaxAmount.HasValue || amount <= rule.MaxAmount.Value))
            .ToList();

        var matchedRule = amountRules.FirstOrDefault(rule => IsWithinWindow(southAfricaTime.TimeOfDay, rule));
        if (matchedRule is not null)
        {
            logger.LogInformation(
                "Absa PAAF schedule matched BankTime rule {RuleId}. Amount={Amount}, OnUs={OnUs}, Day={Day}, SouthAfricaTime={LocalTime}, Window={StartTime}-{EndTime}, MinAmount={MinAmount}, MaxAmount={MaxAmount}.",
                matchedRule.RuleId,
                amount,
                isOnUs,
                PaymentRuleDiagnostics.DayName(matchedRule.DayOfWeek),
                southAfricaTime,
                matchedRule.StartTime,
                matchedRule.EndTime,
                matchedRule.MinAmount,
                matchedRule.MaxAmount);
            return null;
        }

        if (amountRules.Count == 0)
        {
            logger.LogWarning(
                "Absa PAAF schedule found no matching active BankTime rule. Amount={Amount}, OnUs={OnUs}, Day={Day}, SouthAfricaTime={LocalTime}.",
                amount,
                isOnUs,
                PaymentRuleDiagnostics.DayName(dayOfWeek),
                southAfricaTime);
            return $"No active Absa PAAF time and amount rule is configured for this payment on {southAfricaTime:dddd}.";
        }

        var configuredWindows = string.Join(", ", amountRules
            .Select(rule => $"{rule.StartTime:hh\\:mm}–{rule.EndTime:hh\\:mm}")
            .Distinct());
        logger.LogWarning(
            "Absa PAAF schedule deferred payment because local time {LocalTime} is outside matching BankTime rules {RuleIds} with windows {Windows}. Amount={Amount}, OnUs={OnUs}, Day={Day}.",
            southAfricaTime,
            string.Join(",", amountRules.Select(rule => rule.RuleId)),
            configuredWindows,
            amount,
            isOnUs,
            PaymentRuleDiagnostics.DayName(dayOfWeek));
        return $"The payment is outside Absa's configured PAAF processing window(s): {configuredWindows} South African time.";
    }

    private static string NormalizeBankToken(string? bankName)
    {
        if (string.IsNullOrWhiteSpace(bankName)) return string.Empty;
        var token = string.Concat(bankName.Where(char.IsLetterOrDigit)).ToUpperInvariant();
        if (token.Contains("ABSA", StringComparison.Ordinal)) return "ABSA";
        if (token.Contains("FNB", StringComparison.Ordinal) || token.Contains("FIRSTNATIONAL", StringComparison.Ordinal)) return "FNB";
        if (token.Contains("NEDBANK", StringComparison.Ordinal)) return "NEDBANK";
        return token;
    }

    private static bool IsWithinWindow(TimeSpan timeOfDay, BankPaymentTimeRule rule)
    {
        var isEndOfDay = rule.EndTime >= TimeSpan.FromDays(1) - TimeSpan.FromTicks(1);
        return timeOfDay >= rule.StartTime &&
            (isEndOfDay ? timeOfDay <= rule.EndTime : timeOfDay < rule.EndTime);
    }

    private async Task<bool> IsPublicHolidayAsync(DateTime date, CancellationToken cancellationToken)
    {
        try
        {
            var holidays = await GetPublicHolidaysAsync(date.Year, cancellationToken);
            if (holidays.Contains(DateOnly.FromDateTime(date)))
            {
                return true;
            }
        }
        catch (Exception ex) when (ex is not OperationCanceledException || !cancellationToken.IsCancellationRequested)
        {
            logger.LogWarning(ex,
                "Unable to load South African public holidays from {HolidayApi}; falling back to GPay's holiday calendar.",
                _options.ApiBaseUrl);
        }

        // Retain GPay's calendar as a manual override and as a fallback if the public API is unavailable.
        return await gpayOrderSync.IsPublicHolidayAsync(date, cancellationToken);
    }

    private Task<HashSet<DateOnly>> GetPublicHolidaysAsync(int year, CancellationToken cancellationToken)
    {
        var cacheKey = $"SouthAfricanPublicHolidays:{_options.CountryCode}:{year}";
        if (cache.TryGetValue(cacheKey, out HashSet<DateOnly>? cached) && cached is not null)
        {
            return Task.FromResult(cached);
        }

        var request = HolidayRequests.GetOrAdd(cacheKey, _ => new Lazy<Task<HashSet<DateOnly>>>(async () =>
        {
            var baseUrl = _options.ApiBaseUrl.TrimEnd('/') + "/";
            var url = $"{baseUrl}Holidays/{Uri.EscapeDataString(_options.CountryCode)}/{year}";
            var holidays = await httpClient.GetFromJsonAsync<List<PublicHolidayResponse>>(url, cancellationToken)
                ?? throw new InvalidOperationException("The public holiday API returned an empty response.");

            var dates = holidays
                .Where(holiday => holiday.Date.HasValue &&
                    (holiday.NationalHoliday || holiday.HolidayTypes?.Any(type =>
                        string.Equals(type, "Public", StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(type, "Bank", StringComparison.OrdinalIgnoreCase)) == true))
                .Select(holiday => DateOnly.FromDateTime(holiday.Date!.Value))
                .ToHashSet();

            cache.Set(cacheKey, dates, TimeSpan.FromHours(Math.Max(1, _options.CacheHours)));
            return dates;
        }, LazyThreadSafetyMode.ExecutionAndPublication));

        return AwaitHolidayRequestAsync(cacheKey, request, cancellationToken);
    }

    private async Task<HashSet<DateOnly>> AwaitHolidayRequestAsync(
        string cacheKey,
        Lazy<Task<HashSet<DateOnly>>> request,
        CancellationToken cancellationToken)
    {
        try
        {
            return await request.Value.WaitAsync(cancellationToken);
        }
        finally
        {
            ((ICollection<KeyValuePair<string, Lazy<Task<HashSet<DateOnly>>>>>)HolidayRequests)
                .Remove(new KeyValuePair<string, Lazy<Task<HashSet<DateOnly>>>>(cacheKey, request));
        }
    }

    private static TimeZoneInfo ResolveSouthAfricaTimeZone()
    {
        try
        {
            return TimeZoneInfo.FindSystemTimeZoneById("South Africa Standard Time");
        }
        catch (TimeZoneNotFoundException)
        {
            return TimeZoneInfo.FindSystemTimeZoneById("Africa/Johannesburg");
        }
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
