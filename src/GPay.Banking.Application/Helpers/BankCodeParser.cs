using Volo.Abp;

namespace GPay.Banking.Helpers;

/// <summary>
/// Parses bank identifiers from API input into <see cref="BankCode"/>.
/// </summary>
public static class BankCodeParser
{
    public static BankCode Parse(string? bank)
    {
        if (string.IsNullOrWhiteSpace(bank) ||
            bank.Equals("Absa", StringComparison.OrdinalIgnoreCase))
        {
            return BankCode.Absa;
        }

        if (bank.Equals("Fnb", StringComparison.OrdinalIgnoreCase))
        {
            return BankCode.Fnb;
        }

        if (bank.Equals("Nedbank", StringComparison.OrdinalIgnoreCase))
        {
            return BankCode.Nedbank;
        }

        throw new UserFriendlyException($"Bank '{bank}' is not supported.");
    }

    public static string ToStorageCode(BankCode bank) => bank switch
    {
        BankCode.Absa => "Absa",
        BankCode.Fnb => "Fnb",
        BankCode.Nedbank => "Nedbank",
        _ => bank.ToString()
    };
}
