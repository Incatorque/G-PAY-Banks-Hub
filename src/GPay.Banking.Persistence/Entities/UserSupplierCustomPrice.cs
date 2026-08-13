using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class UserSupplierCustomPrice
{
    public Guid PkUserSupplierId { get; set; }

    public Guid FkUserId { get; set; }

    public Guid? FkSupplierId { get; set; }

    public virtual Entity? FkSupplier { get; set; }

    public virtual User FkUser { get; set; } = null!;
}
