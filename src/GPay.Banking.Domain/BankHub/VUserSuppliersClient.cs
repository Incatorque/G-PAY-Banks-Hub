using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VUserSuppliersClient
{
    [Column("PkSupplierClientID")]
    public Guid PkSupplierClientId { get; set; }

    [Column("FkSupplierID")]
    public Guid FkSupplierId { get; set; }

    [Column("FkClientID")]
    public Guid FkClientId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? SupplierClientRefrence { get; set; }

    [Column("FkUserID")]
    public Guid FkUserId { get; set; }
}


