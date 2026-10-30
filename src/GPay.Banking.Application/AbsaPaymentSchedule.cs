using GPay.Banking.Domain;
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
        string? beneficiaryBank,
        CancellationToken cancellationToken = default);
}

public sealed class PaymentDeferredException(string message) : UserFriendlyException(message);

internal sealed class AbsaPaymentSchedule : IAbsaPaymentSchedule
{
    private static readonly ConcurrentDictionary<string, Lazy<Task<HashSet<DateOnly>>>> HolidayRequests = new();

    private readonly HttpClient _httpClient;
    private readonly IGpayOrderSync _gpayOrderSync;
    private readonly IMemoryCache _cache;
    private readonly SouthAfricanHolidayOptions _options;
    private readonly ILogger<AbsaPaymentSchedule> _logger;

    public AbsaPaymentSchedule(
        HttpClient httpClient,
        IGpayOrderSync gpayOrderSync,
        IMemoryCache cache,
        IOptions<SouthAfricanHolidayOptions> options,
        ILogger<AbsaPaymentSchedule> logger)
    {
        _httpClient = httpClient;
        _gpayOrderSync = gpayOrderSync;
        _cache = cache;
        _options = options.Value;
        _logger = logger;
    }

    public async Task<string?> GetPaafDeferralReasonAsync(
        decimal amount,
        string? beneficiaryBank,
        CancellationToken cancellationToken = default)
    {
        // Appendix E has no channel cut-off for on-us PAAF payments.
        if (beneficiaryBank?.IndexOf("ABSA", StringComparison.OrdinalIgnoreCase) >= 0)
        {
            return null;
        }

        var southAfricaTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, GetSouthAfricaTimeZone());
        if (southAfricaTime.DayOfWeek == DayOfWeek.Sunday)
        {
            return "Absa does not process off-us PAAF payments on Sundays.";
        }

        if (await IsPublicHolidayAsync(southAfricaTime.Date, cancellationToken))
        {
            return "Absa does not process off-us PAAF payments on South African public holidays.";
        }

        var isRtgs = amount > 5000000m;
        var cutoff = southAfricaTime.DayOfWeek == DayOfWeek.Saturday
            ? isRtgs ? new TimeSpan(9, 30, 0) : new TimeSpan(10, 30, 0)
            : isRtgs ? new TimeSpan(15, 30, 0) : new TimeSpan(16, 30, 0);

        if (southAfricaTime.TimeOfDay >= cutoff)
        {
            var paymentType = isRtgs ? "RTGS PAAF (above R5,000,000)" : "PAAF up to R5,000,000";
            return $"The Absa off-us {paymentType} cut-off is {cutoff:hh\\:mm} South African time.";
        }

        return null;
    }

    private async Task<bool> IsPublicHolidayAsync(DateTime date, CancellationToken cancellationToken)
    {
        var apiSaysHoliday = false;
        try
        {
            var holidays = await GetPublicHolidaysAsync(date.Year, cancellationToken);
            apiSaysHoliday = holidays.Contains(DateOnly.FromDateTime(date));
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            _logger.LogWarning(ex,
                "Unable to load South African public holidays from {HolidayApi}; falling back to GPay's holiday calendar.",
                _options.ApiBaseUrl);
        }

        // Retain GPay's calendar as a manual override and as a fallback if the public API is unavailable.
        return apiSaysHoliday || await _gpayOrderSync.IsPublicHolidayAsync(date, cancellationToken);
    }

    private Task<HashSet<DateOnly>> GetPublicHolidaysAsync(int year, CancellationToken cancellationToken)
    {
        var cacheKey = $"SouthAfricanPublicHolidays:{_options.CountryCode}:{year}";
        if (_cache.TryGetValue(cacheKey, out HashSet<DateOnly>? cached) && cached is not null)
        {
            return Task.FromResult(cached);
        }

        var request = HolidayRequests.GetOrAdd(cacheKey, _ => new Lazy<Task<HashSet<DateOnly>>>(async () =>
        {
            var baseUrl = _options.ApiBaseUrl.TrimEnd('/') + "/";
            var url = $"{baseUrl}Holidays/{Uri.EscapeDataString(_options.CountryCode)}/{year}";
            var holidays = await _httpClient.GetFromJsonAsync<List<PublicHolidayResponse>>(url, cancellationToken)
                ?? throw new InvalidOperationException("The public holiday API returned an empty response.");

            var dates = holidays
                .Where(holiday => holiday.Date.HasValue &&
                    (holiday.NationalHoliday || holiday.HolidayTypes?.Any(type =>
                        string.Equals(type, "Public", StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(type, "Bank", StringComparison.OrdinalIgnoreCase)) == true))
                .Select(holiday => DateOnly.FromDateTime(holiday.Date!.Value))
                .ToHashSet();

            _cache.Set(cacheKey, dates, TimeSpan.FromHours(Math.Max(1, _options.CacheHours)));
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
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch
        {
            HolidayRequests.TryRemove(cacheKey, out _);
            throw;
        }
    }

    private static TimeZoneInfo GetSouthAfricaTimeZone()
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
