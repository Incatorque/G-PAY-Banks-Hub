using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class UserUserType
{
    public Guid PkUserUserTypeId { get; set; }

    public Guid FkUserId { get; set; }

    public Guid FkUserTypeId { get; set; }

    public virtual User FkUser { get; set; } = null!;

    public virtual UserType FkUserType { get; set; } = null!;
}
