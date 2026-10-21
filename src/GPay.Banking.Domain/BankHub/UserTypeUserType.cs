using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("UserTypeUserType")]
public partial class UserTypeUserType : Entity<Guid>
{
    [Key]
    [Column("PkUserTypeUserTypeID")]
    public override Guid Id { get; protected set; }

    [Column("FkUserTypeID")]
    public Guid? FkUserTypeId { get; set; }

    [Column("FkUserTypesID")]
    public Guid? FkUserTypesId { get; set; }

    [ForeignKey("FkUserTypeId")]
    [InverseProperty("UserTypeUserTypeFkUserTypes")]
    public virtual UserType? FkUserType { get; set; }

    [ForeignKey("FkUserTypesId")]
    [InverseProperty("UserTypeUserTypeFkUserTypesNavigation")]
    public virtual UserType? FkUserTypes { get; set; }
}


