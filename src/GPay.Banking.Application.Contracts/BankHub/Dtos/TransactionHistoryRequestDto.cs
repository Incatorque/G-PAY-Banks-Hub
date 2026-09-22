using System.ComponentModel.DataAnnotations;

namespace GPay.Banking.BankHub;

/// <summary>
/// Request for account transaction history (bank statement lines) for a date range.
/// </summary>
public class TransactionHistoryRequestDto
{
    /// <summary>
    /// Target bank: <c>Absa</c>, <c>Fnb</c>, or <c>Nedbank</c>. Defaults to <c>Absa</c>.
    /// </summary>
    /// <example>Absa</example>
    public string Bank { get; set; } = "Absa";

    /// <summary>
    /// Account number whose history is requested.
    /// </summary>
    /// <example>4049813068</example>
    [Required]
    public string AccountNumber { get; set; } = string.Empty;

    /// <summary>
    /// Inclusive start date (calendar date; interpreted as UTC date boundary by the adapter).
    /// </summary>
    /// <example>2026-09-01</example>
    [Required]
    public DateOnly FromDate { get; set; }

    /// <summary>
    /// Inclusive end date (calendar date). Must be on or after <see cref="FromDate"/>.
    /// </summary>
    /// <example>2026-09-22</example>
    [Required]
    public DateOnly ToDate { get; set; }

    /// <summary>
    /// Optional maximum number of transactions to return (newest-first when the bank supports it).
    /// </summary>
    /// <example>100</example>
    public int? PageSize { get; set; }
}
