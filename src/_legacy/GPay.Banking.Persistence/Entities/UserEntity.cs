using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class UserEntity
{
    public Guid PkUserEntityId { get; set; }

    public Guid FkUserId { get; set; }

    public Guid FkEntityId { get; set; }

    public virtual Entity FkEntity { get; set; } = null!;

    public virtual User FkUser { get; set; } = null!;
}
