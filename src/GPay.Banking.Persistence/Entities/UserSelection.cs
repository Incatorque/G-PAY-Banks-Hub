using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class UserSelection
{
    public Guid PkUserSelectionId { get; set; }

    public Guid FkUserId { get; set; }

    public Guid FkGroupId { get; set; }

    public Guid FkEntityId { get; set; }

    public Guid FkAccountId { get; set; }

    public Guid? FkVendorId { get; set; }

    public Guid FkAddressId { get; set; }

    public DateTime LastChange { get; set; }

    public virtual Account FkAccount { get; set; } = null!;

    public virtual EntityAddress FkAddress { get; set; } = null!;

    public virtual Entity FkEntity { get; set; } = null!;

    public virtual Group FkGroup { get; set; } = null!;

    public virtual User FkUser { get; set; } = null!;

    public virtual Entity? FkVendor { get; set; }
}
