using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VOrderHistory
{
    public Guid FkOrderId { get; set; }

    public DateTime Date { get; set; }

    public string? Note { get; set; }

    public string OrderStatus { get; set; } = null!;

    public string? UserEmail { get; set; }

    public Guid PkOrderHistoryId { get; set; }
}
