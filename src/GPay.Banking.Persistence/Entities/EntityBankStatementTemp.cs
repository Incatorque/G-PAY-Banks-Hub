using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class EntityBankStatementTemp
{
    public Guid PkEntityBankStatementId { get; set; }

    public Guid? FkBankInfoId { get; set; }

    public string? Description { get; set; }

    public string ReferenceNumber { get; set; } = null!;

    public DateTime TransactionDate { get; set; }

    public decimal? Amount { get; set; }

    public DateTime SyncDate { get; set; }

    public bool? Reconciled { get; set; }

    public Guid? FkEntityId { get; set; }

    public Guid? FkAccountId { get; set; }

    public bool? IsIntra { get; set; }

    public string? EventNumber { get; set; }

    public Guid? FkClassCategoryId { get; set; }

    public bool? IsProcessed { get; set; }

    public DateTime? DateMapped { get; set; }

    public int TransactionCodeId { get; set; }

    public string? SupplierBankReference { get; set; }
}
