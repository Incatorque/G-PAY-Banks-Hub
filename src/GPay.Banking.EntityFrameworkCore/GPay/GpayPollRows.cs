namespace GPay.Banking.EntityFrameworkCore.GPay;

// Small EF projections for the GPay tables used by the PayShap poller.
// They intentionally have no navigations to avoid loading GPay's legacy model graph.
internal sealed class GpayOrderRow
{
    public Guid Id { get; set; }
    public int OrderNumber { get; set; }
    public DateTime OrderDate { get; set; }
    public Guid StatusId { get; set; }
    public bool? Active { get; set; }
    public Guid? FromAccountId { get; set; }
    public Guid? ToAccountId { get; set; }
    public string? ReferenceNumber { get; set; }
    public Guid? UserId { get; set; }
    public Guid? EntityBankStatementId { get; set; }
}

internal sealed class GpayOrderHistoryRow
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid StatusId { get; set; }
    public Guid HistoryCategoryId { get; set; }
    public DateTime Date { get; set; }
    public string? Note { get; set; }
    public Guid? UserId { get; set; }
}

internal sealed class GpayOrderStatusRow
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

internal sealed class GpayOrderItemRow
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public decimal? Price { get; set; }
    public decimal? Quantity { get; set; }
}

internal sealed class GpayAccountRow
{
    public Guid Id { get; set; }
    public Guid BankId { get; set; }
    public int? CountryCurrencyId { get; set; }
    public Guid? EntityId { get; set; }
    public bool? Active { get; set; }
    public string AccountNumber { get; set; } = string.Empty;
    public string BranchCode { get; set; } = string.Empty;
}

internal sealed class GpayCountryCurrencyRow
{
    public int Id { get; set; }
    public int CurrencyId { get; set; }
}

internal sealed class GpayCurrencyRow
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
}

internal sealed class GpayBankRow
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool? Active { get; set; }
}

internal sealed class GpayEntityRow
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

internal sealed class GpayEntityBankStatementRow
{
    public Guid Id { get; set; }
    public string? Description { get; set; }
    public string? ReferenceNumber { get; set; }
    public DateTime TransactionDate { get; set; }
    public decimal Amount { get; set; }
    public DateTime SyncDate { get; set; }
    public bool? Reconciled { get; set; }
    public Guid? EntityId { get; set; }
    public Guid? AccountId { get; set; }
    public bool? IsIntra { get; set; }
    public bool? IsProcessed { get; set; }
    public int TransactionCodeId { get; set; }
    public string? SupplierBankReference { get; set; }
    public bool? Active { get; set; }
}

internal sealed class GpayEntityBankStatementLogRow
{
    public Guid Id { get; set; }
    public Guid EntityBankStatementId { get; set; }
    public DateTime UpdatedDate { get; set; }
    public Guid UserId { get; set; }
    public string UpdateDescription { get; set; } = string.Empty;
    public bool? IsCopied { get; set; }
}

internal sealed class GpayTransactionCodeRow
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool? Active { get; set; }
}
