using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class BankTime
{
    public Guid PkBankTimeId { get; set; }

    public Guid FkBankId { get; set; }

    public bool? IsInternal { get; set; }

    public int DayOfWeek { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public bool? Active { get; set; }

    public DateTime DateCreated { get; set; }

    public decimal? Amount { get; set; }

    public virtual Bank FkBank { get; set; } = null!;
}
