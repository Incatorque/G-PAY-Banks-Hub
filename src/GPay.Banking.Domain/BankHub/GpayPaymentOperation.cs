using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Index("ApiRef", Name = "IX_GpayPaymentOperations_ApiRef")]
[Index("CorrelationId", Name = "IX_GpayPaymentOperations_CorrelationId")]
[Index("CreatedAtUtc", Name = "IX_GpayPaymentOperations_CreatedAtUtc")]
[Index("TransactionRef", Name = "IX_GpayPaymentOperations_TransactionRef")]
public partial class GpayPaymentOperation : Entity<Guid>
{
    [Key]
    public override Guid Id { get; protected set; }

    [StringLength(64)]
    public string CorrelationId { get; set; } = null!;

    [StringLength(32)]
    public string BankCode { get; set; } = null!;

    [StringLength(128)]
    public string? TransactionRef { get; set; }

    [StringLength(128)]
    public string? ApiRef { get; set; }

    [StringLength(32)]
    public string Status { get; set; } = null!;

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Amount { get; set; }

    [StringLength(8)]
    public string? Currency { get; set; }

    [StringLength(16)]
    public string? Rail { get; set; }

    public string? RequestJson { get; set; }

    public string? ResponseJson { get; set; }

    public DateTimeOffset CreatedAtUtc { get; set; }

    public DateTimeOffset? UpdatedAtUtc { get; set; }
}


