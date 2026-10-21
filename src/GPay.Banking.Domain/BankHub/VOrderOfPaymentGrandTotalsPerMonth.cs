using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VOrderOfPaymentGrandTotalsPerMonth
{
    [Column("PkSupplierClientID")]
    public Guid? PkSupplierClientId { get; set; }

    [Column("FkClientID")]
    public Guid FkClientId { get; set; }

    [Column("FkSupplierID")]
    public Guid FkSupplierId { get; set; }

    [Column("GT", TypeName = "decimal(38, 6)")]
    public decimal? Gt { get; set; }

    [Column(TypeName = "decimal(38, 6)")]
    public decimal? LastPaymentAmount { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastPaymentDate { get; set; }

    [StringLength(53)]
    [Unicode(false)]
    public string? ValidDateRange { get; set; }
}


