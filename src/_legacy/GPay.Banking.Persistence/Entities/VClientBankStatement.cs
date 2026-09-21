using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VClientBankStatement
{
    public Guid? PkEntityBankStatementId { get; set; }

    public string? Description { get; set; }

    public string? ReferenceNumber { get; set; }

    public DateTime? TransactionDate { get; set; }

    public decimal? Amount { get; set; }

    public DateTime? SyncDate { get; set; }

    public bool? Reconciled { get; set; }

    public Guid? FkClientId { get; set; }

    public string? AccountNumber { get; set; }

    public Guid? PkAccountId { get; set; }

    public bool? IsIntra { get; set; }

    public string? SupplierBankReference { get; set; }

    public string? InvoiceNumber { get; set; }

    public string? ToEntityName { get; set; }

    public string Test { get; set; } = null!;
}
