using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class OrderHistory
{
    public Guid PkOrderHistoryId { get; set; }

    public Guid FkOrderId { get; set; }

    public Guid FkOrderStatusId { get; set; }

    public DateTime Date { get; set; }

    public string? Note { get; set; }

    public Guid? FkUserId { get; set; }

    public Guid FkHistoryCategoryId { get; set; }

    public bool? Active { get; set; }

    public virtual HistoryCategory FkHistoryCategory { get; set; } = null!;

    public virtual Order FkOrder { get; set; } = null!;

    public virtual OrderStatus FkOrderStatus { get; set; } = null!;

    public virtual User? FkUser { get; set; }
}
