using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("BaseProductPrice")]
public partial class BaseProductPrice : Entity<Guid>
{
    [Key]
    [Column("PkBaseProductPriceID")]
    public override Guid Id { get; protected set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? Price { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime EffectiveDate { get; set; }

    [Column("FkBaseProductID")]
    public Guid FkBaseProductId { get; set; }

    public bool? Active { get; set; }

    [InverseProperty("FkBaseProductPrice")]
    public virtual ICollection<BaseProductZone> BaseProductZones { get; set; } = new List<BaseProductZone>();

    [ForeignKey("FkBaseProductId")]
    [InverseProperty("BaseProductPrices")]
    public virtual BaseProduct FkBaseProduct { get; set; } = null!;
}


