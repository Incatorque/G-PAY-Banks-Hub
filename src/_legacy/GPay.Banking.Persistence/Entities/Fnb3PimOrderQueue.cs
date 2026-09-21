using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class Fnb3PimOrderQueue
{
    public Guid Id { get; set; }

    public string OrderId { get; set; } = null!;

    public string OrderNumber { get; set; } = null!;

    public string? ReferenceNumber { get; set; }

    public string FromAccountId { get; set; } = null!;

    public string Debtor3PimInvestmentAccount { get; set; } = null!;

    public string? DebtorAgentTrustAccountNumber { get; set; }

    public string CreditorAccountNumber { get; set; } = null!;

    public string CreditorBranchCode { get; set; } = null!;

    public string CreditorName { get; set; } = null!;

    public decimal Amount { get; set; }

    public string Currency { get; set; } = null!;

    public DateOnly RequestedExecutionDate { get; set; }

    public string? ServiceType { get; set; }

    public string ServiceLevel { get; set; } = null!;

    public string MsgId { get; set; } = null!;

    public string PmtInfId { get; set; } = null!;

    public string EndToEndId { get; set; } = null!;

    public string? InstructionId { get; set; }

    public string Status { get; set; } = null!;

    public int SubmissionAttemptCount { get; set; }

    public int FetchAttemptCount { get; set; }

    public int ReconcileAttemptCount { get; set; }

    public DateTimeOffset? NextAttemptUtc { get; set; }

    public DateTimeOffset? NextFetchAttemptUtc { get; set; }

    public DateTimeOffset? LockedUntilUtc { get; set; }

    public string? LockedBy { get; set; }

    public string? LastErrorCode { get; set; }

    public string? LastErrorDescription { get; set; }

    public int? LastHttpStatus { get; set; }

    public string? LogicalRequestHash { get; set; }

    public DateTimeOffset? SubmittedUtc { get; set; }

    public DateTimeOffset? BankAcceptedUtc { get; set; }

    public DateTimeOffset? ReconciledUtc { get; set; }

    public DateTimeOffset CreatedUtc { get; set; }

    public DateTimeOffset UpdatedUtc { get; set; }

    public byte[] RowVersion { get; set; } = null!;
}
