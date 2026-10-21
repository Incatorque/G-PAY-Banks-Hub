using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("EntityStatus")]
public partial class EntityStatus : Entity<Guid>
{
    [Key]
    [Column("PkStatusID")]
    public override Guid Id { get; protected set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [InverseProperty("FkStatus")]
    public virtual ICollection<EntityPending> EntityPendings { get; set; } = new List<EntityPending>();
}


