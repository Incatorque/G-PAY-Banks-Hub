using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.EntityFrameworkCore.GPay;

public sealed class GpayOrder
{
    public Guid Id { get; set; }
    public int OrderNumber { get; set; }
    public Guid FkOrderStatusId { get; set; }
    public Guid FkClientId { get; set; }
    public Guid? FkFromAccountId { get; set; }
    public Guid? FkEntityBankStatementId { get; set; }
    public string? ReferenceNumber { get; set; }
    public string? SupplierBankReference { get; set; }
    public string? ExtRef { get; set; }
    public bool? Active { get; set; }
}

public sealed class GpayOrderItem
{
    public Guid Id { get; set; }
    public Guid FkOrderId { get; set; }
    public Guid FkOrderStatusId { get; set; }
    public DateTime StatusChangeDate { get; set; }
    public bool? Active { get; set; }
}

public sealed class GpayOrderHistory
{
    public Guid Id { get; set; }
    public Guid FkOrderId { get; set; }
    public Guid FkOrderStatusId { get; set; }
    public DateTime Date { get; set; }
    public string? Note { get; set; }
    public Guid? FkUserId { get; set; }
    public Guid FkHistoryCategoryId { get; set; }
    public bool? Active { get; set; }
}

public sealed class GpayEntityBankStatement
{
    public Guid Id { get; set; }
    public Guid? FkBankInfoId { get; set; }
    public string? Description { get; set; }
    public string? ReferenceNumber { get; set; }
    public DateTime TransactionDate { get; set; }
    public decimal Amount { get; set; }
    public DateTime SyncDate { get; set; }
    public bool? Reconciled { get; set; }
    public Guid? FkEntityId { get; set; }
    public Guid? FkAccountId { get; set; }
    public bool? IsIntra { get; set; }
    public string? EventNumber { get; set; }
    public bool? IsProcessed { get; set; }
    public int TransactionCodeId { get; set; }
    public string? SupplierBankReference { get; set; }
    public bool? Active { get; set; }
}

public sealed class GpayTransactionCode
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool? Active { get; set; }
    public bool? ApplyOnDebit { get; set; }
}

public sealed class GPayLegacyDbContext : DbContext
{
    public GPayLegacyDbContext(DbContextOptions<GPayLegacyDbContext> options)
        : base(options)
    {
    }

    public DbSet<GpayOrder> Orders => Set<GpayOrder>();
    public DbSet<GpayOrderItem> OrderItems => Set<GpayOrderItem>();
    public DbSet<GpayOrderHistory> OrderHistories => Set<GpayOrderHistory>();
    public DbSet<GpayEntityBankStatement> EntityBankStatements => Set<GpayEntityBankStatement>();
    public DbSet<GpayTransactionCode> TransactionCodes => Set<GpayTransactionCode>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GpayOrder>(b =>
        {
            b.ToTable("Order");
            b.HasKey(x => x.Id);
            b.Property(x => x.Id).HasColumnName("PkOrderID");
            b.Property(x => x.FkOrderStatusId).HasColumnName("FkOrderStatusID");
            b.Property(x => x.FkClientId).HasColumnName("FkClientID");
            b.Property(x => x.FkFromAccountId).HasColumnName("FkFromAccountID");
            b.Property(x => x.FkEntityBankStatementId).HasColumnName("FkEntityBankStatementID");
            b.Property(x => x.ReferenceNumber).HasMaxLength(20).IsUnicode(false);
            b.Property(x => x.SupplierBankReference).HasMaxLength(50).IsUnicode(false);
            b.Property(x => x.ExtRef).HasMaxLength(50).IsUnicode(false);
        });

        modelBuilder.Entity<GpayOrderItem>(b =>
        {
            b.ToTable("OrderItem");
            b.HasKey(x => x.Id);
            b.Property(x => x.Id).HasColumnName("PkOrderItemID");
            b.Property(x => x.FkOrderId).HasColumnName("FkOrderID");
            b.Property(x => x.FkOrderStatusId).HasColumnName("FkOrderStatusID");
            b.Property(x => x.StatusChangeDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<GpayOrderHistory>(b =>
        {
            b.ToTable("OrderHistory");
            b.HasKey(x => x.Id);
            b.Property(x => x.Id).HasColumnName("PkOrderHistoryID");
            b.Property(x => x.FkOrderId).HasColumnName("FkOrderID");
            b.Property(x => x.FkOrderStatusId).HasColumnName("FkOrderStatusID");
            b.Property(x => x.FkUserId).HasColumnName("FkUserID");
            b.Property(x => x.FkHistoryCategoryId).HasColumnName("FkHistoryCategoryID");
            b.Property(x => x.Date).HasColumnType("datetime");
            b.Property(x => x.Note).HasMaxLength(500);
        });

        modelBuilder.Entity<GpayEntityBankStatement>(b =>
        {
            b.ToTable("EntityBankStatement");
            b.HasKey(x => x.Id);
            b.Property(x => x.Id).HasColumnName("PkEntityBankStatementID");
            b.Property(x => x.FkBankInfoId).HasColumnName("FkBankInfoID");
            b.Property(x => x.FkEntityId).HasColumnName("FkEntityID");
            b.Property(x => x.FkAccountId).HasColumnName("FkAccountID");
            b.Property(x => x.TransactionDate).HasColumnType("datetime");
            b.Property(x => x.SyncDate).HasColumnType("datetime");
            b.Property(x => x.Amount).HasPrecision(19, 4);
            b.Property(x => x.Description).HasMaxLength(250).IsUnicode(false);
            b.Property(x => x.ReferenceNumber).HasMaxLength(50).IsUnicode(false);
            b.Property(x => x.EventNumber).HasMaxLength(50).IsUnicode(false);
            b.Property(x => x.SupplierBankReference).HasMaxLength(50).IsUnicode(false);
        });

        modelBuilder.Entity<GpayTransactionCode>(b =>
        {
            b.ToTable("TransactionCode");
            b.HasKey(x => x.Id);
            b.Property(x => x.Id).HasColumnName("ID");
            b.Property(x => x.Name).HasMaxLength(30).IsUnicode(false);
        });
    }
}
