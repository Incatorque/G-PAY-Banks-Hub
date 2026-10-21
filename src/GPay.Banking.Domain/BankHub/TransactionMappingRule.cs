using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

public partial class TransactionMappingRule : Entity<Guid>
{
    [Key]
    [Column("PkRuleID")]
    public override Guid Id { get; protected set; }

    [Column("FkAccountID")]
    public Guid? FkAccountId { get; set; }

    [Column("FkClassCatagoryID")]
    public Guid? FkClassCatagoryId { get; set; }

    [Column("FkRuleTypeID")]
    public Guid? FkRuleTypeId { get; set; }

    [Column("FkEntityID")]
    public Guid? FkEntityId { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string? TransactionDescription { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? RuleText { get; set; }

    [Column("FkEntityBankStatementID")]
    public Guid? FkEntityBankStatementId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? MappedDate { get; set; }

    public bool? Active { get; set; }

    [InverseProperty("FkRule")]
    public virtual ICollection<TransactionMappingRuleLink> TransactionMappingRuleLinks { get; set; } = new List<TransactionMappingRuleLink>();
}


