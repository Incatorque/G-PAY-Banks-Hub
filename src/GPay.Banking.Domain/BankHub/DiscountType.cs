using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("DiscountType")]
public partial class DiscountType : Entity<Guid>
{
    [Key]
    [Column("PkDiscountTypeID")]
    public override Guid Id { get; protected set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [InverseProperty("FkDiscountType")]
    public virtual ICollection<ProductZone> ProductZones { get; set; } = new List<ProductZone>();
}


