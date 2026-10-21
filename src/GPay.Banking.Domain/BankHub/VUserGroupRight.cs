using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VUserGroupRight
{
    [Column("PkUserID")]
    public Guid PkUserId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? User { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Email { get; set; }

    [Column("PkGroupID")]
    public Guid PkGroupId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Group { get; set; } = null!;

    public bool? Active { get; set; }
}


