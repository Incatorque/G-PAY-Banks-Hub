using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("MagisterialDistrict")]
public partial class MagisterialDistrict : Entity<Guid>
{
    [Key]
    [Column("PkMagisterialDistrictID")]
    public override Guid Id { get; protected set; }

    [StringLength(255)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("FkProvinceID")]
    public Guid FkProvinceId { get; set; }

    public bool? Active { get; set; }

    [InverseProperty("FkMagisterialDistrict")]
    public virtual ICollection<EntityAddress> EntityAddresses { get; set; } = new List<EntityAddress>();

    [ForeignKey("FkProvinceId")]
    [InverseProperty("MagisterialDistricts")]
    public virtual Province FkProvince { get; set; } = null!;

    [InverseProperty("FkMagisterialDistrict")]
    public virtual ICollection<MagisterialDistrictGridZone> MagisterialDistrictGridZones { get; set; } = new List<MagisterialDistrictGridZone>();
}


