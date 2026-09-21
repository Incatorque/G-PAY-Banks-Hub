using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class H2hvalidationStatus
{
    public Guid PkH2hvalidationStatusId { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
