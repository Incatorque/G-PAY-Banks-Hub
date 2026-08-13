using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class VBankTime
{
    public Guid PkBankTimeId { get; set; }

    public string Name { get; set; } = null!;

    public int DayOfWeek { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public bool? IsInternal { get; set; }

    public bool? Active { get; set; }

    public bool? Bactive { get; set; }

    public Guid FkBankId { get; set; }

    public decimal? Amount { get; set; }
}
