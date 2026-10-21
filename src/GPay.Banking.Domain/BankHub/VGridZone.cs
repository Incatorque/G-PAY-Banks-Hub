using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VGridZone
{
    [Column("PkGridZoneID")]
    public Guid PkGridZoneId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string GridZone { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Grid { get; set; } = null!;

    [Column("PkGridID")]
    public Guid PkGridId { get; set; }

    [StringLength(8)]
    [Unicode(false)]
    public string Status { get; set; } = null!;
}


