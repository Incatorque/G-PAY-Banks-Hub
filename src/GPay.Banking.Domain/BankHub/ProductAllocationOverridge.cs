using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("ProductAllocationOverridge")]
public partial class ProductAllocationOverridge : Entity<Guid>
{
    [Key]
    [Column("PkAllocationOverID")]
    public override Guid Id { get; protected set; }

    [Column("ProductAllocationQTY", TypeName = "decimal(19, 4)")]
    public decimal? ProductAllocationQty { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? ProductAllocationTerms { get; set; }

    [Column(TypeName = "decimal(19, 4)")]
    public decimal? OverridgeQty { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? OverridgeTerms { get; set; }

    [Column("FkProductZoneID")]
    public Guid FkProductZoneId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DateCreated { get; set; }

    [ForeignKey("FkProductZoneId")]
    [InverseProperty("ProductAllocationOverridges")]
    public virtual ProductZone FkProductZone { get; set; } = null!;
}


