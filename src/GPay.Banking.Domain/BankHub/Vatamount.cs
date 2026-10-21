using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Table("VATAmount")]
[Keyless]
public partial class Vatamount
{
    [Column("PkVatID")]
    public Guid PkVatId { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? Amount { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Expired { get; set; }
}


