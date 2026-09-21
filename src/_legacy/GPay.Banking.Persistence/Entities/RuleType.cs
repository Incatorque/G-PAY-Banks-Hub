using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class RuleType
{
    public Guid PkRuleTypeId { get; set; }

    public string? Type { get; set; }
}
