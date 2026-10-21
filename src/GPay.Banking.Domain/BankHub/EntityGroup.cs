using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("EntityGroup")]
public partial class EntityGroup : Entity<Guid>
{
    [Key]
    [Column("PkEntityGroupID")]
    public override Guid Id { get; protected set; }

    [Column("FkEntityID")]
    public Guid FkEntityId { get; set; }

    [Column("FkGroupID")]
    public Guid FkGroupId { get; set; }

    [ForeignKey("FkEntityId")]
    [InverseProperty("EntityGroups")]
    public virtual CustomEntity FkEntity { get; set; } = null!;

    [ForeignKey("FkGroupId")]
    [InverseProperty("EntityGroups")]
    public virtual Group FkGroup { get; set; } = null!;
}


