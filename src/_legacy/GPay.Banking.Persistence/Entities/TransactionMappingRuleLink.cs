using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class TransactionMappingRuleLink
{
    public Guid PkRuleLink { get; set; }

    public Guid FkRuleId { get; set; }

    public Guid? FkEntityBankStatementId { get; set; }

    public DateTime? MappedDate { get; set; }

    public virtual EntityBankStatement? FkEntityBankStatement { get; set; }

    public virtual TransactionMappingRule FkRule { get; set; } = null!;
}
