using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("BankStatementImportWatermark")]
[Index("AccountId", Name = "IX_BankStatementImportWatermark_AccountId")]
[Index("Channel", "AccountId", "AccountNumber", Name = "UQ_BankStatementImportWatermark_Channel_Account", IsUnique = true)]
[Index("AccountId", Name = "UX_Fnb3PimImportWatermark_AccountId", IsUnique = true)]
public partial class BankStatementImportWatermark : Entity<Guid>
{
    [Key]
    public override Guid Id { get; protected set; }

    [StringLength(64)]
    public string AccountId { get; set; } = null!;

    [StringLength(64)]
    public string AccountNumber { get; set; } = null!;

    public DateTimeOffset? LastSuccessfulImportFromUtc { get; set; }

    public DateTimeOffset? LastSuccessfulImportToUtc { get; set; }

    public DateTimeOffset? LastSuccessfulImportUtc { get; set; }

    public DateTimeOffset? LastAttemptUtc { get; set; }

    [StringLength(2000)]
    public string? LastError { get; set; }

    public DateTimeOffset CreatedUtc { get; set; }

    public DateTimeOffset UpdatedUtc { get; set; }

    public byte[] RowVersion { get; set; } = null!;

    [StringLength(20)]
    [Unicode(false)]
    public string Channel { get; set; } = null!;

    [ForeignKey("Channel")]
    [InverseProperty("BankStatementImportWatermarks")]
    public virtual BankChannel ChannelNavigation { get; set; } = null!;
}


