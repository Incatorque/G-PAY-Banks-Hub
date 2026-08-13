using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class Group
{
    public Guid PkGroupId { get; set; }

    public string Name { get; set; } = null!;

    public bool? Active { get; set; }

    public string? Description { get; set; }

    public Guid? FkOwnerEntityId { get; set; }

    public bool? Deleted { get; set; }

    public virtual ICollection<EntityConfig> EntityConfigs { get; set; } = new List<EntityConfig>();

    public virtual ICollection<EntityGroup> EntityGroups { get; set; } = new List<EntityGroup>();

    public virtual ICollection<EtguuserType> EtguuserTypes { get; set; } = new List<EtguuserType>();

    public virtual ICollection<UserGroup> UserGroups { get; set; } = new List<UserGroup>();

    public virtual ICollection<UserSelection> UserSelections { get; set; } = new List<UserSelection>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
