using System.Text.RegularExpressions;

namespace GPay.Banking.Services.Absa.Api.Clients;

/// <summary>
/// Removes session, password, and token values before an API call is stored.
/// </summary>
internal static class AbsaApiPayloadRedactor
{
    private static readonly Regex SecretPattern = new(
        "(\"(?:Session|Password|Token)\"\\s*:\\s*)\"(?:\\\\.|[^\"\\\\])*\"",
        RegexOptions.IgnoreCase | RegexOptions.Compiled,
        TimeSpan.FromSeconds(1));

    public static string? Redact(string? json)
    {
        if (string.IsNullOrEmpty(json))
        {
            return json;
        }

        return SecretPattern.Replace(json, "$1\"***\"");
    }
}
