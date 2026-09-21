using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class SweepChangeLog
{
    public Guid PkSweepChangeLogId { get; set; }

    public Guid? FkSweepId { get; set; }

    public Guid? FkUserId { get; set; }

    public string? LogDescription { get; set; }

    public DateTime? LogDate { get; set; }
}
