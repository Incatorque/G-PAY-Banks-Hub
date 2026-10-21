using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("LoginAudit")]
public partial class LoginAudit : Entity<Guid>
{
    [Key]
    [Column("PkLoginAuditID")]
    public override Guid Id { get; protected set; }

    [Column("FkUserID")]
    public Guid? FkUserId { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string Username { get; set; } = null!;

    [StringLength(500)]
    [Unicode(false)]
    public string Psssword { get; set; } = null!;

    [StringLength(200)]
    [Unicode(false)]
    public string? Pin { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime LoginDate { get; set; }

    [Column("IPAddress")]
    [StringLength(50)]
    [Unicode(false)]
    public string Ipaddress { get; set; } = null!;

    public bool? LoginSuccess { get; set; }

    [ForeignKey("FkUserId")]
    [InverseProperty("LoginAudits")]
    public virtual User? FkUser { get; set; }
}


