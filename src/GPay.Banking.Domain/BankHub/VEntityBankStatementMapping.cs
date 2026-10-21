using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VEntityBankStatementMapping
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
    public string ReferenceNumber { get; set; } = null!;

    [Column(TypeName = "decimal(19, 4)")]
    public decimal Amount { get; set; }

    public bool? IsIntra { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime SyncDate { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Bank { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? EventNumber { get; set; }

    [Column("FkAccountID")]
    public Guid? FkAccountId { get; set; }

    [Column("FkEntityID")]
    public Guid? FkEntityId { get; set; }

    [Column("FkClassCategoryID")]
    public Guid? FkClassCategoryId { get; set; }

    [StringLength(182)]
    [Unicode(false)]
    public string? MappedCat { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateMapped { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Type { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? MappedDate { get; set; }

    public bool? IsProcessed { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string? AccountLabel { get; set; }

    [Column("FkRuleTypeID")]
    public Guid? FkRuleTypeId { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string? Name { get; set; }

    [StringLength(183)]
    public string? FormattedAccountDetails { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string? RuleText { get; set; }

    public bool? Active { get; set; }
}


