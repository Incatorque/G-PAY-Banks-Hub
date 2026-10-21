using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("EntityAccountChargeMappingLog")]
[Index("FkuserId", Name = "IX_EntityAccountChargeMappingLog_FKUserId")]
[Index("FkEntityAccountChargeId", Name = "IX_EntityAccountChargeMappingLog_FkEntityAccountChargeId")]
[Index("FkEntityAccountChargeMappingId", Name = "IX_EntityAccountChargeMappingLog_FkEntityAccountChargeMappingId")]
public partial class EntityAccountChargeMappingLog : Entity<Guid>
{
    [Key]
    [Column("PkEntityAccountChargeMappingLogId")]
    public override Guid Id { get; protected set; }

    public Guid FkEntityAccountChargeMappingId { get; set; }

    public Guid FkEntityAccountChargeId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime LogDate { get; set; }

    [Column("FKUserId")]
    public Guid FkuserId { get; set; }

    [Unicode(false)]
    public string LogDescription { get; set; } = null!;
}


