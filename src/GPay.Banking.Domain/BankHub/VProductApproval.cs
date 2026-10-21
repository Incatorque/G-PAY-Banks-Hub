using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VProductApproval
{
    [StringLength(100)]
    [Unicode(false)]
    public string Supplier { get; set; } = null!;

    [Column("PkSupplierID")]
    public Guid PkSupplierId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Product { get; set; }

    [Column("iOrder")]
    public int IOrder { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Username { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Status { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string Client { get; set; } = null!;

    [Column("PkClientID")]
    public Guid PkClientId { get; set; }

    public bool? Active { get; set; }

    [Column("PkClientProductApprovalID")]
    public Guid PkClientProductApprovalId { get; set; }
}


