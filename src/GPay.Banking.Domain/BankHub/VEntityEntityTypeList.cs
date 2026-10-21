using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VEntityEntityTypeList
{
    [Column("PkEntityID")]
    public Guid PkEntityId { get; set; }

    [Column("PkEntityTypeID")]
    public Guid PkEntityTypeId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [StringLength(250)]
    [Unicode(false)]
    public string? DefaultLink { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? EntityName { get; set; }

    public bool? Active { get; set; }

    [Column("FkStatusID")]
    public Guid FkStatusId { get; set; }
}


