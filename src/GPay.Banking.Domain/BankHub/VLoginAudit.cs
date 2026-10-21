using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VLoginAudit
{
    [Column("PkLoginAuditID")]
    public Guid PkLoginAuditId { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string Username { get; set; } = null!;

    [StringLength(10)]
    [Unicode(false)]
    public string? Passsword { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? Pin { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime LoginDate { get; set; }

    [Column("IPAddress")]
    [StringLength(50)]
    [Unicode(false)]
    public string Ipaddress { get; set; } = null!;

    [StringLength(10)]
    [Unicode(false)]
    public string LoginSuccess { get; set; } = null!;

    public bool Active { get; set; }
}


