using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("BankPaymentQueue")]
[Index("Channel", "Status", "LockedUntilUtc", Name = "IX_BankPaymentQueue_Channel_Status_LockedUntilUtc")]
[Index("Channel", "Status", "NextAttemptUtc", Name = "IX_BankPaymentQueue_Channel_Status_NextAttemptUtc")]
[Index("Channel", "Status", "NextFetchAttemptUtc", Name = "IX_BankPaymentQueue_Channel_Status_NextFetchAttemptUtc")]
[Index("EndToEndId", Name = "IX_BankPaymentQueue_EndToEndId")]
[Index("Status", "NextAttemptUtc", "LockedUntilUtc", Name = "IX_Fnb3PimOrderQueue_Worker")]
[Index("MsgId", Name = "UX_Fnb3PimOrderQueue_MsgId", IsUnique = true)]
[Index("PmtInfId", Name = "UX_Fnb3PimOrderQueue_PmtInfId", IsUnique = true)]
public partial class BankPaymentQueue : Entity<Guid>
{
    [Key]
    public override Guid Id { get; protected set; }

    [StringLength(64)]
    public string OrderId { get; set; } = null!;

    [StringLength(80)]
    public string OrderNumber { get; set; } = null!;

    [StringLength(120)]
    public string? ReferenceNumber { get; set; }

    [StringLength(64)]
    public string FromAccountId { get; set; } = null!;

    [StringLength(64)]
    public string DebtorAccountNumber { get; set; } = null!;

    [StringLength(64)]
    public string? DebtorAgentAccountNumber { get; set; }

    [StringLength(64)]
    public string CreditorAccountNumber { get; set; } = null!;

    [StringLength(16)]
    public string CreditorBranchCode { get; set; } = null!;

    [StringLength(140)]
    public string CreditorName { get; set; } = null!;

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Amount { get; set; }

    [StringLength(3)]
    [Unicode(false)]
    public string Currency { get; set; } = null!;

    public DateOnly RequestedExecutionDate { get; set; }

    [StringLength(20)]
    public string? ServiceType { get; set; }

    [StringLength(20)]
    public string ServiceLevel { get; set; } = null!;

    [StringLength(80)]
    public string? MsgId { get; set; }

    [StringLength(80)]
    public string? PmtInfId { get; set; }

    [StringLength(140)]
    public string EndToEndId { get; set; } = null!;

    [StringLength(120)]
    public string? InstructionId { get; set; }

    [StringLength(50)]
    public string Status { get; set; } = null!;

    public int SubmissionAttemptCount { get; set; }

    public int FetchAttemptCount { get; set; }

    public int ReconcileAttemptCount { get; set; }

    public DateTimeOffset? NextAttemptUtc { get; set; }

    public DateTimeOffset? NextFetchAttemptUtc { get; set; }

    public DateTimeOffset? LockedUntilUtc { get; set; }

    [StringLength(120)]
    public string? LockedBy { get; set; }

    [StringLength(100)]
    public string? LastErrorCode { get; set; }

    [StringLength(2000)]
    public string? LastErrorDescription { get; set; }

    public int? LastHttpStatus { get; set; }

    [StringLength(128)]
    public string? LogicalRequestHash { get; set; }

    public DateTimeOffset? SubmittedUtc { get; set; }

    public DateTimeOffset? BankAcceptedUtc { get; set; }

    public DateTimeOffset? ReconciledUtc { get; set; }

    public DateTimeOffset CreatedUtc { get; set; }

    public DateTimeOffset UpdatedUtc { get; set; }

    public byte[] RowVersion { get; set; } = null!;

    [StringLength(20)]
    [Unicode(false)]
    public string Channel { get; set; } = null!;

    [StringLength(16)]
    public string? DebtorBranchCode { get; set; }

    [StringLength(10)]
    public string? BankStatusCode { get; set; }

    [StringLength(500)]
    public string? BankStatusReason { get; set; }

    public DateTimeOffset? ReturnedUtc { get; set; }

    [Column("FkBankPaymentInstructionID")]
    public Guid? FkBankPaymentInstructionId { get; set; }

    [StringLength(140)]
    public string? DebtorName { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? DebtorAccountType { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? CreditorAccountType { get; set; }

    [StringLength(35)]
    public string? CreditorReference { get; set; }

    [ForeignKey("Channel")]
    [InverseProperty("BankPaymentQueues")]
    public virtual BankChannel ChannelNavigation { get; set; } = null!;

    [ForeignKey("FkBankPaymentInstructionId")]
    [InverseProperty("BankPaymentQueues")]
    public virtual BankPaymentInstruction? FkBankPaymentInstruction { get; set; }
}


