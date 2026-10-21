using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

public partial class UserSecurityQuestion : Entity<Guid>
{
    [Key]
    [Column("PkUserSecurityQuestionID")]
    public override Guid Id { get; protected set; }

    [StringLength(100)]
    [Unicode(false)]
    public string SecurityQuestion { get; set; } = null!;
}


