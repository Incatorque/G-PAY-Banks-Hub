using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class HistoryCategory
{
    public Guid PkHistoryCategoryId { get; set; }

    public string Description { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<OrderHistory> OrderHistories { get; set; } = new List<OrderHistory>();
}
