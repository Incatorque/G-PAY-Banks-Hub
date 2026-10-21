using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VUserGroup
{
    [Column("PkUserID")]
    public Guid PkUserId { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string Password { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? Email { get; set; }

    public bool? Active { get; set; }

    [Column("UIPersistance", TypeName = "xml")]
    public string? Uipersistance { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? Comment { get; set; }

    public bool? Status { get; set; }

    [Column("FkGroupID")]
    public Guid? FkGroupId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? GroupName { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Name { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastLoginDate { get; set; }

    [StringLength(8)]
    [Unicode(false)]
    public string AccountStatus { get; set; } = null!;

    [StringLength(250)]
    [Unicode(false)]
    public string? ImageLocation { get; set; }
}


