using System.Text.Json;
using GPay.Banking.BankHub;
using GPay.Banking.Domain.Dtos;

namespace GPay.Banking.Helpers;

internal static class BankHubAvsRecordMapper
{
    public static void Apply(BankHubAvsRecord record, AccountVerificationResponse response)
    {
        record.IsVerified = response.IsVerified;
        record.ResultCode = response.ResultCode;
        record.ResultDescription = response.ResultDescription;
        record.BankResultCode = response.BankResultCode;
        record.BankReference = response.BankReference;
        record.AccountFound = response.AccountFound;
        record.AccountOpen = response.AccountOpen;
        record.AccountActive = response.AccountActive;
        record.IdentityMatch = response.IdentityMatch;
        record.NameMatch = response.NameMatch;
        record.InitialsMatch = response.InitialsMatch;
        record.EmailMatch = response.EmailMatch;
        record.PhoneMatch = response.PhoneMatch;
        record.AccountTypeMatch = response.AccountTypeMatch;
        record.AccountOpenLongerThan3Months = response.AccountOpenLongerThan3Months;
        record.AllowsCredit = response.AllowsCredit;
        record.AcceptsCredit = response.AcceptsCredit;
        record.AllowsDebit = response.AllowsDebit;
        record.AcceptsDebit = response.AcceptsDebit;
        record.MatchingCriteriaJson = response.MatchingCriteria is null
            ? null
            : JsonSerializer.Serialize(response.MatchingCriteria);
        record.Status = response.IsVerified ? "Verified" : "NotVerified";
        record.ErrorMessage = null;
    }
}
