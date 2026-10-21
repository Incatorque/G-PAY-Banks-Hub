using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("AccountChargeExludedEntityLog")]
[Index("FkuserId", Name = "IX_AccountChargeExludedEntityLog_FKUserId")]
[Index("FkAccountChargeId", Name = "IX_AccountChargeExludedEntityLog_FkAccountChargeId")]
public partial class AccountChargeExludedEntityLog : Entity<Guid>
{
    [Key]
    [Column("PkAccountChargeExludedEntityLogId")]
    public override Guid Id { get; protected set; }

    public Guid FkAccountChargeId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime LogDate { get; set; }

    [Column("FKUserId")]
    public Guid FkuserId { get; set; }

    [Unicode(false)]
    public string LogDescription { get; set; } = null!;

    [ForeignKey("FkAccountChargeId")]
    [InverseProperty("AccountChargeExludedEntityLogs")]
    public virtual AccountCharge FkAccountCharge { get; set; } = null!;

    [ForeignKey("FkuserId")]
    [InverseProperty("AccountChargeExludedEntityLogs")]
    public virtual User Fkuser { get; set; } = null!;
}


