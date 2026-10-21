using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VUserAgent
{
    [Column("PkAgentID")]
    public Guid PkAgentId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    public bool? Active { get; set; }

    [Column("FkStatusID")]
    public Guid FkStatusId { get; set; }

    [Column("TAName")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Taname { get; set; }

    [Column("FkUserID")]
    public Guid FkUserId { get; set; }
}


