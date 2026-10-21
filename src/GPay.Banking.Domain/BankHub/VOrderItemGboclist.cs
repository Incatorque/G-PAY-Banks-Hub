using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VOrderItemGboclist
{
    [Column("FkClientID")]
    public Guid FkClientId { get; set; }

    [Column("FkFromAccountID")]
    public Guid? FkFromAccountId { get; set; }

    [Column("SumVATIncl", TypeName = "decimal(38, 6)")]
    public decimal? SumVatincl { get; set; }
}


