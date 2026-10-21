using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VOrderOfPaymentView
{
    [Column("PkSupplierClientID")]
    public Guid PkSupplierClientId { get; set; }

    [Column("ClientID")]
    public Guid ClientId { get; set; }

    [Column("SupplierID")]
    public Guid SupplierId { get; set; }

    [StringLength(8000)]
    [Unicode(false)]
    public string? ClientName { get; set; }

    [StringLength(8000)]
    [Unicode(false)]
    public string? SupplierName { get; set; }

    [Column(TypeName = "decimal(38, 6)")]
    public decimal LastPaymentAmount { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastPaymentDate { get; set; }

    [StringLength(53)]
    [Unicode(false)]
    public string ValidDateRange { get; set; } = null!;

    [Column(TypeName = "decimal(38, 6)")]
    public decimal MonthToDate { get; set; }

    [Column("PPOrder")]
    public int? Pporder { get; set; }

    [Column("PPAmount", TypeName = "decimal(19, 4)")]
    public decimal? Ppamount { get; set; }

    public bool Active { get; set; }
}


