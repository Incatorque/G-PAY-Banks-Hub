using System.Text.Json.Serialization;

namespace GPay.Banking.Absa.Models.Avs;

/// <summary>
/// Absa Validate Bank Details request (MIG – AVS API v1 §6.3 / §6.4).
/// </summary>
public sealed class AbsaAvsRequest
{
    /// <summary>
    /// Session from <c>/api/User/Authenticate</c>.
    /// </summary>
    [JsonPropertyName("Session")]
    public string? Session { get; set; }

    /// <summary>
    /// Unique billing code from onboarding.
    /// </summary>
    [JsonPropertyName("CapiCode")]
    public required string CapiCode { get; init; }

    /// <summary>
    /// Bank code where the account resides (e.g. 000016 = Absa, or sample form "16").
    /// </summary>
    [JsonPropertyName("BankCode")]
    public required string BankCode { get; init; }

    /// <summary>
    /// Branch code where the account resides.
    /// </summary>
    [JsonPropertyName("BranchCode")]
    public required string BranchCode { get; init; }

    /// <summary>
    /// Account number to verify.
    /// </summary>
    [JsonPropertyName("AccountNumber")]
    public required string AccountNumber { get; init; }

    /// <summary>
    /// Account type (sample uses int: 1 = Cheque, 2 = Savings, …).
    /// </summary>
    [JsonPropertyName("AccountType")]
    public int AccountType { get; init; }

    /// <summary>
    /// Client id type: 1=Company, 2=Identity, 6=Passport, 8=Trust.
    /// </summary>
    [JsonPropertyName("ClientIdType")]
    public int ClientIdType { get; init; }

    /// <summary>
    /// Identity / registration / passport / trust number.
    /// </summary>
    [JsonPropertyName("ClientIdNumber")]
    public required string ClientIdNumber { get; init; }

    /// <summary>
    /// Account holder surname / name.
    /// </summary>
    [JsonPropertyName("ClientName")]
    public required string ClientName { get; init; }

    /// <summary>
    /// Account holder initials (no punctuation).
    /// </summary>
    [JsonPropertyName("ClientInitials")]
    public required string ClientInitials { get; init; }

    /// <summary>
    /// Optional email (sample JSON field name EmailAddress).
    /// </summary>
    [JsonPropertyName("EmailAddress")]
    public string? EmailAddress { get; init; }

    /// <summary>
    /// Optional cell in international format (e.g. +27823509883).
    /// </summary>
    [JsonPropertyName("CellNumber")]
    public string? CellNumber { get; init; }

    /// <summary>
    /// GPay correlation id (not sent to Absa; used for logging).
    /// </summary>
    [JsonIgnore]
    public string? CorrelationId { get; init; }
}

/// <summary>
/// Absa Validate Bank Reference request (MIG §6.4.2) for non-Absa accounts.
/// </summary>
public sealed class AbsaValidateBankReferenceRequest
{
    [JsonPropertyName("ReferenceNumber")]
    public required string ReferenceNumber { get; init; }

    [JsonPropertyName("CapiCode")]
    public required string CapiCode { get; init; }

    [JsonPropertyName("Session")]
    public required string Session { get; init; }
}
