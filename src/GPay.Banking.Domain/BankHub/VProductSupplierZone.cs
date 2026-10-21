using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VProductSupplierZone
{
    [Column("PkSupplierZoneID")]
    public Guid PkSupplierZoneId { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("FkProductID")]
    public Guid FkProductId { get; set; }

    public bool? Active { get; set; }

    [Column("FkSupplierID")]
    public Guid FkSupplierId { get; set; }
}


