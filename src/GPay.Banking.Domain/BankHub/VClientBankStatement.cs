using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VClientBankStatement
{
    [Column("PkEntityBankStatementID")]
    public Guid? PkEntityBankStatementId { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string? Description { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? ReferenceNumber { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? TransactionDate { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? Amount { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? SyncDate { get; set; }

    public bool? Reconciled { get; set; }

    [Column("FkClientID")]
    public Guid? FkClientId { get; set; }

    [StringLength(30)]
    public string? AccountNumber { get; set; }

    [Column("PkAccountID")]
    public Guid? PkAccountId { get; set; }

    public bool? IsIntra { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? SupplierBankReference { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? InvoiceNumber { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? ToEntityName { get; set; }

    [Column("test")]
    [StringLength(1)]
    [Unicode(false)]
    public string Test { get; set; } = null!;
}


