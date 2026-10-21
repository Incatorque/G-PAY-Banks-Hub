using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class Signal
{
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Message { get; set; } = null!;

    public Guid FkUserId { get; set; }

    public bool? IsSent { get; set; }

    public Guid FkOrderId { get; set; }
}


