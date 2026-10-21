using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VTermsAndConditionsAudit
{
    public Guid PkTermsId { get; set; }

    [StringLength(8000)]
    [Unicode(false)]
    public string? UserName { get; set; }

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


