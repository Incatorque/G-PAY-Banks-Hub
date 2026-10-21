using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VEntityBankStatementMvU
{
    public int? Total { get; set; }

    [StringLength(9)]
    [Unicode(false)]
    public string Mapped { get; set; } = null!;

    [Column("FkAccountID")]
    public Guid? FkAccountId { get; set; }

    [Column("FkEntityID")]
    public Guid? FkEntityId { get; set; }
}


