using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace GPay.Banking.BankHub;

[Keyless]
public partial class VMagisterialDistrictProvince
{
    [Column("PkMagisterialDistrictID")]
    public Guid PkMagisterialDistrictId { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string ProvinceName { get; set; } = null!;

    public bool? Active { get; set; }
}


