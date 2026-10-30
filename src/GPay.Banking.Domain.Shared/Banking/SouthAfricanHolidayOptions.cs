namespace GPay.Banking;

public sealed class SouthAfricanHolidayOptions
{
    public const string SectionName = "SouthAfricanHolidays";

    public string ApiBaseUrl { get; set; } = "https://nagerholidays.com/api/v4/";

    public string CountryCode { get; set; } = "ZA";

    public int CacheHours { get; set; } = 24;
}
