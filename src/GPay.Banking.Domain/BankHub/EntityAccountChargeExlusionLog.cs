using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("EntityAccountChargeExlusionLog")]
[Index("FkuserId", Name = "IX_EntityAccountChargeExlusionLog_FKUserId")]
[Index("FkEntityAccountChargeId", Name = "IX_EntityAccountChargeExlusionLog_FkEntityAccountChargeId")]
public partial class EntityAccountChargeExlusionLog : Entity<Guid>
{
    [Key]
    [Column("PkEntityAccountChargeExclusionLogId")]
    public override Guid Id { get; protected set; }

    public Guid FkEntityAccountChargeId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime LogDate { get; set; }

    [Column("FKUserId")]
    public Guid FkuserId { get; set; }

    [Unicode(false)]
    public string LogDescription { get; set; } = null!;

    [ForeignKey("FkEntityAccountChargeId")]
    [InverseProperty("EntityAccountChargeExlusionLogs")]
    public virtual EntityAccountCharge FkEntityAccountCharge { get; set; } = null!;

    [ForeignKey("FkuserId")]
    [InverseProperty("EntityAccountChargeExlusionLogs")]
    public virtual User Fkuser { get; set; } = null!;
}


