using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("EntityEntityType")]
[Index("FkEntityTypeId", Name = "NCI_FKEntityTypeID_FKEntityID")]
public partial class EntityEntityType : Entity<Guid>
{
    [Key]
    [Column("PkEntityEntityTypeID")]
    public override Guid Id { get; protected set; }

    [Column("FkEntityID")]
    public Guid FkEntityId { get; set; }

    [Column("FkEntityTypeID")]
    public Guid FkEntityTypeId { get; set; }

    [ForeignKey("FkEntityId")]
    [InverseProperty("EntityEntityTypes")]
    public virtual CustomEntity FkEntity { get; set; } = null!;

    [ForeignKey("FkEntityTypeId")]
    [InverseProperty("EntityEntityTypes")]
    public virtual EntityType FkEntityType { get; set; } = null!;
}


