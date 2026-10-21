using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("BaseProductZone")]
public partial class BaseProductZone : Entity<Guid>
{
    [Key]
    [Column("PkBaseProductZoneID")]
    public override Guid Id { get; protected set; }

    [Column("FkGridZoneID")]
    public Guid FkGridZoneId { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? Differential { get; set; }

    [Column("FkBaseProductPriceID")]
    public Guid FkBaseProductPriceId { get; set; }

    public bool? Active { get; set; }

    [ForeignKey("FkBaseProductPriceId")]
    [InverseProperty("BaseProductZones")]
    public virtual BaseProductPrice FkBaseProductPrice { get; set; } = null!;

    [ForeignKey("FkGridZoneId")]
    [InverseProperty("BaseProductZones")]
    public virtual GridZone FkGridZone { get; set; } = null!;
}


