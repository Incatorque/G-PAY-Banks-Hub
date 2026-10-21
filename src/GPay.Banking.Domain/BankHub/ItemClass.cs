using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("ItemClass")]
public partial class ItemClass : Entity<Guid>
{
    [Key]
    [Column("PkClassID")]
    public override Guid Id { get; protected set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [StringLength(500)]
    [Unicode(false)]
    public string? ClassDescription { get; set; }

    public bool? Active { get; set; }

    [Column("FkOwnerEntityID")]
    public Guid? FkOwnerEntityId { get; set; }
}


