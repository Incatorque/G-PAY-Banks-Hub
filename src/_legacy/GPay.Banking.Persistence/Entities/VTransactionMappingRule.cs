using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VTransactionMappingRule
{
    public Guid PkRuleId { get; set; }

    public string? EntityTa { get; set; }

    public string? RuleType { get; set; }

    public string Category { get; set; } = null!;

    public string? Description { get; set; }

    public string? TransactionDescription { get; set; }

    public string? AccountNumber { get; set; }

    public string? AccountLabel { get; set; }

    public Guid? FkAccountId { get; set; }

    public Guid? FkEntityId { get; set; }

    public bool? Active { get; set; }

    public string Class { get; set; } = null!;

    public DateTime? MappedDate { get; set; }

    public Guid? FkEntityBankStatementId { get; set; }

    public string RuleOrigin { get; set; } = null!;

    public Guid? FkClassCatagoryId { get; set; }

    public Guid? FkRuleTypeId { get; set; }

    public string? FormattedAccountDetails { get; set; }
}
