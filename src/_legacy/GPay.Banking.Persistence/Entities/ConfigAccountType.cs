using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class ConfigAccountType
{
    public Guid PkConfigAccountTypeId { get; set; }

    public string Name { get; set; } = null!;

    public bool? Active { get; set; }

    public virtual ICollection<EntityConfigAccount> EntityConfigAccounts { get; set; } = new List<EntityConfigAccount>();
}
