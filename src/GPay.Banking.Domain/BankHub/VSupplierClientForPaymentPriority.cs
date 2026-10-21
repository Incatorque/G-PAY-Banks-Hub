using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VSupplierClientForPaymentPriority
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

    [StringLength(100)]
    [Unicode(false)]
    public string ClientName { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string SupplierName { get; set; } = null!;

    [Column("FkSupplierAccountID")]
    public Guid FkSupplierAccountId { get; set; }

    [Column("FkClientAccountID")]
    public Guid FkClientAccountId { get; set; }

    [Column("PPOrder")]
    public int? Pporder { get; set; }

    [Column("PPAmount", TypeName = "decimal(19, 4)")]
    public decimal? Ppamount { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string? EntityType { get; set; }
}


