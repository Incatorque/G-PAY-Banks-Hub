using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("BankChannel")]
public partial class BankChannel : Entity<string>
{
    [Key]
    [StringLength(20)]
    [Unicode(false)]
    [Column("Code")]
    public override string Id { get; protected set; } = null!;

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("FkBankID")]
    public Guid? FkBankId { get; set; }

    public bool Active { get; set; }

    public DateTimeOffset CreatedUtc { get; set; }

    public bool ImportsStatements { get; set; }

    [InverseProperty("ChannelNavigation")]
    public virtual ICollection<BankApiCallLog> BankApiCallLogs { get; set; } = new List<BankApiCallLog>();

    [InverseProperty("ChannelNavigation")]
    public virtual ICollection<BankPaymentInstruction> BankPaymentInstructions { get; set; } = new List<BankPaymentInstruction>();

    [InverseProperty("ChannelNavigation")]
    public virtual ICollection<BankPaymentQueue> BankPaymentQueues { get; set; } = new List<BankPaymentQueue>();

    [InverseProperty("ChannelNavigation")]
    public virtual ICollection<BankStatementImportWatermark> BankStatementImportWatermarks { get; set; } = new List<BankStatementImportWatermark>();

    [ForeignKey("FkBankId")]
    [InverseProperty("BankChannel")]
    public virtual Bank? FkBank { get; set; }
}


