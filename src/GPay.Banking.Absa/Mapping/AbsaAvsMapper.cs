using GPay.Banking.Absa.Configuration;
using GPay.Banking.Absa.Models.Avs;
using GPay.Banking.Contracts.Dtos.AccountVerification;
using Microsoft.Extensions.Options;

namespace GPay.Banking.Absa.Mapping;

/// <summary>
/// Maps GPay AVS DTOs to/from Absa CAPI AVS payloads (MIG – AVS API v1).
/// </summary>
public interface IAbsaAvsMapper
{
    /// <summary>
    /// Maps a GPay request to an Absa ValidateBankDetails request (Session filled by the client).
    /// </summary>
    AbsaAvsRequest ToAbsaRequest(AccountVerificationRequest request, string correlationId);

    /// <summary>
    /// Maps an Absa AVS response to a GPay response.
    /// </summary>
    AccountVerificationResponse ToGpayResponse(AbsaAvsResponse response, string? reference);
}

/// <summary>
/// Default Absa AVS mapper.
/// </summary>
public sealed class AbsaAvsMapper : IAbsaAvsMapper
{
    private readonly AbsaCapiOptions _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="AbsaAvsMapper"/> class.
    /// </summary>
    public AbsaAvsMapper(IOptions<AbsaCapiOptions> options)
    {
        _options = options.Value;
    }

    /// <summary>
    /// Parameterless constructor for unit tests (uses Absa defaults).
    /// </summary>
    public AbsaAvsMapper()
        : this(Options.Create(new AbsaCapiOptions { CapiCode = "10068", DefaultBankCode = "000016" }))
    {
    }

    /// <inheritdoc />
    public AbsaAvsRequest ToAbsaRequest(AccountVerificationRequest request, string correlationId)
    {
        var (initials, lastName) = SplitName(request);
        if (string.IsNullOrWhiteSpace(initials) &&
            request.AdditionalData?.TryGetValue("ClientInitials", out var extraInitials) == true)
        {
            initials = extraInitials;
        }

        if (string.IsNullOrWhiteSpace(lastName) &&
            request.AdditionalData?.TryGetValue("ClientName", out var extraName) == true)
        {
            lastName = extraName;
        }

        var bankCode = ResolveBankCode(request);
        var clientIdType = ResolveClientIdType(request);
        var clientIdNumber = request.IdentityNumber?.Trim()
            ?? request.AdditionalData?.GetValueOrDefault("ClientIdNumber")
            ?? string.Empty;

        return new AbsaAvsRequest
        {
            CorrelationId = correlationId,
            CapiCode = ResolveCapiCode(request),
            BankCode = bankCode,
            BranchCode = request.BranchCode.Trim(),
            AccountNumber = request.AccountNumber.Trim(),
            AccountType = ResolveAccountType(request.AccountType),
            ClientIdType = clientIdType,
            ClientIdNumber = clientIdNumber,
            ClientName = lastName?.Trim() ?? string.Empty,
            ClientInitials = initials?.Trim() ?? string.Empty,
            EmailAddress = request.Email?.Trim(),
            CellNumber = NormalizeCell(request.PhoneNumber)
        };
    }

    /// <inheritdoc />
    public AccountVerificationResponse ToGpayResponse(AbsaAvsResponse response, string? reference)
    {
        if (response.HasErrors)
        {
            var first = response.ErrorList![0];
            return new AccountVerificationResponse
            {
                IsVerified = false,
                ResultCode = "NOT_VERIFIED",
                ResultDescription = first.Description
                    ?? first.DetailedMessages?.FirstOrDefault()?.Message
                    ?? "Absa returned an error.",
                BankResultCode = first.Code ?? response.StatusCode,
                BankReference = response.ReferenceNumber,
                Reference = reference,
                MatchingCriteria = ToCriteriaDictionary(response)
            };
        }

        if (response.IsPendingStatus)
        {
            return new AccountVerificationResponse
            {
                IsVerified = false,
                ResultCode = "PENDING",
                ResultDescription = "Absa AVS enquiry is pending (non-Absa / AVS-R).",
                BankResultCode = response.StatusCode,
                BankReference = response.ReferenceNumber,
                Reference = reference
            };
        }

        var accountFound = ToTriState(response.GetValue("Account Found"));
        var accountOpen = ToTriState(response.GetValue("Account Open"));
        var idMatch = ToYnU(response.GetValue("ID Matched"));
        var nameMatch = ToYnU(response.GetValue("Name Matched"));
        var emailMatch = ToYnU(response.GetValue("Email Address Match"));
        var cellMatch = ToYnU(response.GetValue("Cell Number Match"));
        var openLonger = ToNullableBool(response.GetValue("Account Open Longer Than 3 Months"));

        var identityOk = IsYesOrUnverifiedOk(idMatch);
        var nameOk = IsYesOrUnverifiedOk(nameMatch);
        var accountOk = accountFound == true && accountOpen == true;

        var isBusinessSuccess =
            response.IsSuccessStatus &&
            accountOk &&
            identityOk &&
            nameOk;

        return new AccountVerificationResponse
        {
            IsVerified = isBusinessSuccess,
            AccountFound = accountFound,
            AccountOpen = accountOpen,
            AccountActive = openLonger ?? accountOpen,
            IdentityMatch = idMatch,
            NameMatch = nameMatch,
            EmailMatch = emailMatch,
            PhoneMatch = cellMatch,
            AccountOpenLongerThan3Months = openLonger,
            AllowsCredit = ToNullableBool(response.GetValue("Account Allows Credit")),
            AcceptsCredit = ToNullableBool(response.GetValue("Account Accepts Credit")),
            AllowsDebit = ToNullableBool(response.GetValue("Account Allows Debit")),
            AcceptsDebit = ToNullableBool(response.GetValue("Account Accepts Debit")),
            ResultCode = isBusinessSuccess
                ? "VERIFIED"
                : response.StatusCode is "033" or "099" ? "ERROR" : "NOT_VERIFIED",
            ResultDescription = isBusinessSuccess
                ? "Account verified."
                : response.IsSuccessStatus
                    ? "Account was not verified against matching criteria."
                    : $"Absa AVS status {response.StatusCode}.",
            BankResultCode = response.StatusCode,
            BankReference = response.ReferenceNumber,
            Reference = reference,
            MatchingCriteria = ToCriteriaDictionary(response)
        };
    }

    private string ResolveCapiCode(AccountVerificationRequest request)
    {
        if (request.AdditionalData?.TryGetValue("CapiCode", out var code) == true &&
            !string.IsNullOrWhiteSpace(code))
        {
            return code.Trim();
        }

        return string.IsNullOrWhiteSpace(_options.CapiCode) ? string.Empty : _options.CapiCode.Trim();
    }

    private string ResolveBankCode(AccountVerificationRequest request)
    {
        if (!string.IsNullOrWhiteSpace(request.IssuingBankCode))
        {
            return request.IssuingBankCode.Trim();
        }

        if (request.AdditionalData?.TryGetValue("BankCode", out var code) == true &&
            !string.IsNullOrWhiteSpace(code))
        {
            return code.Trim();
        }

        return string.IsNullOrWhiteSpace(_options.DefaultBankCode) ? "000016" : _options.DefaultBankCode.Trim();
    }

    private static int ResolveClientIdType(AccountVerificationRequest request)
    {
        if (request.AdditionalData?.TryGetValue("ClientIdType", out var raw) == true &&
            int.TryParse(raw, out var parsed))
        {
            return parsed;
        }

        var identityType = request.IdentityType?.Trim().ToUpperInvariant();
        return identityType switch
        {
            "1" or "COMPANY" or "COMPANYREGISTRATION" or "COMPANY_REGISTRATION" or "REGISTRATION" => 1,
            "6" or "PASSPORT" => 6,
            "8" or "TRUST" or "TRUSTNUMBER" => 8,
            "2" or "SAID" or "ID" or "IDENTITY" or "NATIONAL_ID" or "RSAIDENTITY" => 2,
            _ => GuessClientIdType(request.IdentityNumber)
        };
    }

    private static int GuessClientIdType(string? identityNumber)
    {
        if (string.IsNullOrWhiteSpace(identityNumber))
        {
            return 2;
        }

        return identityNumber.All(char.IsDigit) && identityNumber.Length == 13 ? 2 : 6;
    }

    private static int ResolveAccountType(string? accountType)
    {
        if (string.IsNullOrWhiteSpace(accountType))
        {
            return 1; // Cheque / current (MIG sample)
        }

        if (int.TryParse(accountType.Trim(), out var numeric))
        {
            return numeric;
        }

        return accountType.Trim().ToUpperInvariant() switch
        {
            "00" or "UNSPECIFIED" or "NOTSPECIFIED" => 0,
            "01" or "1" or "CURRENT" or "CHEQUE" or "CHECKING" => 1,
            "02" or "2" or "SAVINGS" => 2,
            "03" or "3" or "TRANSMISSION" => 3,
            "04" or "4" or "BOND" => 4,
            "06" or "6" or "SUBSCRIPTION" or "SUBSCRIPTIONSHARE" => 6,
            _ => 1
        };
    }

    private static string? NormalizeCell(string? phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
        {
            return null;
        }

        var trimmed = phone.Trim().Replace(" ", string.Empty);
        if (trimmed.StartsWith('+'))
        {
            return trimmed;
        }

        if (trimmed.StartsWith("0") && trimmed.Length == 10)
        {
            return "+27" + trimmed[1..];
        }

        if (trimmed.StartsWith("27") && trimmed.Length == 11)
        {
            return "+" + trimmed;
        }

        return trimmed;
    }

    private static (string? Initials, string? LastName) SplitName(AccountVerificationRequest request)
    {
        if (!string.IsNullOrWhiteSpace(request.Initials) || !string.IsNullOrWhiteSpace(request.LastName))
        {
            return (request.Initials?.Trim(), request.LastName?.Trim());
        }

        if (string.IsNullOrWhiteSpace(request.AccountHolderName))
        {
            return (null, null);
        }

        var parts = request.AccountHolderName.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 1)
        {
            return (null, parts[0]);
        }

        var initials = string.Concat(parts.Take(parts.Length - 1).Select(p => char.ToUpperInvariant(p[0])));
        return (initials, parts[^1]);
    }

    private static Dictionary<string, string>? ToCriteriaDictionary(AbsaAvsResponse response)
    {
        if (response.ValueList is null || response.ValueList.Count == 0)
        {
            return null;
        }

        return response.ValueList
            .Where(v => !string.IsNullOrWhiteSpace(v.Key))
            .GroupBy(v => v.Key!, StringComparer.OrdinalIgnoreCase)
            .ToDictionary(g => g.Key, g => g.Last().Value ?? string.Empty, StringComparer.OrdinalIgnoreCase);
    }

    private static bool? ToTriState(string? value) =>
        value?.Trim().ToUpperInvariant() switch
        {
            "YES" or "Y" or "TRUE" => true,
            "NO" or "N" or "FALSE" => false,
            _ => null
        };

    private static bool? ToNullableBool(string? value) =>
        value?.Trim().ToUpperInvariant() switch
        {
            "YES" or "Y" or "TRUE" => true,
            "NO" or "N" or "FALSE" => false,
            _ => null
        };

    private static string? ToYnU(string? value) =>
        value?.Trim().ToUpperInvariant() switch
        {
            "YES" or "Y" or "TRUE" => "Y",
            "NO" or "N" or "FALSE" => "N",
            "UNVERIFIED" or "U" => "U",
            null or "" => null,
            _ => "U"
        };

    private static bool IsYesOrUnverifiedOk(string? ynU) =>
        ynU is null or "Y" or "U";
}
