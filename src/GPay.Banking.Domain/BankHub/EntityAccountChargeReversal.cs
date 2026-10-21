using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

public partial class EntityAccountChargeReversal : Entity<Guid>
{
    [Key]
    [Column("PkEntityAccountChargeReversalId")]
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
    [InverseProperty("EntityAccountChargeReversals")]
    public virtual EntityAccountCharge FkEntityAccountCharge { get; set; } = null!;
}


