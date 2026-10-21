using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace GPay.Banking.BankHub;

[Table("Group")]
public partial class Group : Entity<Guid>
{
    [Key]
    [Column("PkGroupID")]
    public override Guid Id { get; protected set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    public bool? Active { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? Description { get; set; }

    [Column("FkOwnerEntityID")]
    public Guid? FkOwnerEntityId { get; set; }

    public bool? Deleted { get; set; }

    [InverseProperty("FkGroup")]
    public virtual ICollection<EntityConfig> EntityConfigs { get; set; } = new List<EntityConfig>();

    [InverseProperty("FkGroup")]
    public virtual ICollection<EntityGroup> EntityGroups { get; set; } = new List<EntityGroup>();

    [InverseProperty("FkGroup")]
    public virtual ICollection<EtguuserType> EtguuserTypes { get; set; } = new List<EtguuserType>();

    [InverseProperty("FkGroup")]
    public virtual ICollection<UserGroup> UserGroups { get; set; } = new List<UserGroup>();

    [InverseProperty("FkGroup")]
    public virtual ICollection<UserSelection> UserSelections { get; set; } = new List<UserSelection>();

    [InverseProperty("FkGroup")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}


