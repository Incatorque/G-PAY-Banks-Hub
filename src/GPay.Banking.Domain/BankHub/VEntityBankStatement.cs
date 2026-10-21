using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VEntityBankStatement
{
    [Column("PkEntityBankStatementID")]
    public Guid PkEntityBankStatementId { get; set; }

    [Column("TAName")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Taname { get; set; }

    [StringLength(30)]
    public string AccountNumber { get; set; } = null!;

    [StringLength(250)]
    [Unicode(false)]
    public string? Description { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime TransactionDate { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? ReferenceNumber { get; set; }

    [StringLength(30)]
    [Unicode(false)]
    public string? TranCode { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal Amount { get; set; }

    public bool? IsIntra { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime SyncDate { get; set; }

    public bool? Active { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Bank { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? EventNumber { get; set; }

    [Column("FkAccountID")]
    public Guid? FkAccountId { get; set; }

    [Column("FkEntityID")]
    public Guid? FkEntityId { get; set; }

    [Column("FkClassCategoryID")]
    public Guid? FkClassCategoryId { get; set; }

    public bool? IsProcessed { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string? AccountLabel { get; set; }

    [StringLength(20)]
    public string? ResourceType { get; set; }

    [StringLength(3)]
    [Unicode(false)]
    public string Reconciled { get; set; } = null!;

    [StringLength(183)]
    public string? FormattedAccountDetails { get; set; }

    [StringLength(3)]
    [Unicode(false)]
    public string IsCopy { get; set; } = null!;
}


