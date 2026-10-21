using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
[Table("GlobalPublicRecipient")]
public partial class GlobalPublicRecipient
{
    [Column("RECIP_NO")]
    [StringLength(50)]
    public string? RecipNo { get; set; }

    [Column("NAME0")]
    [StringLength(50)]
    public string? Name0 { get; set; }

    [Column("BRANCH")]
    [StringLength(50)]
    public string? Branch { get; set; }
}


