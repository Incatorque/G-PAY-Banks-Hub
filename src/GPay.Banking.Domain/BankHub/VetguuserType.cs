using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VetguuserType
{
    [Column("PkETGUUserTypeID")]
    public Guid PkEtguuserTypeId { get; set; }

    [Column("FkEntityTypeID")]
    public Guid FkEntityTypeId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string EntityTypeName { get; set; } = null!;

    [Column("FkGroupID")]
    public Guid? FkGroupId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string GroupName { get; set; } = null!;

    [Column("FKUserID")]
    public Guid? FkuserId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? UserName { get; set; }

    [Column("FkUserTypeID")]
    public Guid FkUserTypeId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string UserTypeName { get; set; } = null!;
}


