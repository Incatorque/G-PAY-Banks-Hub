using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VBankStatementGbbilist
{
    [Column("FkAccountID")]
    public Guid? FkAccountId { get; set; }

    [Column("FkEntityID")]
    public Guid? FkEntityId { get; set; }

    [Column(TypeName = "decimal(38, 4)")]
    public decimal? Deposits { get; set; }
}


