using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class GetProductTransactionCodeId
{
    [Column("PkOrderID")]
    public Guid PkOrderId { get; set; }

    [Column("PkProductID")]
    public Guid PkProductId { get; set; }

    [Column("TransactionCodeID")]
    public int? TransactionCodeId { get; set; }

    [StringLength(200)]
    public string? TransactionDescription { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Code { get; set; } = null!;

    [StringLength(2500)]
    [Unicode(false)]
    public string Description { get; set; } = null!;
}


