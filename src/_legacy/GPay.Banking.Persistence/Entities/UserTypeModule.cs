using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class UserTypeModule
{
    public Guid PkUserTypeModuleId { get; set; }

    public Guid? FkUserTypeId { get; set; }

    public Guid? FkModuleId { get; set; }

    public virtual Module? FkModule { get; set; }

    public virtual UserType? FkUserType { get; set; }
}
