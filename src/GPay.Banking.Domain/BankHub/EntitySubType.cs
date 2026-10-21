using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("EntitySubType")]
public partial class EntitySubType : Entity<Guid>
{
    [Key]
    [Column("PkEntitySubTypeID")]
    public override Guid Id { get; protected set; }

    [StringLength(250)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    public bool? Active { get; set; }

    [InverseProperty("FkEntitySubType")]
    public virtual ICollection<EntityPending> EntityPendings { get; set; } = new List<EntityPending>();
}


