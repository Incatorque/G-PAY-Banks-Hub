using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("BankPaymentInstruction")]
[Index("Channel", "MsgId", Name = "UQ_BankPaymentInstruction_Channel_MsgId", IsUnique = true)]
public partial class BankPaymentInstruction : Entity<Guid>
{
    [Key]
    public override Guid Id { get; protected set; }

    [StringLength(20)]
    [Unicode(false)]
    public string Channel { get; set; } = null!;

    [StringLength(80)]
    public string MsgId { get; set; } = null!;

    [StringLength(120)]
    public string? InstructionId { get; set; }

    [StringLength(30)]
    public string Status { get; set; } = null!;

    public int TransactionCount { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal ControlSum { get; set; }

    public DateTimeOffset? SubmittedUtc { get; set; }

    public DateTimeOffset? NextFetchAttemptUtc { get; set; }

    public int FetchAttemptCount { get; set; }

    public DateTimeOffset? LockedUntilUtc { get; set; }

    [StringLength(120)]
    public string? LockedBy { get; set; }

    public int? LastHttpStatus { get; set; }

    [StringLength(100)]
    public string? LastErrorCode { get; set; }

    [StringLength(2000)]
    public string? LastErrorDescription { get; set; }

    public DateTimeOffset CreatedUtc { get; set; }

    public DateTimeOffset UpdatedUtc { get; set; }

    public byte[] RowVersion { get; set; } = null!;

    [InverseProperty("FkBankPaymentInstruction")]
    public virtual ICollection<BankPaymentQueue> BankPaymentQueues { get; set; } = new List<BankPaymentQueue>();

    [ForeignKey("Channel")]
    [InverseProperty("BankPaymentInstructions")]
    public virtual BankChannel ChannelNavigation { get; set; } = null!;
}


