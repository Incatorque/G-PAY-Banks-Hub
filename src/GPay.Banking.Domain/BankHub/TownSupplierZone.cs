using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("TownSupplierZone")]
public partial class TownSupplierZone : Entity<Guid>
{
    [Key]
    [Column("PkTownSupplierZoneID")]
    public override Guid Id { get; protected set; }

    [Column("FkTownID")]
    public Guid FkTownId { get; set; }

    [Column("FkSupplierZoneID")]
    public Guid FkSupplierZoneId { get; set; }

    [ForeignKey("FkSupplierZoneId")]
    [InverseProperty("TownSupplierZones")]
    public virtual SupplierZone FkSupplierZone { get; set; } = null!;

    [ForeignKey("FkTownId")]
    [InverseProperty("TownSupplierZones")]
    public virtual Town FkTown { get; set; } = null!;
}


