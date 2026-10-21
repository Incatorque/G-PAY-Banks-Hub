using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("UserTypeModule")]
public partial class UserTypeModule : Entity<Guid>
{
    [Key]
    [Column("pkUserTypeModuleID")]
    public override Guid Id { get; protected set; }

    [Column("fkUserTypeID")]
    public Guid? FkUserTypeId { get; set; }

    [Column("fkModuleID")]
    public Guid? FkModuleId { get; set; }

    [ForeignKey("FkModuleId")]
    [InverseProperty("UserTypeModules")]
    public virtual Module? FkModule { get; set; }

    [ForeignKey("FkUserTypeId")]
    [InverseProperty("UserTypeModules")]
    public virtual UserType? FkUserType { get; set; }
}


