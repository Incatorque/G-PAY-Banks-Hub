using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("ProductExcluded")]
public partial class ProductExcluded : Entity<Guid>
{
    [Key]
    [Column("PkProductExludedID")]
    public override Guid Id { get; protected set; }

    [Column("FkSupplierClientID")]
    public Guid FkSupplierClientId { get; set; }

    [Column("FkProductID")]
    public Guid FkProductId { get; set; }

    [ForeignKey("FkProductId")]
    [InverseProperty("ProductExcludeds")]
    public virtual Product FkProduct { get; set; } = null!;

    [ForeignKey("FkSupplierClientId")]
    [InverseProperty("ProductExcludeds")]
    public virtual SupplierClient FkSupplierClient { get; set; } = null!;
}


