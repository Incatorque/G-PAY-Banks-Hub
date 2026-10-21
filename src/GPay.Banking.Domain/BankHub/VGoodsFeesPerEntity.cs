using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VGoodsFeesPerEntity
{
    [StringLength(100)]
    [Unicode(false)]
    public string? ShortName { get; set; }

    [Column(TypeName = "decimal(25, 8)")]
    public decimal? Value { get; set; }

    [Column("FkSupplierID")]
    public Guid FkSupplierId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Agent { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string CommissionType { get; set; } = null!;
}


