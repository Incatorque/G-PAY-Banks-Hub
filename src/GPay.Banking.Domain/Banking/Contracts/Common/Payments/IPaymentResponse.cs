namespace GPay.Banking.Domain.Dtos;

/// <summary>Normalized fields shared by payment initiation and status responses.</summary>
public interface IPaymentResponse
{
    string Status { get; }
    string? RawStatusLabel { get; }
    string? Reference { get; }
    decimal? Amount { get; }
    string? Currency { get; }
    string? ApiReference { get; }
    string? TransactionReference { get; }
    string? TransactionId { get; }
    string? SourceStatementRef { get; }
    string? TargetStatementRef { get; }
    int? BankStatusCode { get; }
    string? PaymentRail { get; }
    string? ResultDescription { get; }
    string? ErrorCode { get; }
}
