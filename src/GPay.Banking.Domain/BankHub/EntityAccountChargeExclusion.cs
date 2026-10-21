using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

public partial class EntityAccountChargeExclusion : Entity<Guid>
{
    [Key]
    [Column("PkEntityAccountChargeExclusionId")]
    public override Guid Id { get; protected set; }

    [Column("FkEntityAccountChargeID")]
    public Guid FkEntityAccountChargeId { get; set; }

    [StringLength(50)]
    public string? Mask { get; set; }

    [Column(TypeName = "money")]
    public decimal? Value { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreateDate { get; set; }

    [ForeignKey("FkEntityAccountChargeId")]
    [InverseProperty("EntityAccountChargeExclusions")]
    public virtual EntityAccountCharge FkEntityAccountCharge { get; set; } = null!;
}


