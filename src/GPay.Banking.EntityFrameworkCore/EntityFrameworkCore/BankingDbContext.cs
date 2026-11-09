using GPay.Banking.BankHub;
using GPay.Banking.EntityFrameworkCore.GPay;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace GPay.Banking.EntityFrameworkCore;

[ConnectionStringName("Default")]
public class BankingDbContext : AbpDbContext<BankingDbContext>
{
    public DbSet<BankHubApiCall> BankHubApiCalls { get; set; } = default!;
    public DbSet<BankHubAvsBatch> BankHubAvsBatches { get; set; } = default!;
    public DbSet<BankHubAvsRecord> BankHubAvsRecords { get; set; } = default!;

    public BankingDbContext(DbContextOptions<BankingDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<BankHubAvsBatch>(b =>
        {
            b.ToTable("AppBankHubAvsBatches");
            b.ConfigureByConvention();
            b.Property(x => x.FileName).IsRequired().HasMaxLength(256);
            b.Property(x => x.Status).IsRequired().HasMaxLength(32);
            b.Property(x => x.BankCode).HasMaxLength(32);
            b.HasIndex(x => x.ExternalBatchId);
            b.HasIndex(x => x.ApiClientId);
        });

        builder.Entity<BankHubAvsRecord>(b =>
        {
            b.ToTable("AppBankHubAvsRecords");
            b.ConfigureByConvention();
            b.Property(x => x.BankCode).IsRequired().HasMaxLength(32);
            b.Property(x => x.AccountNumber).IsRequired().HasMaxLength(32);
            b.Property(x => x.BranchCode).IsRequired().HasMaxLength(16);
            b.Property(x => x.CorrelationId).HasMaxLength(128);
            b.Property(x => x.Status).HasMaxLength(32);
            b.Property(x => x.SuccessRate).HasPrecision(5, 2);
            b.HasIndex(x => x.BatchId);
            b.HasIndex(x => x.CorrelationId);
            b.HasIndex(x => x.ApiClientId);
        });

        builder.Entity<BankHubApiCall>(b =>
        {
            b.ToTable("AppBankHubApiCalls", "dbo");
            b.ConfigureByConvention();
            b.Property(x => x.BankCode).IsRequired().HasMaxLength(32);
            b.Property(x => x.Direction).IsRequired().HasMaxLength(16);
            b.Property(x => x.Operation).IsRequired().HasMaxLength(64);
            b.Property(x => x.CorrelationId).HasMaxLength(128);
            b.HasIndex(x => x.GpayOrderId);
            b.HasIndex(x => x.CreationTime);
            b.HasIndex(x => x.CorrelationId);
            b.HasIndex(x => x.Operation);
        });

        // Minimal GPay read models used by the Bank Hub-ready order poller. These deliberately
        // omit the legacy navigation graph, while still querying through this context.
        builder.Entity<GpayOrderRow>(b =>
        {
            b.ToTable("Order", "dbo");
            b.HasKey(x => x.Id);
            b.Property(x => x.Id).HasColumnName("PkOrderID");
            b.Property(x => x.StatusId).HasColumnName("FkOrderStatusID");
            b.Property(x => x.FromAccountId).HasColumnName("FkFromAccountID");
            b.Property(x => x.ToAccountId).HasColumnName("FkToAccountID");
            b.Property(x => x.UserId).HasColumnName("FkUserID");
            b.Property(x => x.EntityBankStatementId).HasColumnName("FkEntityBankStatementID");
        });

        builder.Entity<GpayEntityBankStatementRow>(b =>
        {
            b.ToTable("EntityBankStatement", "dbo");
            b.HasKey(x => x.Id);
            b.Property(x => x.Id).HasColumnName("PkEntityBankStatementID");
            b.Property(x => x.EntityId).HasColumnName("FkEntityID");
            b.Property(x => x.AccountId).HasColumnName("FkAccountID");
        });

        builder.Entity<GpayEntityBankStatementLogRow>(b =>
        {
            b.ToTable("EntityBankStatementLog", "dbo");
            b.HasKey(x => x.Id);
            b.Property(x => x.Id).HasColumnName("PkEntityBankStatementLog");
            b.Property(x => x.EntityBankStatementId).HasColumnName("FKEntityBankStatementId");
            b.Property(x => x.UserId).HasColumnName("FKUserId");
        });

        builder.Entity<GpayTransactionCodeRow>(b =>
        {
            b.ToTable("TransactionCode", "dbo");
            b.HasKey(x => x.Id);
            b.Property(x => x.Id).HasColumnName("ID");
        });

        builder.Entity<GpayOrderHistoryRow>(b =>
        {
            b.ToTable("OrderHistory", "dbo");
            b.HasKey(x => x.Id);
            b.Property(x => x.Id).HasColumnName("PkOrderHistoryID");
            b.Property(x => x.OrderId).HasColumnName("FkOrderID");
            b.Property(x => x.StatusId).HasColumnName("FkOrderStatusID");
            b.Property(x => x.HistoryCategoryId).HasColumnName("FkHistoryCategoryID");
            b.Property(x => x.UserId).HasColumnName("FkUserID");
        });

        builder.Entity<GpayOrderStatusRow>(b =>
        {
            b.ToTable("OrderStatus", "dbo");
            b.HasKey(x => x.Id);
            b.Property(x => x.Id).HasColumnName("PkOrderStatusID");
        });

        builder.Entity<GpayOrderItemRow>(b =>
        {
            b.ToTable("OrderItem", "dbo");
            b.HasKey(x => x.Id);
            b.Property(x => x.Id).HasColumnName("PkOrderItemID");
            b.Property(x => x.OrderId).HasColumnName("FkOrderID");
        });

        builder.Entity<GpayAccountRow>(b =>
        {
            b.ToTable("Account", "dbo");
            b.HasKey(x => x.Id);
            b.Property(x => x.Id).HasColumnName("PkAccountID");
            b.Property(x => x.BankId).HasColumnName("FkBankID");
            b.Property(x => x.CountryCurrencyId).HasColumnName("CountryCurrencyID");
            b.Property(x => x.EntityId).HasColumnName("FkEntityID");
        });

        builder.Entity<GpayCountryCurrencyRow>(b =>
        {
            b.ToTable("CountryCurrency", "dbo");
            b.HasKey(x => x.Id);
            b.Property(x => x.Id).HasColumnName("ID");
            b.Property(x => x.CurrencyId).HasColumnName("CurrencyID");
        });

        builder.Entity<GpayCurrencyRow>(b =>
        {
            b.ToTable("Currency", "dbo");
            b.HasKey(x => x.Id);
            b.Property(x => x.Id).HasColumnName("ID");
            b.Property(x => x.Code).HasColumnName("code");
        });

        builder.Entity<GpayBankRow>(b =>
        {
            b.ToTable("Bank", "dbo");
            b.HasKey(x => x.Id);
            b.Property(x => x.Id).HasColumnName("PkBankID");
        });

        builder.Entity<GpayHolidayRow>(b =>
        {
            b.ToTable("Holiday", "dbo");
            b.HasKey(x => x.Id);
            b.Property(x => x.Id).HasColumnName("ID");
        });

        builder.Entity<GpayEntityRow>(b =>
        {
            b.ToTable("Entity", "dbo");
            b.HasKey(x => x.Id);
            b.Property(x => x.Id).HasColumnName("PkEntityID");
        });
    }
}
