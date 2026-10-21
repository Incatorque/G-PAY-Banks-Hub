using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VUserTypesAllowedPerUser
{
    [Column("pkUserTypeID")]
    public Guid PkUserTypeId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    public bool? Active { get; set; }

    [Column("FkUserID")]
    public Guid FkUserId { get; set; }

    [Column("FkUserTypesID")]
    public Guid? FkUserTypesId { get; set; }
}


