using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VuserSecurity
{
    [Column("PkUserSecurityQuestionID")]
    public Guid PkUserSecurityQuestionId { get; set; }

    [Column("PkUserSercurityAnswerID")]
    public Guid? PkUserSercurityAnswerId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string SecurityQuestion { get; set; } = null!;

    [StringLength(200)]
    [Unicode(false)]
    public string? Answer { get; set; }

    [Column("FkUserID")]
    public Guid? FkUserId { get; set; }
}


