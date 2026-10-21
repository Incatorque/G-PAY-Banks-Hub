using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VbaseProductAll
{
    [Column("BaseProductID")]
    public Guid BaseProductId { get; set; }

    public bool? BaseProductActive { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? BaseProductCode { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string? BaseProductDescription { get; set; }

    [Column("BaseProductGridID")]
    public Guid? BaseProductGridId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime EffectiveDate { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? BaseProductPrice { get; set; }

    [Column("BaseProductPriceID")]
    public Guid BaseProductPriceId { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? Differential { get; set; }

    [Column("GridZOneDescription")]
    [StringLength(50)]
    [Unicode(false)]
    public string GridZoneDescription { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string GridDescription { get; set; } = null!;

    [Column("MagisterialDistrictGridZoneID")]
    public Guid MagisterialDistrictGridZoneId { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string MagisterialDistrictName { get; set; } = null!;
}


