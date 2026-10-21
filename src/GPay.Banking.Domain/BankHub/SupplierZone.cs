using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("SupplierZone")]
public partial class SupplierZone : Entity<Guid>
{
    [Key]
    [Column("PkSupplierZoneID")]
    public override Guid Id { get; protected set; }

    [StringLength(250)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column("FkSupplierID")]
    public Guid FkSupplierId { get; set; }

    public bool? Active { get; set; }

    [ForeignKey("FkSupplierId")]
    [InverseProperty("SupplierZones")]
    public virtual CustomEntity FkSupplier { get; set; } = null!;

    [InverseProperty("FkSupplierZone")]
    public virtual ICollection<ProductZone> ProductZones { get; set; } = new List<ProductZone>();

    [InverseProperty("FkSupplierZone")]
    public virtual ICollection<TownSupplierZone> TownSupplierZones { get; set; } = new List<TownSupplierZone>();
}


