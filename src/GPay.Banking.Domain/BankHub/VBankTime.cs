using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VBankTime
{
    [Column("PkBankTimeID")]
    public Guid PkBankTimeId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    public int DayOfWeek { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public bool? IsInternal { get; set; }

    public bool? Active { get; set; }

    [Column("BActive")]
    public bool? Bactive { get; set; }

    [Column("FkBankID")]
    public Guid FkBankId { get; set; }

    [Column(TypeName = "decimal(16, 4)")]
    public decimal? Amount { get; set; }
}


