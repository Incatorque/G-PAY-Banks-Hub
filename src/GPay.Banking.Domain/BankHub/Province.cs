using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("Province")]
public partial class Province : Entity<Guid>
{
    [Key]
    [Column("PkProvinceID")]
    public override Guid Id { get; protected set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    public bool? Active { get; set; }

    [Column("FkCountryID")]
    public int? FkCountryId { get; set; }

    [InverseProperty("FkProvince")]
    public virtual ICollection<EntityAddressPending> EntityAddressPendings { get; set; } = new List<EntityAddressPending>();

    [InverseProperty("FkProvince")]
    public virtual ICollection<EntityAddress> EntityAddresses { get; set; } = new List<EntityAddress>();

    [InverseProperty("FkProvince")]
    public virtual ICollection<MagisterialDistrict> MagisterialDistricts { get; set; } = new List<MagisterialDistrict>();

    [InverseProperty("FkProvince")]
    public virtual ICollection<Town> Towns { get; set; } = new List<Town>();
}


