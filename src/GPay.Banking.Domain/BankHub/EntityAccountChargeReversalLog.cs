using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("EntityAccountChargeReversalLog")]
public partial class EntityAccountChargeReversalLog : Entity<Guid>
{
    [Key]
    [Column("PkEntityAccountChargeReversalLogId")]
    public override Guid Id { get; protected set; }

    public Guid FkEntityAccountChargeId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime LogDate { get; set; }

    [Column("FKUserId")]
    public Guid FkuserId { get; set; }

    [Unicode(false)]
    public string LogDescription { get; set; } = null!;

    [ForeignKey("FkEntityAccountChargeId")]
    [InverseProperty("EntityAccountChargeReversalLogs")]
    public virtual EntityAccountCharge FkEntityAccountCharge { get; set; } = null!;

    [ForeignKey("FkuserId")]
    [InverseProperty("EntityAccountChargeReversalLogs")]
    public virtual User Fkuser { get; set; } = null!;
}


