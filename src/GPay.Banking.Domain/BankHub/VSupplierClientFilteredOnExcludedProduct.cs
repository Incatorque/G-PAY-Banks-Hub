using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VSupplierClientFilteredOnExcludedProduct
{
    [Column("FkSupplierID")]
    public Guid FkSupplierId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("PkSupplierClientID")]
    public Guid PkSupplierClientId { get; set; }

    [Column("FkClientID")]
    public Guid FkClientId { get; set; }
}


