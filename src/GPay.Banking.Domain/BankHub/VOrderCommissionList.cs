using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VOrderCommissionList
{
    [Column("PkOrderID")]
    public Guid PkOrderId { get; set; }

    [Column("FkAgentID")]
    public Guid FkAgentId { get; set; }

    [Column(TypeName = "decimal(38, 6)")]
    public decimal? CommissionSum { get; set; }
}


