using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VEntityBankStatementDeleted
{
    public Guid PkEntityBankStatementId { get; set; }

    public string? Taname { get; set; }

    public string AccountNumber { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime TransactionDate { get; set; }

    public string? ReferenceNumber { get; set; }

    public decimal Amount { get; set; }

    public bool? IsIntra { get; set; }

    public DateTime SyncDate { get; set; }

    public bool? Active { get; set; }

    public string? Bank { get; set; }

    public string? EventNumber { get; set; }

    public Guid? FkAccountId { get; set; }

    public Guid? FkEntityId { get; set; }

    public Guid? FkClassCategoryId { get; set; }

    public bool? IsProcessed { get; set; }

    public string? AccountLabel { get; set; }

    public string? ResourceType { get; set; }

    public string Reconciled { get; set; } = null!;

    public string? FormattedAccountDetails { get; set; }

    public string IsCopy { get; set; } = null!;
}
