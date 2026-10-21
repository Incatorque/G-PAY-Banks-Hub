using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("MagisterialDistrictGridZone")]
public partial class MagisterialDistrictGridZone : Entity<Guid>
{
    [Key]
    [Column("PkMagisterialDistrictGridZoneID")]
    public override Guid Id { get; protected set; }

    [Column("FkMagisterialDistrictID")]
    public Guid FkMagisterialDistrictId { get; set; }

    [Column("FkGridZoneID")]
    public Guid FkGridZoneId { get; set; }

    public bool? Active { get; set; }

    [ForeignKey("FkGridZoneId")]
    [InverseProperty("MagisterialDistrictGridZones")]
    public virtual GridZone FkGridZone { get; set; } = null!;

    [ForeignKey("FkMagisterialDistrictId")]
    [InverseProperty("MagisterialDistrictGridZones")]
    public virtual MagisterialDistrict FkMagisterialDistrict { get; set; } = null!;
}


