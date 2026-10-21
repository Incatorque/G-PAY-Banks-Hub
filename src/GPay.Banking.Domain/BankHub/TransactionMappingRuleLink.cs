using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

public partial class TransactionMappingRuleLink : Entity<Guid>
{
    [Key]
    [Column("PkRuleLink")]
    public override Guid Id { get; protected set; }

    [Column("FkRuleID")]
    public Guid FkRuleId { get; set; }

    [Column("FkEntityBankStatementID")]
    public Guid? FkEntityBankStatementId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? MappedDate { get; set; }

    [ForeignKey("FkEntityBankStatementId")]
    [InverseProperty("TransactionMappingRuleLinks")]
    public virtual EntityBankStatement? FkEntityBankStatement { get; set; }

    [ForeignKey("FkRuleId")]
    [InverseProperty("TransactionMappingRuleLinks")]
    public virtual TransactionMappingRule FkRule { get; set; } = null!;
}


