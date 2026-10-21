using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("Town")]
public partial class Town : Entity<Guid>
{
    [Key]
    [Column("PkTownID")]
    public override Guid Id { get; protected set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("FkProvinceID")]
    public Guid FkProvinceId { get; set; }

    public bool? Active { get; set; }

    [InverseProperty("FkTown")]
    public virtual ICollection<EntityAddressPending> EntityAddressPendings { get; set; } = new List<EntityAddressPending>();

    [InverseProperty("FkTown")]
    public virtual ICollection<EntityAddress> EntityAddresses { get; set; } = new List<EntityAddress>();

    [ForeignKey("FkProvinceId")]
    [InverseProperty("Towns")]
    public virtual Province FkProvince { get; set; } = null!;

    [InverseProperty("FkTown")]
    public virtual ICollection<TownSupplierZone> TownSupplierZones { get; set; } = new List<TownSupplierZone>();
}


