using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

public partial class UserSecurityAnswer : Entity<Guid>
{
    [Key]
    [Column("PkUserSercurityAnswerID")]
    public override Guid Id { get; protected set; }

    [Column("FkUserID")]
    public Guid FkUserId { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string Answer { get; set; } = null!;

    [Column("FkUserSecurityQuestionsID")]
    public Guid FkUserSecurityQuestionsId { get; set; }

    [ForeignKey("FkUserId")]
    [InverseProperty("UserSecurityAnswers")]
    public virtual User FkUser { get; set; } = null!;
}


