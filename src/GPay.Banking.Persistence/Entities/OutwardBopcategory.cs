using System;
using System.Collections.Generic;

namespace GPay.Banking.Persistence.Entities;

public partial class OutwardBopcategory
{
    public Guid PkoutwardBopcategoryId { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public bool Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public Guid UpdatedBy { get; set; }

    public bool Active { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual User UpdatedByNavigation { get; set; } = null!;
}
