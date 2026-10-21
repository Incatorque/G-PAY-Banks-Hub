using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VUploadReference
{
    public int Id { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Reference { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string? ControllingEntity { get; set; }

    public bool? Active { get; set; }
}


