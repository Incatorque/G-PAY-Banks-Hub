using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("TermsAndConditionsAudit")]
public partial class TermsAndConditionsAudit : Entity<Guid>
{
    [Key]
    [Column("PkTermsId")]
    public override Guid Id { get; protected set; }

    [StringLength(50)]
    [Unicode(false)]
    public string UserName { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string UserEmail { get; set; } = null!;

    public bool? UserTermsAccepted { get; set; }

    [Column("GPayTermsAccepted")]
    public bool? GpayTermsAccepted { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DateAccepted { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DateReset { get; set; }

    public Guid FkUserId { get; set; }

    public bool? Active { get; set; }
}


