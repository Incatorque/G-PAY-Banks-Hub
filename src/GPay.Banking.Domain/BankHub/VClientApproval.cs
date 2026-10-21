using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VClientApproval
{
    [Column("PkClientApprovalID")]
    public Guid PkClientApprovalId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Status { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string Client { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string? UserName { get; set; }

    [Column("iOrder")]
    [StringLength(30)]
    [Unicode(false)]
    public string? IOrder { get; set; }

    public bool? Active { get; set; }

    [Column("PkEntityID")]
    public Guid PkEntityId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? UserTo { get; set; }
}


