using Volo.Abp;

namespace GPay.Banking.Helpers;

/// <summary>
/// Parses bank identifiers from API input into <see cref="BankCode"/>.
/// </summary>
internal static class BankCodeParser
{
    public static BankCode Parse(string? bank)
    {
        if (string.IsNullOrWhiteSpace(bank))
        {
            return BankCode.Absa;
        }

        return bank.ToUpperInvariant() switch
        {
            "ABSA" => BankCode.Absa,
            "FNB" or "FIRST NATIONAL BANK" => BankCode.Fnb,
            "NEDBANK" => BankCode.Nedbank,
            "CAPITEC" or "CAPITEC BANK" => BankCode.Capitec,
            _ => throw new UserFriendlyException($"Bank '{bank}' is not supported.")
        };
    }
}
