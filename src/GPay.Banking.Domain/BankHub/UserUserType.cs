using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Table("UserUserType")]
[Keyless]
public partial class UserUserType
{
    [Column("PkUserUserTypeID")]
    public Guid PkUserUserTypeId { get; set; }

    [Column("FkUserID")]
    public Guid FkUserId { get; set; }

    [Column("FkUserTypeID")]
    public Guid FkUserTypeId { get; set; }

    [ForeignKey("FkUserId")]
    [InverseProperty("UserUserTypes")]
    public virtual User FkUser { get; set; } = null!;

    [ForeignKey("FkUserTypeId")]
    [InverseProperty("UserUserTypes")]
    public virtual UserType FkUserType { get; set; } = null!;
}


