using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VEntityAccountLink
{
    [Column("FkAccountID")]
    public Guid FkAccountId { get; set; }

    [Column("FkEntityID")]
    public Guid FkEntityId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Entity { get; set; }

    [StringLength(20)]
    public string? ResourceType { get; set; }

    [StringLength(150)]
    [Unicode(false)]
    public string? AccountLabel { get; set; }
}


