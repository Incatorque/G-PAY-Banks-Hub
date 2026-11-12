namespace GPay.Banking.Services.Capitec.Api.Mapping;

/// <summary>Maps Capitec CPS lifecycle statuses (Bulk Payments API v2.5.3) into Bank Hub statuses.</summary>
public static class CapitecPaymentStatuses
{
    public static string ToLabel(string? status) => status?.Trim() switch
    {
        "017" or "Successful" => "Completed",
        "002" or "Rejected" => "Failed",
        "001" or "Accepted" => "Submitted",
        "003" or "Initiating Party Valid" or
        "004" or "Request Valid" or
        "005" or "Completed" or
        "006" or "Not Validated" or
        "007" or "Instruction Valid" or
        "008" or "Debtor Valid" or
        "009" or "Transaction Validation" or
        "010" or "Transform & Submit" or
        "011" or "Awaiting Response" or
        "012" or "Initiated" or
        "013" or "Mop Derived" or
        "014" or "Credit Account Validation Required" or
        "015" or "Validated" or
        "016" or "Transformed & Submitted" or
        "018" or "Credit Transaction Validation" or
        "019" or "Valid" => "Submitted",
        _ => "Pending"
    };

    public static int? ToCode(string? status) => int.TryParse(status, out var code) ? code : status?.Trim() switch
    {
        "Accepted" => 1,
        "Rejected" => 2,
        "Successful" => 17,
        _ => null
    };
}
