using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class IntegratedSystem
{
    public int PkSystemId { get; set; }

    public string Name { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    public bool? Active { get; set; }

    public string Origin { get; set; } = null!;

    public bool? IsApi { get; set; }

    public Guid? FkEntityConfigId { get; set; }

    public bool? IsNative { get; set; }

    public virtual EntityConfig? FkEntityConfig { get; set; }

    public virtual ICollection<PaymentRequest> PaymentRequests { get; set; } = new List<PaymentRequest>();

    public virtual ICollection<UserPin> UserPins { get; set; } = new List<UserPin>();
}
