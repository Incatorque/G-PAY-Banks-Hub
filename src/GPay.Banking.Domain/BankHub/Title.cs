using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("Title")]
public partial class Title : Entity<Guid>
{
    [Key]
    [Column("PkTitleID")]
    public override Guid Id { get; protected set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    public bool? Active { get; set; }

    [InverseProperty("FkTitle")]
    public virtual ICollection<EntityContactPending> EntityContactPendings { get; set; } = new List<EntityContactPending>();

    [InverseProperty("FkTitle")]
    public virtual ICollection<EntityContact> EntityContacts { get; set; } = new List<EntityContact>();
}


