using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VUserClient
{
    [Column("PkEntityID")]
    public Guid PkEntityId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    public bool? Active { get; set; }

    [Column("FkUserID")]
    public Guid FkUserId { get; set; }

    [Column("FkStatusID")]
    public Guid FkStatusId { get; set; }

    [Column("TAName")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Taname { get; set; }
}


