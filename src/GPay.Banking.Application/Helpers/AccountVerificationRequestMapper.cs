using GPay.Banking.BankHub;
using GPay.Banking.Domain.Dtos;

namespace GPay.Banking.Helpers;

internal static class AccountVerificationRequestMapper
{
    public static AccountVerificationRequest From(
        VerifyAccountRequestDto? input,
        BankCode bank,
        BankHubAvsRecord? fallback = null) => new()
    {
        Bank = bank,
        AccountNumber = input?.AccountNumber ?? fallback?.AccountNumber ?? string.Empty,
        BranchCode = input?.BranchCode ?? fallback?.BranchCode ?? string.Empty,
        IssuingBankCode = input?.IssuingBankCode,
        IdentityNumber = input?.IdentityNumber ?? fallback?.IdentityNumber,
        IdentityType = input?.IdentityType,
        AccountHolderName = input?.AccountHolderName ?? fallback?.AccountHolderName,
        Initials = input?.Initials,
        LastName = input?.LastName ?? fallback?.AccountHolderName,
        AccountType = input?.AccountType,
        Email = input?.Email,
        PhoneNumber = input?.PhoneNumber,
        Reference = input?.Reference ?? fallback?.Reference
    };
}
