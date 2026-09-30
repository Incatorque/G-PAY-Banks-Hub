using GPay.Banking.BankHub;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace GPay.Banking.EntityFrameworkCore;

[ConnectionStringName("Default")]
public class BankingDbContext : AbpDbContext<BankingDbContext>
{
    public DbSet<BankHubAvsBatch> BankHubAvsBatches { get; set; } = null!;
    public DbSet<BankHubAvsRecord> BankHubAvsRecords { get; set; } = null!;
    public DbSet<BankHubPaymentRecord> BankHubPaymentRecords { get; set; } = null!;
    public DbSet<BankHubApiCall> BankHubApiCalls { get; set; } = null!;

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
            b.Property(x => x.Status).HasMaxLength(32);
            b.Property(x => x.SuccessRate).HasPrecision(5, 2);
            b.HasIndex(x => x.BatchId);
            b.HasIndex(x => x.CorrelationId);
            b.HasIndex(x => x.ApiClientId);
        });

        builder.Entity<BankHubPaymentRecord>(b =>
        {
            b.ToTable("AppBankHubPaymentRecords");
            b.ConfigureByConvention();
            b.Property(x => x.BankCode).IsRequired().HasMaxLength(32);
            b.Property(x => x.Currency).IsRequired().HasMaxLength(8);
            b.Property(x => x.PaymentRail).IsRequired().HasMaxLength(16);
            b.Property(x => x.Status).IsRequired().HasMaxLength(32);
            b.Property(x => x.FromAccountNumber).IsRequired().HasMaxLength(32);
            b.Property(x => x.ToAccountNumber).IsRequired().HasMaxLength(32);
            b.Property(x => x.Amount).HasPrecision(18, 2);
            b.HasIndex(x => x.CorrelationId);
            b.HasIndex(x => x.TransactionReference);
            b.HasIndex(x => x.ApiReference);
            b.HasIndex(x => x.Status);
        });

        builder.Entity<BankHubApiCall>(b =>
        {
            b.ToTable("AppBankHubApiCalls");
            b.ConfigureByConvention();
            b.Property(x => x.BankCode).IsRequired().HasMaxLength(32);
            b.Property(x => x.Direction).IsRequired().HasMaxLength(16);
            b.Property(x => x.Operation).IsRequired().HasMaxLength(64);
            b.HasIndex(x => x.CreationTime);
            b.HasIndex(x => x.CorrelationId);
            b.HasIndex(x => x.Operation);
        });
    }
}
