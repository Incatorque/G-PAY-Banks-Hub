using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VOrderSupplier
{
    [Column("FkOrderID")]
    public Guid FkOrderId { get; set; }

    [Column("FkSupplierID")]
    public Guid FkSupplierId { get; set; }

    public int Payment { get; set; }
}


