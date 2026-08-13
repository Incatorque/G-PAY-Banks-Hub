using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VEntityBankStatementMapping
{
    public Guid PkEntityBankStatementId { get; set; }

    public string? Taname { get; set; }

    public string AccountNumber { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime TransactionDate { get; set; }

    public string ReferenceNumber { get; set; } = null!;

    public decimal Amount { get; set; }

    public bool? IsIntra { get; set; }

    public DateTime SyncDate { get; set; }

    public string Bank { get; set; } = null!;

    public string? EventNumber { get; set; }

    public Guid? FkAccountId { get; set; }

    public Guid? FkEntityId { get; set; }

    public Guid? FkClassCategoryId { get; set; }

    public string? MappedCat { get; set; }

    public DateTime? DateMapped { get; set; }

    public string? Type { get; set; }

    public DateTime? MappedDate { get; set; }

    public bool? IsProcessed { get; set; }

    public string? AccountLabel { get; set; }

    public Guid? FkRuleTypeId { get; set; }

    public string? Name { get; set; }

    public string? FormattedAccountDetails { get; set; }

    public string? RuleText { get; set; }

    public bool? Active { get; set; }
}
