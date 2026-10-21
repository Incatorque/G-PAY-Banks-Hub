using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("UserPasswordChangeLog")]
public partial class UserPasswordChangeLog : Entity<Guid>
{
    [Key]
    [Column("PKUserPasswordChangeLogId")]
    public override Guid Id { get; protected set; }

    [Column(TypeName = "datetime")]
    public DateTime UpdatedDate { get; set; }

    [Column("FKUserId")]
    public Guid FkuserId { get; set; }

    [ForeignKey("FkuserId")]
    [InverseProperty("UserPasswordChangeLogs")]
    public virtual User Fkuser { get; set; } = null!;
}


