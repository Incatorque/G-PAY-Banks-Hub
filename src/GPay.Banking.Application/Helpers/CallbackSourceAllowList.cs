using System.Net;
using System.Net.Sockets;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace GPay.Banking.Helpers;

/// <summary>
/// Validates callback source IP / reverse-DNS against <see cref="BankCallbackOptions"/>.
/// </summary>
public static class CallbackSourceAllowList
{
    public static IReadOnlyList<string> ConfiguredEntries(IEnumerable<string>? values) =>
        (values ?? []).Where(v => !string.IsNullOrWhiteSpace(v)).ToList();

    public static string? GetSourceIp(HttpContext? httpContext)
    {
        if (httpContext is null)
        {
            return null;
        }

        var forwarded = httpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
        if (!string.IsNullOrWhiteSpace(forwarded))
        {
            var first = forwarded.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                .FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(first))
            {
                return first;
            }
        }

        return httpContext.Connection.RemoteIpAddress?.ToString();
    }

    public static async Task<CallbackSourceCheckResult> CheckAsync(
        BankCallbackOptions options,
        string bankKey,
        string? sourceIp,
        ILogger? logger = null,
        CancellationToken cancellationToken = default)
    {
        if (!options.EnforceSourceAllowList)
        {
            return CallbackSourceCheckResult.Allowed("Allow-list enforcement is disabled.");
        }

        if (!options.Banks.TryGetValue(bankKey, out var bank) || bank is null)
        {
            return CallbackSourceCheckResult.Allowed("No bank allow-list configured.");
        }

        var ips = ConfiguredEntries(bank.AllowedSourceIps);
        var domains = ConfiguredEntries(bank.AllowedSourceDomains);
        if (ips.Count == 0 && domains.Count == 0)
        {
            logger?.LogDebug(
                "BankCallbacks:{Bank} has no AllowedSourceIps/AllowedSourceDomains yet — allowing source {SourceIp}",
                bankKey,
                sourceIp);
            return CallbackSourceCheckResult.Allowed("No AllowedSourceIps/AllowedSourceDomains configured yet.");
        }

        if (string.IsNullOrWhiteSpace(sourceIp))
        {
            return CallbackSourceCheckResult.Denied("Source IP could not be determined.");
        }

        if (ips.Count > 0 && IpMatches(sourceIp, ips))
        {
            return CallbackSourceCheckResult.Allowed("Source IP matched allow-list.");
        }

        if (domains.Count > 0)
        {
            var host = await TryReverseDnsAsync(sourceIp, cancellationToken);
            if (!string.IsNullOrWhiteSpace(host) && DomainMatches(host, domains))
            {
                return CallbackSourceCheckResult.Allowed($"Source domain '{host}' matched allow-list.");
            }
        }

        return CallbackSourceCheckResult.Denied(
            $"Source '{sourceIp}' is not in the allow-list for bank '{bankKey}'.");
    }

    /// <summary>
    /// Resolves bank key from source IP/domain when <paramref name="bank"/> is omitted.
    /// </summary>
    public static async Task<string?> ResolveBankKeyAsync(
        BankCallbackOptions options,
        string? sourceIp,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(sourceIp) || options.Banks.Count == 0)
        {
            return null;
        }

        string? reverseHost = null;
        foreach (var (key, bank) in options.Banks)
        {
            var ips = ConfiguredEntries(bank.AllowedSourceIps);
            if (ips.Count > 0 && IpMatches(sourceIp, ips))
            {
                return key;
            }

            var domains = ConfiguredEntries(bank.AllowedSourceDomains);
            if (domains.Count == 0)
            {
                continue;
            }

            reverseHost ??= await TryReverseDnsAsync(sourceIp, cancellationToken);
            if (!string.IsNullOrWhiteSpace(reverseHost) && DomainMatches(reverseHost, domains))
            {
                return key;
            }
        }

        return null;
    }

    public static bool IpMatches(string sourceIp, IReadOnlyList<string> allowed)
    {
        if (!IPAddress.TryParse(sourceIp, out var source))
        {
            return false;
        }

        foreach (var entry in allowed)
        {
            if (entry.Contains('/', StringComparison.Ordinal))
            {
                if (CidrContains(entry, source))
                {
                    return true;
                }

                continue;
            }

            if (IPAddress.TryParse(entry, out var exact) && exact.Equals(source))
            {
                return true;
            }
        }

        return false;
    }

    public static bool DomainMatches(string hostName, IReadOnlyList<string> allowed)
    {
        var host = hostName.Trim().TrimEnd('.').ToLowerInvariant();
        foreach (var entry in allowed)
        {
            var pattern = entry.Trim().TrimStart('*', '.').TrimEnd('.').ToLowerInvariant();
            if (string.IsNullOrWhiteSpace(pattern))
            {
                continue;
            }

            if (host == pattern || host.EndsWith("." + pattern, StringComparison.Ordinal))
            {
                return true;
            }
        }

        return false;
    }

    private static bool CidrContains(string cidr, IPAddress address)
    {
        var parts = cidr.Split('/', 2);
        if (parts.Length != 2 ||
            !IPAddress.TryParse(parts[0], out var network) ||
            !int.TryParse(parts[1], out var prefixLength))
        {
            return false;
        }

        if (network.AddressFamily != address.AddressFamily)
        {
            return false;
        }

        var networkBytes = network.GetAddressBytes();
        var addressBytes = address.GetAddressBytes();
        if (networkBytes.Length != addressBytes.Length)
        {
            return false;
        }

        var bitLength = networkBytes.Length * 8;
        if (prefixLength < 0 || prefixLength > bitLength)
        {
            return false;
        }

        var fullBytes = prefixLength / 8;
        var remainingBits = prefixLength % 8;

        for (var i = 0; i < fullBytes; i++)
        {
            if (networkBytes[i] != addressBytes[i])
            {
                return false;
            }
        }

        if (remainingBits == 0)
        {
            return true;
        }

        var mask = (byte)(0xFF << (8 - remainingBits));
        return (networkBytes[fullBytes] & mask) == (addressBytes[fullBytes] & mask);
    }

    private static async Task<string?> TryReverseDnsAsync(string ip, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        try
        {
            var entry = await Dns.GetHostEntryAsync(ip);
            return entry.HostName;
        }
        catch (SocketException)
        {
            return null;
        }
        catch (ArgumentException)
        {
            return null;
        }
    }
}
