using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("EntityModule")]
public partial class EntityModule : Entity<Guid>
{
    [Key]
    [Column("PkEntityModuleID")]
    public override Guid Id { get; protected set; }

    [Column("FkEntityID")]
    public Guid FkEntityId { get; set; }

    [Column("FkModuleID")]
    public Guid FkModuleId { get; set; }

    [ForeignKey("FkEntityId")]
    [InverseProperty("EntityModules")]
    public virtual CustomEntity FkEntity { get; set; } = null!;

    [ForeignKey("FkModuleId")]
    [InverseProperty("EntityModules")]
    public virtual Module FkModule { get; set; } = null!;
}


