using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("UserSupplierCustomPrice")]
public partial class UserSupplierCustomPrice : Entity<Guid>
{
    [Key]
    [Column("PkUserSupplierID")]
    public override Guid Id { get; protected set; }

    [Column("FkUserID")]
    public Guid FkUserId { get; set; }

    [Column("FkSupplierID")]
    public Guid? FkSupplierId { get; set; }

    [ForeignKey("FkSupplierId")]
    [InverseProperty("UserSupplierCustomPrices")]
    public virtual CustomEntity? FkSupplier { get; set; }

    [ForeignKey("FkUserId")]
    [InverseProperty("UserSupplierCustomPrices")]
    public virtual User FkUser { get; set; } = null!;
}


