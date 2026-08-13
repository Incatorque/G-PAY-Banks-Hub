using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class UserPin
{
    public Guid PkUserPin { get; set; }

    public Guid FkUserId { get; set; }

    public int FkSystemId { get; set; }

    public string Pin { get; set; } = null!;

    public DateTime IssuedAt { get; set; }

    public DateTime ExpiresAt { get; set; }

    public DateTime? RedeemedAt { get; set; }

    public virtual IntegratedSystem FkSystem { get; set; } = null!;
}
