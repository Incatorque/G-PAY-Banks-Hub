using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class UserTypeUserType
{
    public Guid PkUserTypeUserTypeId { get; set; }

    public Guid? FkUserTypeId { get; set; }

    public Guid? FkUserTypesId { get; set; }

    public virtual UserType? FkUserType { get; set; }

    public virtual UserType? FkUserTypes { get; set; }
}
