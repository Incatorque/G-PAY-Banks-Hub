using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
[Table("EntityBankStatementTemp")]
public partial class EntityBankStatementTemp
{
    [Column("PkEntityBankStatementID")]
    public Guid PkEntityBankStatementId { get; set; }

    [Column("FkBankInfoID")]
    public Guid? FkBankInfoId { get; set; }

    [StringLength(250)]
    [Unicode(false)]
    public string? Description { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string ReferenceNumber { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime TransactionDate { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? Amount { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime SyncDate { get; set; }

    public bool? Reconciled { get; set; }

    [Column("FkEntityID")]
    public Guid? FkEntityId { get; set; }

    [Column("FkAccountID")]
    public Guid? FkAccountId { get; set; }

    public bool? IsIntra { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? EventNumber { get; set; }

    [Column("FkClassCategoryID")]
    public Guid? FkClassCategoryId { get; set; }

    public bool? IsProcessed { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateMapped { get; set; }

    public int TransactionCodeId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? SupplierBankReference { get; set; }
}


