using GPay.Banking.Contracts.Entities;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.Persistence.Data;

public partial class BankingDbContext
{
    public virtual DbSet<AvsBatch> AvsBatches { get; set; } = null!;

    public virtual DbSet<AvsBatchSegment> AvsBatchSegments { get; set; } = null!;

    public virtual DbSet<AvsBatchItem> AvsBatchItems { get; set; } = null!;

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AvsBatch>(entity =>
        {
            entity.ToTable("GpayAvsBatches");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.ExternalBatchId).HasMaxLength(64);
            entity.Property(x => x.Reference).HasMaxLength(128);
            entity.Property(x => x.SourceFileName).HasMaxLength(260);
            entity.Property(x => x.CorrelationId).HasMaxLength(64).IsRequired();
            entity.Property(x => x.Status).HasConversion<string>().HasMaxLength(32);
            entity.HasIndex(x => x.ExternalBatchId);
            entity.HasIndex(x => x.CreatedAtUtc);
        });

        modelBuilder.Entity<AvsBatchSegment>(entity =>
        {
            entity.ToTable("GpayAvsBatchSegments");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Status).HasConversion<string>().HasMaxLength(32);
            entity.Property(x => x.BankCode).HasConversion<string>().HasMaxLength(32);
            entity.HasIndex(x => x.BatchId);
            entity.HasIndex(x => new { x.BatchId, x.SegmentIndex }).IsUnique();
            entity.HasOne(x => x.Batch)
                .WithMany(x => x.Segments)
                .HasForeignKey(x => x.BatchId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<AvsBatchItem>(entity =>
        {
            entity.ToTable("GpayAvsBatchItems");
            entity.HasKey(x => x.Id);
            entity.Property(x => x.ExternalRecordId).HasMaxLength(64);
            entity.Property(x => x.Status).HasConversion<string>().HasMaxLength(32);
            entity.Property(x => x.BankCode).HasConversion<string>().HasMaxLength(32);
            entity.Property(x => x.ErrorCode).HasMaxLength(64);
            entity.Property(x => x.ErrorMessage).HasMaxLength(1024);
            entity.Property(x => x.BankReference).HasMaxLength(128);
            entity.Property(x => x.RequestJson).IsRequired();
            entity.HasIndex(x => x.BatchId);
            entity.HasIndex(x => x.SegmentId);
            entity.HasIndex(x => x.ExternalRecordId);
            entity.HasIndex(x => new { x.BatchId, x.RowNumber });
            entity.HasOne(x => x.Batch)
                .WithMany(x => x.Items)
                .HasForeignKey(x => x.BatchId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(x => x.Segment)
                .WithMany(x => x.Items)
                .HasForeignKey(x => x.SegmentId)
                .OnDelete(DeleteBehavior.NoAction);
        });
    }
}
