using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class TransactionMappingRule
{
    public Guid PkRuleId { get; set; }

    public Guid? FkAccountId { get; set; }

    public Guid? FkClassCatagoryId { get; set; }

    public Guid? FkRuleTypeId { get; set; }

    public Guid? FkEntityId { get; set; }

    public string? TransactionDescription { get; set; }

    public string? RuleText { get; set; }

    public Guid? FkEntityBankStatementId { get; set; }

    public DateTime? MappedDate { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<TransactionMappingRuleLink> TransactionMappingRuleLinks { get; set; } = new List<TransactionMappingRuleLink>();
}
