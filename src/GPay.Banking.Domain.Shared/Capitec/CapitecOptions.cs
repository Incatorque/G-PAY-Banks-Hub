namespace GPay.Banking.Services.Capitec;

/// <summary>Settings for the Capitec Payments Bulk Payments API.</summary>
public sealed class CapitecOptions
{
    public const string SectionName = "Capitec";

    public string Environment { get; set; } = "QA";
    public string BaseUrl { get; set; } = "https://paycps60.capitecbank.co.za/api/payments-paycps-api-gateway/v1/";
    public string TokenUrl { get; set; } = "https://login.microsoftonline.com/a428b46f-c29b-4a6c-85f1-8e05c10b6671/oauth2/token";
    public string ClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
    public string Resource { get; set; } = "7496c555-e637-4180-9837-2537253f7d40";
    public string Scope { get; set; } = "openid";
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string InitiatingPartyId { get; set; } = string.Empty;
    public string? DebtorName { get; set; }
    public int TimeoutSeconds { get; set; } = 60;
    public string CreditTransferPath { get; set; } = "payment/creditTransferRequest/pbac";
    public string StatusEnquiryPath { get; set; } = "payment/statusEnquiry";
    public Dictionary<string, string> BankBicfi { get; set; } = new(StringComparer.OrdinalIgnoreCase);
}
