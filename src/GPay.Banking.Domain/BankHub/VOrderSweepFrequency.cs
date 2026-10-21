using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VOrderSweepFrequency
{
    [Column("PkOrderSweepFrequencyID")]
    public int PkOrderSweepFrequencyId { get; set; }

    public int Days { get; set; }

    public bool? Active { get; set; }

    public bool? Deleted { get; set; }
}


