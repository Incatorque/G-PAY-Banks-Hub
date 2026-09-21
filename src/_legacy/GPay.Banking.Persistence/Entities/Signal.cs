using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class Signal
{
    public int Id { get; set; }

    public string Message { get; set; } = null!;

    public Guid FkUserId { get; set; }

    public bool? IsSent { get; set; }

    public Guid FkOrderId { get; set; }
}
