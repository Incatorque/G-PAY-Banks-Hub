using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VproductAllocationOver
{
    [Column("PkProductID")]
    public Guid PkProductId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Code { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string? ShortName { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Unit { get; set; } = null!;

    public int Excluded { get; set; }

    [Column("ProductAllocationQTY", TypeName = "decimal(19, 4)")]
    public decimal? ProductAllocationQty { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? ProductAllocationTerms { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? OverridgeQty { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? OverridgeTerms { get; set; }

    [Column("PkSupplierClientID")]
    public Guid PkSupplierClientId { get; set; }

    [Column("PkAllocationOverID")]
    public Guid? PkAllocationOverId { get; set; }

    [Column("PkProductZoneID")]
    public Guid? PkProductZoneId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateCreated { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string? SupplierZoneName { get; set; }
}


