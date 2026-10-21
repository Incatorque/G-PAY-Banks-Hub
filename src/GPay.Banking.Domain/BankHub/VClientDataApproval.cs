using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VClientDataApproval
{
    [Column("PkEntityApprovalID")]
    public Guid PkEntityApprovalId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Client { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? UserName { get; set; }

    [Column("iOrder")]
    [StringLength(30)]
    [Unicode(false)]
    public string? IOrder { get; set; }

    public bool Active { get; set; }

    [Column("PkEntityID")]
    public Guid? PkEntityId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? RequestedUserName { get; set; }

    [Column("RequestedUserID")]
    public Guid? RequestedUserId { get; set; }

    public bool? New { get; set; }

    public bool? Edit { get; set; }
}


