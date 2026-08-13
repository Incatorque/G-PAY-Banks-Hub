using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class UserGroup
{
    public Guid PkUserGroupId { get; set; }

    public Guid FkUserId { get; set; }

    public Guid FkGroupId { get; set; }

    public virtual Group FkGroup { get; set; } = null!;

    public virtual User FkUser { get; set; } = null!;
}
