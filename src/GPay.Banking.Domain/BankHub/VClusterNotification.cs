using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VClusterNotification
{
    public Guid PkClusterId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Subject { get; set; } = null!;

    [StringLength(250)]
    [Unicode(false)]
    public string? Destination { get; set; }

    public bool? Active { get; set; }
}


