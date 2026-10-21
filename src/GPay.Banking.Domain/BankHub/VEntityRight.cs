using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VEntityRight
{
    [Column("PkModuleID")]
    public Guid PkModuleId { get; set; }

    [StringLength(65)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("PkEntityID")]
    public Guid? PkEntityId { get; set; }

    [Column("PkGroupID")]
    public Guid? PkGroupId { get; set; }

    [Column("PkUserID")]
    public Guid? PkUserId { get; set; }
}


