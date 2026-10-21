using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("OrderHistory")]
[Index("FkOrderId", Name = "IX_OrderHistory_FkOrderId")]
public partial class OrderHistory : Entity<Guid>
{
    [Key]
    [Column("PkOrderHistoryID")]
    public override Guid Id { get; protected set; }

    [Column("FkOrderID")]
    public Guid FkOrderId { get; set; }

    [Column("FkOrderStatusID")]
    public Guid FkOrderStatusId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime Date { get; set; }

    [StringLength(500)]
    public string? Note { get; set; }

    [Column("FkUserID")]
    public Guid? FkUserId { get; set; }

    [Column("FkHistoryCategoryID")]
    public Guid FkHistoryCategoryId { get; set; }

    public bool? Active { get; set; }

    [ForeignKey("FkHistoryCategoryId")]
    [InverseProperty("OrderHistories")]
    public virtual HistoryCategory FkHistoryCategory { get; set; } = null!;

    [ForeignKey("FkOrderId")]
    [InverseProperty("OrderHistories")]
    public virtual Order FkOrder { get; set; } = null!;

    [ForeignKey("FkOrderStatusId")]
    [InverseProperty("OrderHistories")]
    public virtual OrderStatus FkOrderStatus { get; set; } = null!;

    [ForeignKey("FkUserId")]
    [InverseProperty("OrderHistories")]
    public virtual User? FkUser { get; set; }
}


