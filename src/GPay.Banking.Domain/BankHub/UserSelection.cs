using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("UserSelection")]
public partial class UserSelection : Entity<Guid>
{
    [Key]
    [Column("PkUserSelectionID")]
    public override Guid Id { get; protected set; }

    [Column("FkUserID")]
    public Guid FkUserId { get; set; }

    [Column("FkGroupID")]
    public Guid FkGroupId { get; set; }

    [Column("FkEntityID")]
    public Guid FkEntityId { get; set; }

    [Column("FkAccountID")]
    public Guid FkAccountId { get; set; }

    [Column("FkVendorID")]
    public Guid? FkVendorId { get; set; }

    [Column("FkAddressID")]
    public Guid FkAddressId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime LastChange { get; set; }

    [ForeignKey("FkAccountId")]
    [InverseProperty("UserSelections")]
    public virtual Account FkAccount { get; set; } = null!;

    [ForeignKey("FkAddressId")]
    [InverseProperty("UserSelections")]
    public virtual EntityAddress FkAddress { get; set; } = null!;

    [ForeignKey("FkEntityId")]
    [InverseProperty("UserSelectionFkEntities")]
    public virtual CustomEntity FkEntity { get; set; } = null!;

    [ForeignKey("FkGroupId")]
    [InverseProperty("UserSelections")]
    public virtual Group FkGroup { get; set; } = null!;

    [ForeignKey("FkUserId")]
    [InverseProperty("UserSelections")]
    public virtual User FkUser { get; set; } = null!;

    [ForeignKey("FkVendorId")]
    [InverseProperty("UserSelectionFkVendors")]
    public virtual CustomEntity? FkVendor { get; set; }
}


