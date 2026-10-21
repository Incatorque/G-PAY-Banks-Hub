using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VTransactionMappingRule
{
    public Guid PkRuleId { get; set; }

    [Column("EntityTA")]
    [StringLength(100)]
    [Unicode(false)]
    public string? EntityTa { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? RuleType { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string Category { get; set; } = null!;

    [StringLength(500)]
    [Unicode(false)]
    public string? Description { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string? TransactionDescription { get; set; }

    [StringLength(30)]
    public string? AccountNumber { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string? AccountLabel { get; set; }

    [Column("FkAccountID")]
    public Guid? FkAccountId { get; set; }

    public Guid? FkEntityId { get; set; }

    public bool? Active { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Class { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? MappedDate { get; set; }

    public Guid? FkEntityBankStatementId { get; set; }

    [StringLength(1)]
    [Unicode(false)]
    public string RuleOrigin { get; set; } = null!;

    [Column("FkClassCatagoryID")]
    public Guid? FkClassCatagoryId { get; set; }

    [Column("FkRuleTypeID")]
    public Guid? FkRuleTypeId { get; set; }

    [StringLength(183)]
    public string? FormattedAccountDetails { get; set; }
}


