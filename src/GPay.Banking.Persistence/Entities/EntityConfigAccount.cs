using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class EntityConfigAccount
{
    public Guid PkEntityConfigAccountId { get; set; }

    public Guid? FkEntityConfigId { get; set; }

    public Guid? FkAccountId { get; set; }

    public Guid? FkConfigAccountTypeId { get; set; }

    public virtual Account? FkAccount { get; set; }

    public virtual ConfigAccountType? FkConfigAccountType { get; set; }

    public virtual EntityConfig? FkEntityConfig { get; set; }
}
